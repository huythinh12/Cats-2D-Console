using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test class
public class ButtonNext : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update

    public void Next(string name)
    {
        gm.NextScene(name);
    }
    // Update is called once per frame
    void Update()
    {
      gm=GameObject.FindObjectOfType<GameManager>();
        
    }
}
