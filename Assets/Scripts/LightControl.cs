using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    Light2D light;
    public Vector3 nextPos;
    public Transform rayLeft, rayRight;
    float rightX, leftX;
    float rotateZ = -164.428f;
    float rotateZNew = -176.1f;
    Vector2 randX = new Vector2();
    Vector3 defaultPos;
    Quaternion defaultRotate = new Quaternion();
    GameObject Player1, Player2;
    Transform charP1, charP2;
    List<string> bodyInsides1, bodyInsides2;
    Transform characterSave1, characterSave2;
    public bool isLightEnd;
    bool ishandleOn, isMove, isInsideLight;
    bool lightOut = true;
    bool isStopCoroutine = false;
    Coroutine CGivePoint;
    Player player2, player1;
    private void Awake()
    {
        light = GetComponent<Light2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        light.pointLightOuterAngle = 6;
        light.intensity = 0;
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        //set default rotate and position
        //defaultRotate.eulerAngles = new Vector3(0, 0, rotateZ);
        //transform.rotation = defaultRotate;
        //transform.position = new Vector3(0, 12.8f, 0);
        defaultPos = transform.position;
    }
    private void OnEnable()
    {
        ishandleOn = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameReady && Player1)
        {
            if (Player1.transform.childCount != 0)
            {
                rightX = rayRight.position.x;
                leftX = rayLeft.position.x;
                //check to run func one time per light on
                if (ishandleOn)
                {
                    StartCoroutine(ChangeRangeLight(light.pointLightOuterAngle));
                    CGivePoint = StartCoroutine(GivePoint());
                }

                if (!lightOut && transform.name != "PointLight" && isMove)
                {
                    var speed = 5f;
                    transform.position = Vector3.MoveTowards(transform.position, randX, speed * Time.deltaTime);
                }
                if (Player2.transform.childCount > 0)
                {
                    //get transform char1 and char2 
                    charP1 = Player1.transform.GetChild(0);
                    charP2 = Player2.transform.GetChild(0);

                    //get angle value from parent up to left and right
                    var angleLeft = Vector2.Angle(transform.parent.right, rayLeft.position - transform.position);
                    var angleRight = Vector2.Angle(transform.parent.right, rayRight.position - transform.position);

                    if (!lightOut)
                    {
                        CheckBodyInside(charP1, angleLeft, angleRight);
                        CheckBodyInside(charP2, angleLeft, angleRight);
                    }
                }
            }

        }
    }
    void CheckBodyInside(Transform character, float angleLeft, float angleRight)
    {
        bool isP1 = false;
        if (character.parent.name == "Player1")
        {
            isP1 = true;
            characterSave1 = character;
            bodyInsides1 = new List<string>();
        }
        else
        {
            characterSave2 = character;
            bodyInsides2 = new List<string>();
        }
        var getChildBody = character.Find("Body");
        if (getChildBody)
        {
            foreach (Transform item in getChildBody.transform)
            {
                var angleItem = Vector2.Angle(transform.parent.right, item.position - transform.position);
                if (IsBetween(angleItem, angleRight, angleLeft))
                {
                    if (isP1)
                    {
                        bodyInsides1.Add(item.name);
                    }
                    else
                    {
                        bodyInsides2.Add(item.name);
                    }
                }
            }
        }
    }

    IEnumerator GivePoint()
    {
        while (true)
        {
            yield return null;
            if (!lightOut)
            {
                yield return new WaitForSeconds(0.7f);
                player1 = characterSave1.GetComponent<Player>();
                player2 = characterSave2.GetComponent<Player>();
                GivePointToPlayer(player1, bodyInsides1);
                GivePointToPlayer(player2, bodyInsides2);
                isStopCoroutine = true;
            }
            else if (lightOut && isStopCoroutine)
            {
                if (player2)
                {
                    player2.givingPoint = false;
                }
                if (player1)
                {
                    player1.givingPoint = false;
                }
                StopCoroutine(CGivePoint);
                isStopCoroutine = false;
            }
        }
    }

    void GivePointToPlayer(Player player, List<string> bodyInsides)
    {
        if (bodyInsides.Count == 2 && !player.isbloom && !player.dying)
        {
            player.givingPoint = true;
            player.state++;
            //if (player.state == player.stateMax)
            //{
            //    player.ActiveBloom();
            //    player.point += 10;
            //    player.state = 50;
            //}
        }
        else
        {
            player.givingPoint = false;
        }

    }

    bool IsBetween(float value, float bound1, float bound2)
    {
        return (value < Mathf.Max(bound1, bound2) && value > Mathf.Min(bound1, bound2));
    }
    IEnumerator ChangeRangeLight(float currentLightAngle)
    {
        ishandleOn = false;

        yield return null;
        isMove = Random.Range(0, 2) == 1;//random move or not

        if (transform.name == "PointLight")
        {
            defaultRotate.eulerAngles = new Vector3(0, 0, Random.Range(-148, -212));

        }
        else if (transform.name == "PointLightLeft")
        {
            if (isMove)
            {
                randX = new Vector2(Random.Range(transform.position.x, 0), transform.position.y);
            }
            defaultRotate.eulerAngles = new Vector3(0, 0, Random.Range(-117, -168));
        }
        else if (transform.name == "PointLightRight")
        {
            if (isMove)
            {
                randX = new Vector2(Random.Range(transform.position.x, 0), transform.position.y);
            }
            defaultRotate.eulerAngles = new Vector3(0, 0, Random.Range(-193, -244));
        }


        var nextRadius = Random.Range(5, 15);
        light.pointLightOuterAngle = nextRadius;
        if (light.pointLightInnerAngle > 0)
        {
            light.pointLightInnerAngle = light.pointLightOuterAngle - 0.3f;
        }
        var step = CountStepRadius(currentLightAngle, nextRadius);
        var newRayright = Nextposition("rayRight", rightX, 0.1731f, step, nextRadius, currentLightAngle);
        var newRayleft = Nextposition("rayLeft", leftX, 0.164f, step, nextRadius, currentLightAngle);
        rayLeft.position = newRayleft;
        rayRight.position = newRayright;
        transform.rotation = defaultRotate;

        StartCoroutine(LightIn());

    }
    IEnumerator LightOut()
    {
        yield return new WaitForSeconds(Random.Range(4, 5));
        float intensity = 2;
        float countTime = 2 / 125f;
        while (light.intensity >= 0)
        {
            intensity -= countTime;
            light.intensity = intensity;
            if (intensity < 0.7)
            {
                lightOut = true;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        transform.position = defaultPos;
        ishandleOn = true;
        isLightEnd = true;//check to ParrentLightControl.cs

    }
    IEnumerator LightIn()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        float intensity = 0;
        float countTime = 2 / 175f;
        while (light.intensity <= 2)
        {
            intensity += countTime;
            light.intensity = intensity;
            if (intensity > 0.8)
            {
                lightOut = false;
            }
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(LightOut());
    }
    float CountStepRadius(float currentLightAngle, float nextLightAngle)
    {
        return Mathf.Max(currentLightAngle, nextLightAngle) - Mathf.Min(currentLightAngle, nextLightAngle);
    }

    Vector3 Nextposition(string rayName, float x, float currentOffset, float step, float nextRadius, float currentLightAngle)
    {
        for (int i = 0; i < step; i++)
        {
            if (rayName == "rayRight")
            {
                if (nextRadius > currentLightAngle)
                {
                    currentOffset += 0.001f;
                    x += currentOffset;
                }
                else if (nextRadius < currentLightAngle)
                {
                    currentOffset -= 0.001f;
                    x -= currentOffset;
                }
            }
            else
            {
                if (nextRadius > currentLightAngle)
                {
                    currentOffset -= 0.0006f;
                    x -= currentOffset;
                }
                else if (nextRadius < currentLightAngle)
                {
                    currentOffset += 0.0006f;
                    x += currentOffset;
                }

            }
        }
        //convert between left or right ray
        float rayY, rayZ;
        if (rayName == "rayRight")
        {
            rayY = rayRight.position.y;
            rayZ = rayRight.position.z;
        }
        else
        {
            rayY = rayLeft.position.y;
            rayZ = rayLeft.position.z;
        }
        return new Vector3(x, rayY, rayZ);
    }
}
