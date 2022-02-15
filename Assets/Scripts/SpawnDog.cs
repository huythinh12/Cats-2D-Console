using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDog : MonoBehaviour
{
    Vector3 posDog, leftPos, rightPos;
    public GameObject leftDog, rightDog;
    public GameObject dog;
    public Transform alertRight, alertLeft;
    public GameObject alertShape;
    GameObject newAlert;
    Vector2 newPosAlert;
    Transform saveParent,saveSideAlert;
    bool endAlert = false,isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        rightPos = transform.GetChild(0).localPosition;
        leftPos = transform.GetChild(1).localPosition;

        StartCoroutine(RandomSpawnDog());
    }

    IEnumerator RandomSpawnDog()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5,6));
           
            if (Random.Range(0, 2) == 1)
            {
                posDog = rightPos;
                saveParent = rightDog.transform;
                saveSideAlert = alertRight;
                newPosAlert = new Vector2(10.46f, saveSideAlert.position.y);
                
            }
            else
            {
                posDog = leftPos;
                saveParent = leftDog.transform;
                saveSideAlert = alertLeft;
                newPosAlert = new Vector2(-10.46f, saveSideAlert.position.y);
            }
            newAlert = Instantiate(alertShape, saveSideAlert.position, Quaternion.identity, saveSideAlert);
            if(newAlert.transform.parent.name == "alertLeft")
            {
                newAlert.GetComponent<SpriteRenderer>().flipX =true;
            }
            endAlert = false;
            yield return new WaitForSeconds(5);
            endAlert = true;
            var newDog = Instantiate(dog, posDog, Quaternion.identity, saveParent);
            yield return new WaitUntil(() => !newDog.GetComponent<DogAI>().isLive);

        }

    }
    IEnumerator DestroyAlert()
    {
        yield return new WaitForSeconds(2.5f);
        isRunning = false;
        Destroy(newAlert);
    }
    // Update is called once per frame
    void Update()
    {
        if (newAlert)
        {
            if (endAlert)
            {
                newAlert.transform.position = Vector2.MoveTowards(newAlert.transform.position, saveSideAlert.position, 5 * Time.deltaTime);
                if (!isRunning)
                {
                    isRunning = true;
                    StartCoroutine(DestroyAlert());
                }
            }
            else
            {
                newAlert.transform.position = Vector2.MoveTowards(newAlert.transform.position, newPosAlert, 5 * Time.deltaTime);
            }
        }

    }
}
