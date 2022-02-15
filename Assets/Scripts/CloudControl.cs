using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControl : MonoBehaviour
{
    public GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpanwCloud());
    }
    IEnumerator RandomSpanwCloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            {
                if (Random.Range(0, 2) == Random.Range(0, 2))
                {
                        var nextPos = new Vector3(transform.position.x, transform.position.y + (Random.Range(-1,3)), transform.position.z);
                        Instantiate(cloud, nextPos, Quaternion.identity, transform);
                }
            }
        }
    }
}
