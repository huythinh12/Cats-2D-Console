using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public GameObject nextbutton, prevbutton;
    public static bool isLoadScene = false, isTimeOut = false, isGameReady = false;
    public int playerReady;
    public string nameScene;
    public int winRatePlayer1, winRatePlayer2, draw, countPlayerJoined;
    SoundManager soundManager;
    public List<GameObject> listCharacterSelected;
    public List<string> inputDevice;
    public int minute, second;
    public static int min, sec;
    int timeDefault;
    bool isCheckStart = false;
    GameObject Player1, Player2;
    private void Awake()
    {

        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

    }

    // Start is called before the first frame update
    private void Start()
    {
        min = minute;
        sec = second;
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //do stuff
        if (scene.buildIndex > 2)
        {
            Player1 = GameObject.Find("Player1");
            Player2 = GameObject.Find("Player2");
        }
        if (scene.buildIndex > 2 && scene.name != "Champion")
        {
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        //time = timeDefault;
        //yield return new WaitForSeconds(3);
        //isGameReady = true;
        //while (time > 0)
        //{
        //    time--;
        //    yield return new WaitForSeconds(1);
        //}
        //isTimeOut = true;
        //isGameReady = false;
        yield return new WaitUntil(() => isTimeOut);
        GameObject.Find("StatusInGame").transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(WaitForNextScene());
    }
    IEnumerator WaitForNextScene()
    {
        yield return new WaitUntil(() => Player1 || Player2);


        var pointP1 = Player1.GetComponentInChildren<Player>().point;
        var pointP2 = Player2.GetComponentInChildren<Player>().point;
        if (pointP1 > pointP2)
        {
            winRatePlayer1++;
        }
        else if (pointP1 < pointP2)
        {
            winRatePlayer2++;
        }
        else
            draw++;

        yield return new WaitForSeconds(3);
        NextScene("Hungry");

    }
    private void Update()
    {
        if (!isCheckStart && playerReady == 2)
        {
            NextScene("Sunsine");
            isCheckStart = true;
        }
    }



    IEnumerator FadeOut(string name)
    {
        soundManager.FadeOutVolume();
        GameObject.FindGameObjectWithTag("CrossFade").GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        isLoadScene = false;

        //FadeIn
        StartCoroutine(FadeIn(name));
    }
    IEnumerator FadeIn(string name)
    {
        if (nameScene != "LoadingScene")
        {
            LoadScene("LoadingScene");
            yield return new WaitForSeconds(1.5f);
            NextScene(name);
        }
        else
        {
            LoadScene(name);
            yield return null;
            soundManager.FadeInVolume();
            CheckInMusic();
            //check if game-in and give info to character to spawn.
            GiveInfoToCharacter();
        }
    }

    private void GiveInfoToCharacter()
    {
        var listPlayer = GameObject.FindObjectsOfType<SpawnPlayerInGame>();
        foreach (var item in listPlayer)
        {
            if (item.transform.name == "Player1")
            {
                item.controlName = inputDevice[0];
                item.charSelection = listCharacterSelected[0];
            }
            else if (item.transform.name == "Player2")
            {
                item.controlName = inputDevice[1];
                item.charSelection = listCharacterSelected[1];
            }
        }
    }

    public void CheckInMusic()
    {
        if (SceneManager.GetActiveScene().name == "WaitingRoom")
        {
            soundManager.Play("Beso");
        }
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            soundManager.Play("Maestro");
        }
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        nameScene = name;

    }

    public void NextScene(string name)
    {
        isTimeOut = false;
        isLoadScene = true;
        StartCoroutine(FadeOut(name));
    }

}
