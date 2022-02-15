using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsControl : MonoBehaviour
{
    Windows[] windows;
    // Start is called before the first frame update
    void Start()
    {
        windows = GetComponentsInChildren<Windows>();
        StartCoroutine(RandomWindow());
    }
    IEnumerator RandomWindow()
    {
        yield return new WaitUntil(() => windows.Length>0);
        while (true)
        {
        yield return new WaitForSeconds(Random.Range(1,3));
            foreach (var item in windows)
            {
                if(Random.Range(0,3) == Random.Range(0,3))
                     item.handleWindow =true;
            }
        }
    }
    // Update is called once per frame
}
