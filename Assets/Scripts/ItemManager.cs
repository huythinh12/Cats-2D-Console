using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] doorsAuto;
    AutoDoor ad;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(RandomDoor());
    }
    IEnumerator RandomDoor()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            var rd = Random.Range(0, 2);
            ad = doorsAuto[rd].GetComponent<AutoDoor>();
            StartCoroutine(ad.RandomOpen());
            yield return new WaitUntil(() => ad.isTimeout);
        }
    }

}
