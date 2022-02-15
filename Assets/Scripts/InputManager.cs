using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    bool stopButton = true;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (stopButton)
            if (GameManager.isLoadScene)
            {
                GetComponent<PlayerInputManager>().enabled = false;
                stopButton = false;
            }
    }
}
