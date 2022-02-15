using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrentLightControl : MonoBehaviour
{
    public List<GameObject> lights;
    bool isHandleOn = true;
    int rd;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {

        if (isHandleOn)
        {
            StartCoroutine(RandomLight());
        }
    }
 
    IEnumerator RandomLight()
    {
        isHandleOn = false;
        rd = Random.Range(1,3);
        foreach (var item in lights)
        {
            item.SetActive(false);
        }
        lights[rd].SetActive(true);
        var lightObj = lights[rd].GetComponent<LightControl>();
        lightObj.isLightEnd = false;
        bool isEnd = false;

        //wait until light end and go to next random
        while (!lightObj.isLightEnd)
        {
            isEnd = lights[rd].GetComponent<LightControl>().isLightEnd;
            lightObj.isLightEnd = isEnd;
            yield return null;
        }
        isHandleOn = true;

    }
}
