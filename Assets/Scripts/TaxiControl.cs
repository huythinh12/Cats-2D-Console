using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiControl : MonoBehaviour
{
    public GameObject Taxi;
    public GameObject[] Cars;

    private void OnEnable()
    {
        StartCoroutine(RandomSpanwTaxi());

    }
    IEnumerator RandomSpanwTaxi()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            {
                var rd = Random.Range(0, 6);
                if (Random.Range(0, 2) == Random.Range(0, 2))
                {
                    if (Cars[rd].transform.name.Contains("Car"))
                    {
                        var nextPos = new Vector3(transform.position.x, transform.position.y + (-0.5f), transform.position.z);
                        Instantiate(Cars[rd], nextPos, Quaternion.identity, transform);
                    }
                    else
                    {
                        var nextPos = new Vector3(transform.position.x, transform.position.y + (-0.1f), transform.position.z);
                        Instantiate(Cars[rd], nextPos, Quaternion.identity, transform);

                    }
                }
            }
        }
    }
    // Update is called once per frame

}
