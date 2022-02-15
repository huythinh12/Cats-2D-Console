using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlStart : MonoBehaviour
{
    public List<GameObject> arrChild = new List<GameObject>();
    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in arrChild)
        {
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
            if (!isRunning && GameManager.isGameReady)
            {
                isRunning = true;
                foreach (var item in arrChild)
                {
                    item.gameObject.SetActive(true);
                }
            }
        
    }
}
