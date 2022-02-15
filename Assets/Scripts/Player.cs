using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    PlayerController controls;
    Rigidbody2D rb;
    Vector2 movement;
    Vector3 directionFlip;
    LayerMask magnetLayer;
    public Vector2 getPush = new Vector2(5, 2);
    public float movementSpeed = 2.5f;
    public float rotationSpeed = 50;
    public float direction;
    public float jump = 5f, forcePush = 1500f;
    public bool getFat;
    public bool isSameItem;
    public bool isColiderItem;
    public int point, state, stateMax = 100;
    public GameObject[] items;
    Transform childPoint, footPoint;
    Animator anim;
    CrossFade crossfade;
    bool isAlmostReady = false;
    bool isHit = false;
    bool isGround = false;
    bool isPush = false;
    bool isFull = false;
    bool isGrow = false;
    bool onClick = true;
    bool isActiveSpecial = false;
    bool isGetAvatar = false;
    bool isEndDecrease = false;
    bool onGround = false;
    bool isRuningsActiveSpecial = false;
    bool isRunningCoroutine = false;
    public string nameItem;
    public bool isDead = false, dying = false;
    public bool blooming = false, isbloom = false;
    bool invisible = false;
    public bool isExtra = false, isMinus = false;
    public bool givingPoint = false;
    PointInGame pointInGameP2, pointInGameP1;
    PointEffector2D magnet;
    [ColorUsage(true, true)]
    public Color myColor;
    Renderer r;
    List<Transform> listPosItem = new List<Transform>();
    HashSet<string> listNameItem = new HashSet<string>();
    public List<GameObject> listItemObj = new List<GameObject>();
    MaterialPropertyBlock _propBlock;
    GameObject avatar, objItem;
    SpriteRenderer playerColor;
    new Light2D light;
    public int dItem = 0;
    PlayerInput playerInput;
    IEnumerator degreeCoroutine;
    Coroutine saveCoroutine;
    private void Awake()
    {
        controls = new PlayerController();

        light = GetComponentInChildren<Light2D>();


    }
    private void OnEnable()
    {
        controls.PlayerControl.Enable();
    }
    private void OnDisable()
    {
        controls.PlayerControl.Disable();

    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (GameManager.isGameReady)
            if (!dying)
            {
                if (!isHit)
                    movement = ctx.ReadValue<Vector2>();
            }

    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (GameManager.isGameReady)
            //Check jump once 
            if (!dying)
            {
                if (!isHit)
                {
                    if (ctx.started && isGround)
                    {
                        if (!getFat)
                            rb.velocity = Vector2.up * jump;
                        else
                        {

                            rb.velocity = Vector2.up * (jump + 2);
                        }
                    }

                }
            }


    }
    public void OnPush(InputAction.CallbackContext ctx)
    {
        if (GameManager.isGameReady)
            if (!dying)
            {
                if (ctx.started)
                {
                    isPush = true;
                    if (!blooming)
                        anim.SetBool("isPush", isPush);
                    if (isFull && onClick)
                        StartCoroutine(WaitToGrowAgain());
                }
                else if (ctx.performed)
                {
                    isPush = false;
                    anim.SetBool("isPush", isPush);
                }
                else
                {
                    isPush = false;
                    anim.SetBool("isPush", isPush);
                }
            }

    }
    IEnumerator TurnOnBloom()
    {
        blooming = true;
        // 1s = 50 recall .
        float inTime = 0.65f / 50;
        while (light.pointLightOuterRadius < 0.65f)
        {
            light.pointLightOuterRadius += inTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        magnet.enabled = true;
        _propBlock.SetFloat("_Thicc", 0.9f);
        r.SetPropertyBlock(_propBlock);
        yield return new WaitForSeconds(0.5f);
        while (light.pointLightOuterRadius > 0f)
        {
            light.pointLightOuterRadius -= inTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        blooming = false;
        StartCoroutine(DecreaseInEverySecond());
        yield return new WaitUntil(() => !isActiveSpecial);
        isEndDecrease = false;
        StartCoroutine(TurnOffBloom());
    }

    IEnumerator TurnOffBloom()
    {
        blooming = true;
        // 1frame = 50 recall .
        float inTime = 0.65f / 50;
        while (light.pointLightOuterRadius < 0.65f)
        {
            light.pointLightOuterRadius += inTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        _propBlock.SetFloat("_Thicc", 0f);
        r.SetPropertyBlock(_propBlock);
        yield return new WaitForSeconds(0.5f);
        while (light.pointLightOuterRadius > 0f)
        {
            light.pointLightOuterRadius -= inTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        light.pointLightOuterRadius = 0;
        blooming = false;
        isbloom = false;
        magnet.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.enabled = true;
            //thay the onsceneloaded de test *
            pointInGameP2 = GameObject.Find("PointP2").GetComponentInChildren<PointInGame>();
            pointInGameP1 = GameObject.Find("PointP1").GetComponentInChildren<PointInGame>();
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        r = GetComponent<Renderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("item"))
                listPosItem.Add(transform.GetChild(i));
        }
        //get child transform
        childPoint = transform.GetChild(1);
        footPoint = transform.GetChild(7);
        magnetLayer = LayerMask.GetMask("Magnet");

        point = 0;
        state = 99;
        stateMax = 100;
        playerColor = GetComponent<SpriteRenderer>();
        //get color from plus point and create anim when get point 
        //plus = transform.Find("Plus");
        //plus.GetComponent<SpriteRenderer>().color = Color.clear;
        //StartCoroutine(PlusAnim());


        _propBlock = new MaterialPropertyBlock();

        magnet = gameObject.GetComponentInChildren<PointEffector2D>();
        StartCoroutine(DegreeState());
        StartCoroutine(CheckGetItem());
    }

    IEnumerator PlusAnim()
    {
        yield return new WaitForSeconds(1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {


        //when start begin starting point
        if (scene.buildIndex > 2)
        {
            pointInGameP2 = GameObject.Find("PointP2").GetComponentInChildren<PointInGame>();
            pointInGameP1 = GameObject.Find("PointP1").GetComponentInChildren<PointInGame>();
        }
        //if (scene.name == "Hungry")
        //{
        //    print("dung r hungry");
        //}
        //else
        //{
        //    print("ko phai hungry");
        //}
    }
    IEnumerator DegreeState()
    {
        yield return new WaitUntil(() => GameManager.isGameReady);

        while (true)
        {
            yield return null;

            if (state > 0)
            {
                isRunningCoroutine = false;
                saveCoroutine = StartCoroutine(DecreaseInEverySecond());
                yield return new WaitUntil(() => isEndDecrease);
                isEndDecrease = false;
                state--;
            }
            else
            {

                if (!isRunningCoroutine)
                {
                    isRunningCoroutine = true;
                    StopCoroutine(saveCoroutine);
                }
                yield return null;
                if (point > 0 && state <= 0)
                {
                    StartCoroutine(DecreaseInEverySecond());
                    yield return new WaitUntil(() => isEndDecrease);
                    isEndDecrease = false;
                    point--;
                }


            }

        }
    }

    IEnumerator Respawn(GameObject player)
    {
        isDead = false;
        invisible = true;
        dying = true;
        isHit = false;
        anim.SetBool("isGetHit", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isGetHit", false);
        playerColor.color = Color.clear;
        yield return new WaitForSeconds(2);
        player.transform.position = player.transform.parent.position;
        anim.SetBool("isPush", false);
        dying = false;
        byte x = 85;
        var d = 0;
        while (d < 3)
        {
            playerColor.color = new Color32(255, 255, 255, x);
            yield return new WaitForSeconds(1);
            x += 85;
            d++;
        }
        invisible = false;

    }
    private void ActiveInTime()
    {
        CheckSceneToActiveSpecial(SceneManager.GetActiveScene().name);

        if (transform.parent.name == "Player2")
        {
            if (pointInGameP2)
                pointInGameP2.SetStatus(2);
        }
        else
        {
            if (pointInGameP1)
                pointInGameP1.SetStatus(2);
        }
        StopCoroutine(saveCoroutine);
        point += 10;
        state = 50;
        isActiveSpecial = true;

        if (isbloom)
        {
            StartCoroutine(TurnOnBloom());
        }
        else
        {
            StartCoroutine(DecreaseInEverySecond());
        }
        //yield return new WaitUntil(() => isEndDecrease);
    }

    private void CheckSceneToActiveSpecial(string name)
    {
        if (name == "Sunsine")
        {
            isbloom = true;
        }
        else if (name == "Hungry")
        {
            getFat = true;
        }
    }

    IEnumerator DecreaseInEverySecond()
    {
        float currentValue = 2;
        float totalSecond;
        if (isActiveSpecial)
        {
            totalSecond = 12;
        }
        else
        {
            totalSecond = 3;
        }
        float valuePerTime = (currentValue / totalSecond);
        valuePerTime *= Time.fixedDeltaTime;

        while (currentValue >= 0)
        {
            yield return new WaitForFixedUpdate();

            if (givingPoint)
            {
                if (!getFat || !isbloom)
                {
                    currentValue = 2;
                    givingPoint = false;
                }
            }

            SetCurrentValue(ref currentValue, valuePerTime);
        }
        isEndDecrease = true;
        if (isActiveSpecial)
        {
            isActiveSpecial = false;
            isRuningsActiveSpecial = false;
            getFat = false;
        }
        //yield return null;
        SetCurrentValue(ref currentValue, -2);

    }

    private void SetCurrentValue(ref float currentValue, float nextValue)
    {
        if (transform.parent.name == "Player2")
        {
            if (pointInGameP2)
            {
                pointInGameP2.SetStatus(currentValue -= nextValue);
            }
        }
        else
        {
            if (pointInGameP1)
            {
                pointInGameP1.SetStatus(currentValue -= nextValue);
            }
        }

    }
    private void ActiveItemInList()
    {
        bool isSpeedChange = false;
        foreach (var item in listItemObj)
        {
            if (item.name.Contains("extra"))
            {
                isExtra = true;
            }

            else if (item.name.Contains("minus"))
            {
                isMinus = true;
            }
            else if (isSpeedChange)
            {
                movementSpeed = 3;
            }
            else if (item.name.Contains("fast") && !isSpeedChange)
            {
                isSpeedChange = true;
                movementSpeed = 4.5f;
            }
            else if (item.name.Contains("slow") && !isSpeedChange)
            {
                isSpeedChange = true;
                movementSpeed = 1.5f;
            }


        }
    }

    void Update()
    {
        //checkstack-item-onhead
        if (GameManager.isGameReady)
        {
            if (listItemObj.Count > 0)
            {
                ActiveItemInList();
                CheckForStackItem();
            }
            //state for getfat
            CheckStateMax();
            if (dying)
            {
                rb.drag = 1000;
            }
            else
            {
                rb.drag = 0;
            }

            if (isDead)
            {
                StartCoroutine(Respawn(gameObject));
            }
            if (transform.parent.name == "Player2")
            {
                if (transform.name.Contains("Si"))
                {
                    if (!isGetAvatar)
                    {
                        avatar = GameObject.Find("Si_Avatar");
                        isGetAvatar = true;
                    }
                }
                else
                {
                    if (!isGetAvatar)
                    {
                        avatar = GameObject.Find("La_Avatar");
                        isGetAvatar = true;
                    }
                }

                if (pointInGameP2 && !dying)
                {
                    pointInGameP2.SetState(state);
                    pointInGameP2.SetPoint(point);
                }
            }
            else
            {
                if (transform.name.Contains("Si"))
                {
                    if (!isGetAvatar)
                    {
                        avatar = GameObject.Find("Si_Avatar");
                        isGetAvatar = true;
                    }
                }
                else
                {
                    if (!isGetAvatar)
                    {
                        avatar = GameObject.Find("La_Avatar");
                        isGetAvatar = true;
                    }
                }
                if (pointInGameP1 && !dying)
                {
                    pointInGameP1.SetState(state);
                    pointInGameP1.SetPoint(point);
                }
            }
            CheckStateForAvatar();
            CheckRayCastToPush();
            if (footPoint)
            {
                PreventJumpInfinite();
            }
            //make movement for player
            float move = movement.x * movementSpeed;
            MakeMoveAndGetHit(move);
            //Set animation to action 
            if (!dying)
            {
                anim.SetFloat("yJump", rb.velocity.y);
                anim.SetFloat("Move", Mathf.Abs(move));
            }
            //flip object when move right or left
            FlipMove(move);
        }
        else if(!GameManager.isGameReady && SceneManager.GetActiveScene().buildIndex > 2)
        {
            //end time and stop action character make it right way
            if(rb.velocity.y == 0)
            {
                rb.drag = 1000;
            }
            anim.SetBool("isPush", false);
            anim.SetFloat("yJump", rb.velocity.y);
            anim.SetBool("isJump", false);
            anim.SetFloat("Move", 0);

        }

    }
    private void CheckStateForAvatar()
    {
        if (state < 50)
        {
            avatar.GetComponent<Animator>().SetBool("isHappy", false);
        }
        else if (state >= 50 && !isActiveSpecial)
        {
            avatar.GetComponent<Animator>().SetBool("isHappy", true);
            avatar.GetComponent<Animator>().SetBool("isSunny", false);
        }
        else if (isActiveSpecial)
        {
            avatar.GetComponent<Animator>().SetBool("isSunny", true);
        }
    }
    private void CheckStateMax()
    {
        if (state == stateMax)
        {
            //getFat = true;*
            if (!isRuningsActiveSpecial)
            {
                isRuningsActiveSpecial = true;
                ActiveInTime();
            }
        }
    }

    IEnumerator WaitToGrowAgain()
    {
        onClick = false;
        isGrow = true;
        anim.SetBool("isGrow", true);
        transform.localScale = new Vector2(1.3f, 1.3f);
        yield return new WaitForSeconds(1.5f);
        isGrow = false;
        transform.localScale = new Vector2(1f, 1f);
        anim.SetBool("isGrow", false);
        yield return new WaitForSeconds(2.5f);
        onClick = true;
    }

    private void MakeMoveAndGetHit(float move)
    {
        //check if player get hit and stop move 
        if (!isHit)
        {
            //check if fat and change behavior move 
            CheckFat(move);
            rb.velocity = new Vector2(move, rb.velocity.y);
        }
        else
        {
            // when get hit set animation get hit
            anim.SetBool("isJump", false);
            StartCoroutine(WaitForAnimationFinish());
        }
    }

    public IEnumerator WaitForAnimationFinish()
    {
        isHit = true;
        anim.SetBool("isGetHit", true);
        yield return new WaitUntil(() => !isHit);
        anim.SetBool("isGetHit", false);
    }

    private void CheckFat(float move)
    {
        if (getFat)
        {
            isFull = true;
            isPush = false;
            Rolling(move);
            rb.constraints = RigidbodyConstraints2D.None;
            anim.SetBool("isFull", true);
        }
        else
        {
            //when is not fat reset all to normal
            isFull = false;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("isFull", false);
        }
    }

    private void Rolling(float move)
    {
        if (move > 0)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime);
        }
        else if (move < 0)
        {
            transform.Rotate(Vector3.back * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.zero);
        }
    }

    private bool CheckRayCastToPush()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(childPoint.position, childPoint.right * direction, 0.05f);
        if (hitInfo)
        {
            if (hitInfo.transform.tag == "Player")
                return true;
        }
        return false;
    }

    private void FlipMove(float move)
    {
        if (move < 0)//move left
        {
            direction = -1;
            directionFlip = new Vector3(direction, 1, 1);
            transform.localScale = directionFlip;

        }
        else if (move > 0)//move rigth
        {
            direction = 1;
            directionFlip = new Vector3(direction, 1, 1);
            transform.localScale = directionFlip;
        }
    }

    private void PreventJumpInfinite()
    {

        if (!getFat)
        {
            if (Physics2D.Raycast(footPoint.position, Vector3.down, 0.1f, ~magnetLayer))
            {
                anim.SetBool("isJump", false);
                isGround = true;
            }
            else
            {
                anim.SetBool("isJump", true);
                isGround = false;
            }
        }

    }

    public void GetHitCompleted()
    {
        isHit = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Player>();
        var enemyRigid = collision.gameObject.GetComponent<Rigidbody2D>();
        //check when fat onGround 


        //action hit when two player collider
        if (collision.gameObject.tag == "Player")
        {
            if (isPush && CheckRayCastToPush() && !enemy.isFull && !enemy.isbloom && !enemy.invisible)

            {
                enemy.isHit = true;
                //get push from another side 
                getPush.x *= direction;
                enemyRigid.velocity = getPush;
            }
            else if (isbloom && !enemy.isbloom && !enemy.invisible)
            {
                GetHitWithSide(collision, enemy, enemyRigid, 4.5f, 3);
            }
            else if (isGrow)
            {
                GetHitWithSide(collision, enemy, enemyRigid, 6, 5);

                //enemy.isHit = true;
                ////get push from another side with each fat cat 
                //var posN = collision.contacts[0].normal;
                //float x, y;
                ////set to horizon value
                //if (posN.x > 0)
                //    x = -1;
                //else if (posN.x < 0)
                //    x = 1;
                //else
                //    x = Random.Range(-1, 1);
                ////set to vertical value
                //if (enemy.onGround || enemy.isGround)
                //    y = 1;
                //else if (posN.y < 0)
                //    y = 1;
                //else if (posN.y > 0)
                //    y = -1;
                //else
                //    y = Random.Range(-1, 1);
                //enemyRigid.velocity = new Vector2(6 * x, 5 * y);
            }

        }

    }

    private static void GetHitWithSide(Collision2D collision, Player enemy, Rigidbody2D enemyRigid, float vecX, float vecY)
    {
        var posN = collision.contacts[0].normal;

        enemy.isHit = true;
        float x, y;
        //set to horizon value
        if (posN.x > 0)
            x = -1;
        else if (posN.x < 0)
            x = 1;
        else
            x = Random.Range(-1, 1);
        //set to vertical value
        if (enemy.onGround || enemy.isGround)
            y = 1;
        else if (posN.y < 0)
            y = 1;
        else if (posN.y > 0)
            y = -1;
        else
            y = Random.Range(-1, 1);
        enemyRigid.velocity = new Vector2(vecX * x, vecY * y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        }
        //reset value to action hit and get push
        isHit = false;
        getPush.x = Mathf.Abs(getPush.x);

        if (getFat)
        {
            if (collision.collider.tag == "Ground")
            {
                isGround = true;
            }

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb.sleepMode = RigidbodySleepMode2D.StartAwake;
        }
        if (getFat)
        {
            if (collision.collider.tag == "Ground")
            {
                isGround = false;
            }
        }

    }
    IEnumerator CheckGetItem()
    {
        while (true)
        {

            yield return null;
            if (nameItem.Contains("extra"))
            {

                GetItem("extra");

            }
            else if (nameItem.Contains("random"))
            {
                GetItem("random");
                //ActiveRandomItem();
            }
            else if (nameItem.Contains("fast"))
            {
                GetItem("fast");
            }
            else if (nameItem.Contains("minus"))
            {
                GetItem("minus");

            }
            else if (nameItem.Contains("slow"))
            {
                GetItem("slow");

            }
        }
    }
    private void GetItem(string nameInListItem)
    {
        var nextSize = new Vector3(0.5f, 0.5f, 0);
        foreach (var item in items)
        {
            if (item.name.Contains(nameInListItem))
            {
                if (listNameItem.Add(nameInListItem))
                {
                    GameObject objectClone = CreateOnHeadItem(nextSize, item);
                    objectClone.tag = "HeadItem";
                    break;
                }
                else if (!listNameItem.Add(nameInListItem) && isColiderItem)
                {
                    foreach (var iTem in listItemObj)
                    {
                        if (iTem.name.Contains(nameInListItem))
                        {
                            //iTem.name = " coocococo";
                            listItemObj.Remove(iTem);
                            Destroy(iTem);
                            break;
                        }
                    }
                    objItem = CreateOnHeadItem(nextSize, item);
                    objItem.tag = "HeadItem";
                    break;

                }
            }

        }

    }

    private GameObject CreateOnHeadItem(Vector3 nextSize, GameObject item)
    {
        var objectClone = Instantiate(item, transform.position, Quaternion.identity, transform);
        listItemObj.Add(objectClone);
        objectClone.GetComponent<Animator>().enabled = false;
        objectClone.transform.localScale = nextSize;
        MoveItemOnHead(objectClone);
        return objectClone;
    }

    private void MoveItemOnHead(GameObject objectClone)
    {

        if (listItemObj.Count == 1)
        {
            objectClone.transform.position = listPosItem[0].position;
        }
        else if (listItemObj.Count == 2)
        {
            objectClone.transform.position = listPosItem[1].position;

        }
        else if (listItemObj.Count == 3)
        {
            objectClone.transform.position = listPosItem[2].position;

        }
        else if (listItemObj.Count == 4)
        {
            objectClone.transform.position = listPosItem[3].position;

        }
        else if (listItemObj.Count == 5)
        {
            objectClone.transform.position = listPosItem[4].position;

        }
    }
    private void CheckForStackItem()
    {
        for (int i = 0; i < listItemObj.Count; i++)
        {
            listItemObj[i].transform.position = listPosItem[i].position;
        }
    }


}
