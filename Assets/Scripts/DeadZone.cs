using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var obj = collision.transform.GetComponent<Player>();
            obj.isDead = true;
            if (obj.point >= 5)
                obj.point -= 5;
            else
            {
                obj.point = 0;
            }
        }
    }
}
