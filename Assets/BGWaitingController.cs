using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGWaitingController : MonoBehaviour
{
    public GameObject BGWaiting, BGWaiting1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BGWaiting.transform.Translate(Vector3.left * Time.deltaTime);
        BGWaiting1.transform.Translate(Vector3.left * Time.deltaTime);
        if (BGWaiting.transform.position.x <= -20f)
        {
            BGWaiting.transform.position = new Vector2(15f, BGWaiting.transform.position.y);
        }
        if (BGWaiting1.transform.position.x <= -19f)
        {
            BGWaiting1.transform.position = new Vector2(15f, BGWaiting1.transform.position.y);

        }
    }
}
