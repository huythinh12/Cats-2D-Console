using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    Vector3 nextPos;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TimeLife());
    }
    void Update()
    {
        if (transform.parent.name == "CloudLeft")
        {
            nextPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 1f * Time.deltaTime);
        }
        else if (transform.parent.name == "CloudRight")
        {
            var flip = new Vector3(-1, 1, 1);
            transform.localScale = flip;
            nextPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 1f * Time.deltaTime);
        }
    }
    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(25);
        Destroy(gameObject);
    }
}
