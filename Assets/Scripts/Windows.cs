using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windows : MonoBehaviour
{
    public bool isOpen = false;
    BoxCollider2D doorCollider;
    Animator anim;
    public GameObject speakerHit;
    bool isOpenCompleted = false;
    public bool handleWindow;
    Vector3 speakerPos;
    IEnumerator handleWinCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        doorCollider = gameObject.GetComponent<BoxCollider2D>();
        handleWindow = false;
        speakerPos = new Vector3(transform.position.x, transform.position.y + 0.6f,transform.position.z);
    }
    IEnumerator OpenWindow()
    {
        handleWindow = false;
        yield return new WaitUntil(() => doorCollider);
        anim.SetBool("isOpenWindow", true);
        yield return new WaitUntil(() => isOpenCompleted);
        var speakerObject= Instantiate(speakerHit, speakerPos, Quaternion.identity,transform);
        yield return new WaitForSeconds(0.5f);
        doorCollider.isTrigger = true;
        Destroy(speakerObject);
        yield return new WaitUntil(() => !speakerObject);
        yield return new WaitForSeconds(0.5f);
        doorCollider.isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
           
        if(collision.transform.tag == "Player")
        {
            //if(collision.transform.)
            Invoke("HandleWindow",Random.Range(0.5f,1.6f));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            CancelInvoke("HandleWindow");
        }
    }
    private void HandleWindow()
    {
        handleWindow = true;
    }
    public void OpenCompleted()
    {
        isOpenCompleted = true;
    }
    public void CloseWindow()
    {
        anim.SetBool("isOpenWindow", false);
        isOpenCompleted = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (handleWindow)
        {
            StartCoroutine(OpenWindow());
        }
    }
}
