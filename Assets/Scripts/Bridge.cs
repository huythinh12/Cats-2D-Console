using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    Vector3 nextPos, nextPosV, currentPos;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        var y = currentPos.y;
        nextPos = new Vector3(transform.position.x, -6, transform.position.z);
        nextPosV = new Vector3(transform.position.x, y-= 0.09f, transform.position.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        var saveC = StartCoroutine(Vibrate());
        yield return new WaitForSeconds(1);
        StopCoroutine(saveC);
        while (transform.position != nextPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 5 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Vibrate()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(currentPos, nextPosV,5* Time.deltaTime);
            yield return null;
        }

    }
    // Update is called once per frame
   
}
