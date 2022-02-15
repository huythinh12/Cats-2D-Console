using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    Animator anim;
    int touchTime,restoreTouchTime; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        restoreTouchTime = touchTime = 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //anim.enabled = true;
        anim.SetBool("isTouch", true);

        touchTime--;
        if (touchTime <= 0)
        {
            anim.SetBool("isBreak", true);
            StartCoroutine(RestoreState());
            touchTime = restoreTouchTime;
        }

    }
    IEnumerator RestoreState()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("isBreak", false);
        yield return new WaitForSeconds(2);
        anim.SetBool("isIdle", true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("isTouch", false);
        touchTime--;
        if (touchTime <= 0)
        {
            anim.SetBool("isBreak", true);
            StartCoroutine(RestoreState());
            touchTime = restoreTouchTime;

        }
    }
    // Update is called once per frame
   
}
