using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayerTitleController : MonoBehaviour
{
    public GameObject title1, title2 ,buttonTitleJoin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance != null)
        {
            if(GameManager.instance.countPlayerJoined == 1)
            {
                buttonTitleJoin.gameObject.SetActive(false);
                title1.gameObject.SetActive(false);
            }
            else if(GameManager.instance.countPlayerJoined == 2)
            {
                title2.gameObject.SetActive(false);
            }
        }
    }
}
