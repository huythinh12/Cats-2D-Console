using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingMenu : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    public void NextScene(string name)
    {
        gm.NextScene(name);
    }
    
    // Update is called once per frame
}
