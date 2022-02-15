using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DogAI : MonoBehaviour
{
    public AIPath ai;
    public bool isLeft, isLive = true;
    public GameObject invisibleAITarget;
    bool endLeft = false, endRight = false, lifeTime = true, isRunning = false;
    GameObject invisibleTargetLeft, invisibleTargetRight;
    AIDestinationSetter aiSetter;
    Transform nextPosMove, limitLeft, limitRight;
    AIPath aiPath;
    IEnumerator lifeCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        aiSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        invisibleTargetLeft = GameObject.Find("invisibleAiTargetLeft");
        invisibleTargetRight = GameObject.Find("invisibleAiTargetRight");
        limitLeft = GameObject.Find("LimitLeft").transform;
        limitRight = GameObject.Find("LimitRight").transform;

        lifeCoroutine = LifeTimeDog();
        if (transform.parent.name == "RightDog")
        {
            aiSetter.target = nextPosMove = invisibleTargetLeft.transform;
        }
        else if (transform.parent.name == "LeftDog")
        {
            aiSetter.target = nextPosMove = invisibleTargetRight.transform;
        }
        StartCoroutine(Patrol());
    }
    IEnumerator LifeTimeDog()
    {
        int lifeCycleWalk = Random.Range(3,5);
        Transform rd = transform;
        if (Random.Range(0, 2) == 1)
        {
            rd = limitLeft;
        }
        else rd = limitRight;
        while (true)
        {
            yield return null;
            if (!aiSetter.isTarget)
            {
                int d = lifeCycleWalk;
                while (d > 0)
                {
                    yield return new WaitForSeconds(1);
                    yield return null;
                    d--;
                   if (d == 0)
                    {
                        yield return null;
                        if (!aiPath.reachedEndOfPath)
                        {
                            yield return new WaitUntil(() => aiPath.reachedEndOfPath);
                            yield return null;
                            lifeTime = false;
                            aiSetter.target = rd;
                        }
                        else
                        {
                            yield return null;
                            lifeTime = false;
                            aiSetter.target = rd;
                        }

                    }
                }
            }
        }

    }
    IEnumerator Patrol()
    {
        while (true)
        {
            yield return null;
            if (aiSetter.isChasing)
            {
                int time = 5;
                int restoreTime = time;
                while (time > 0)
                {
                    aiSetter.isChasing = false;
                    yield return new WaitForSeconds(1);
                    yield return null;
                    if (aiSetter.isChasing)
                    {
                        time = restoreTime;
                    }
                    time--;
                }
            }
            else
            {
                yield return null;
                if (!aiSetter.isTarget && lifeTime)
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                         StartCoroutine(lifeCoroutine);
                    }
                    aiSetter.target = nextPosMove;

                    if (nextPosMove.transform.name == "invisibleAiTargetRight")
                    {
                        if (endRight)
                        {
                            nextPosMove = invisibleTargetLeft.transform;
                        }
                    }
                    else if (nextPosMove.transform.name == "invisibleAiTargetLeft")
                    {
                        if (endLeft)
                        {
                            nextPosMove = invisibleTargetRight.transform;
                        }
                    }
                }
                else if(aiSetter.isTarget)
                {
                    StopCoroutine(lifeCoroutine);
                    isRunning = false;
                    lifeTime = true;
                }
            }

        }
    }


    // Update is called once per frame
    void Update()
    {

        if (ai.desiredVelocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = true;
        }
        else if (ai.desiredVelocity.x >= -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Food")
        {
            Destroy(collision.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Limit")
        {
            isLive = false;
            Destroy(gameObject,0.1f);
        }

        if (collision.transform.name == "invisibleAiTargetLeft")
        {
            endLeft = true;
            endRight = false;
        }
        else if (collision.transform.name == "invisibleAiTargetRight")
        {

            endRight = true;
            endLeft = false;
        }

    }
}
