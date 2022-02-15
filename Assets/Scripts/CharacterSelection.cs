using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;
    public Character[] character;

    PlayerController controls;
    PlayerInput inputName;
    GameObject obj;
    GameManager gm;
    Vector3 playerpos;

    bool isReady = false,
         isSelectRight = false,
         isSelectLeft = false,
         isStart = false;

    int indexCharacter = 0;
    private void Awake()
    {
        controls = new PlayerController();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        controls.PlayerControl.Enable();
    }
    public void OnNext(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            isSelectRight = true;
        }
    }
    public void OnPrev(InputAction.CallbackContext ctx)
    {

        if (ctx.started)
        {
           isSelectLeft = true;
        }
    }
    public void OnStart(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            //check start button double press
            isStart = !isStart;
            if (isStart)
                gm.playerReady++;
            else
                gm.playerReady--;
        }
    }
    private void OnDisable()
    {
        controls.PlayerControl.Disable();

    }
    // Start is called before the first frame update
    void Start()
    {
        inputName = FindObjectOfType<PlayerInput>();
        gm = FindObjectOfType<GameManager>();

        gm.inputDevice.Add(inputName.currentControlScheme);

        gm.countPlayerJoined++;

        //direction position for spawn each player  
        if (gm.countPlayerJoined == 1)
        {
            playerpos = transform.position;
        }
        else if (gm.countPlayerJoined == 2)
        {
            playerpos = new Vector3(4.78f, -0.99f, -1);
        }

        //set default after player join
        for (int i = 0; i < character.Length; i++)
        {
            var nameUI = character[i].player.GetComponentInChildren<Text>();
            nameUI.text = character[i].getName;
            //set playerinput false
            character[i].player.GetComponent<PlayerInput>().enabled = false;
        }

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //check all player ready 
        if (gm.playerReady==2 && !isReady)
        {
            isReady = true;
            obj.GetComponent<PlayerInput>().enabled = true;
            gm.listCharacterSelected.Add(obj);
            StartCoroutine(WaitTime());
        }

        if (isReady) return;

        //next and right button to select character
        if (isSelectRight)
        {
            Next();
        }
        else if (isSelectLeft)
        {
            Prev();
        }
        isSelectRight = false;
        isSelectLeft = false;
    }
    //wait Fadeout and make to player display disappeared
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        obj = Instantiate(character[0].player, playerpos, Quaternion.identity);
        obj.transform.parent = transform;
    }

    public void Next()
    {
        Destroy(obj);
        indexCharacter = (indexCharacter + 1) % character.Length;
        obj = Instantiate(character[indexCharacter].player, playerpos, Quaternion.identity);
        obj.transform.parent = transform;
    }

    public void Prev()
    {
        Destroy(obj);
        indexCharacter--;
        if (indexCharacter < 0)
        {
            indexCharacter += character.Length;
        }
        obj = Instantiate(character[indexCharacter].player, playerpos, Quaternion.identity);
        obj.transform.parent = transform;
    }
}
