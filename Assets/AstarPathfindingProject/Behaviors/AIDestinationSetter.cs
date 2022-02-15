using UnityEngine;
using System.Collections;

namespace Pathfinding
{
    /// <summary>
    /// Sets the destination of an AI to the position of a specified object.
    /// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
    /// This component will then make the AI move towards the <see cref="target"/> set on this component.
    ///
    /// See: <see cref="Pathfinding.IAstarAI.destination"/>
    ///
    /// [Open online documentation to see images]
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        /// <summary>The object that the AI should move to</summary>
        public Transform target;
        public float dogTargetSpeed, dogChasingSpeed,dogPatrolSpeed;
        LayerMask magnetOfPlayer;
        LayerMask player, food;
        IAstarAI ai;
        AIPath aiControlPath;
        public Transform[] listTarget;
        RaycastHit2D hitInfo;
        public bool isTarget =false , isChasing = false;
        private void Start()
        {
            magnetOfPlayer = LayerMask.GetMask("Magnet");
            player = LayerMask.GetMask("Player");
            food = LayerMask.GetMask("Food");
            aiControlPath = GetComponent<AIPath>();
            aiControlPath.maxSpeed = 1;
        }
        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {

            if (transform.localScale.x == -1)
            {
                hitInfo = Physics2D.Raycast(transform.GetChild(0).position, Vector2.left, Mathf.Infinity, ~magnetOfPlayer);
            }
            else if (transform.localScale.x == 1)
            {
                hitInfo = Physics2D.Raycast(transform.GetChild(0).position, Vector2.right, Mathf.Infinity, ~magnetOfPlayer);
            }
            //check cicle zone to detect player
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, player);
            if (colliders.Length > 0)
            {
                isTarget = true;
                isChasing = true;
                aiControlPath.maxSpeed = 3;
                target = colliders[0].transform;
            }
            else
            {
                //check cicle zone to detect food
                Collider2D[] foodcolliders = Physics2D.OverlapCircleAll(transform.position, 2f, food);
                if (foodcolliders.Length == 1)
                {
                    isTarget = true;
                    aiControlPath.maxSpeed = dogTargetSpeed;
                    target = foodcolliders[0].transform;
                }
                else if (foodcolliders.Length > 1)
                {
                    isTarget = true;
                    aiControlPath.maxSpeed = dogTargetSpeed;
                    Transform saveMinDistance = foodcolliders[0].transform;
                    float min_distance = Vector3.Distance(foodcolliders[0].transform.position, transform.position);
                    //check wich food closer
                    target = MinDistance(foodcolliders, ref saveMinDistance, ref min_distance);

                }
                else if (hitInfo.collider.transform.tag == "Player")
                {
                    //check raycast detect player 
                    aiControlPath.maxSpeed = dogChasingSpeed;
                    isTarget = true;
                    isChasing = true;
                    target = hitInfo.collider.transform;
                }
                else if (hitInfo.collider.transform.tag == "Food")
                {
                    //check raycast detect food 
                    aiControlPath.maxSpeed = dogTargetSpeed;
                    isTarget = true;
                    target = hitInfo.collider.transform;
                }
                else
                {
                    isTarget = false;
                    aiControlPath.maxSpeed = dogPatrolSpeed;
                }
              
            }
           
            if (target != null && ai != null) ai.destination = target.position;
        }
        private Transform MinDistance(Collider2D[] foodcolliders, ref Transform saveMinDistance, ref float min_distance)
        {
            for (int i = 1; i < foodcolliders.Length; i++)
            {
                float distance = Vector3.Distance(foodcolliders[i].transform.position, transform.position);
                if (distance < min_distance)
                {
                    min_distance = distance;
                    saveMinDistance = foodcolliders[i].transform;
                }
            }

            return target = saveMinDistance;
        }
    }
}
