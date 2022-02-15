using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRatcast : MonoBehaviour
{
    Transform child;
    LayerMask magnet;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        magnet = LayerMask.GetMask("Magnet");

    }

    // Update is called once per frame
    void Update()
    {
        //if (child)
        //{
        //    Debug.DrawLine(child.position, new Vector2(child.position.x, child.position.y - 0.3f), Color.red);
        //    RaycastHit2D hitInfo = Physics2D.Raycast(child.position, Vector3.down, 0.2f,~magnet);
        //    if (hitInfo.transform)
        //        print(hitInfo.transform.name + " hit info from child foot");
        //}
          
    }
}
