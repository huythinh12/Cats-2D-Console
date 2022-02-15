using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydrant : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            anim.SetBool("isWaterFire", true);
        }
    }
    public void EndWaterFire()
    {
        anim.SetBool("isWaterFire", false);

    }
    // Update is called once per frame
  
}
