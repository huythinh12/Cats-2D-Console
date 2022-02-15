
using UnityEngine.SceneManagement;
using UnityEngine;

public class CrossFade : MonoBehaviour
{
    Animator anim;
    public bool isReady = false;
    bool isFadeInDone = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    //fadein luon duoc goi tu anim
    public void isFadeIn()
    {
        isFadeInDone = true;
    }
    //go duoc goi tu anim CountDown cua CrossFade
    public void Go()
    {
       GameManager.isGameReady = true;
    }
    
    public void isFadeOut()
    {
        anim.SetBool("isFadeIn", false);
        isFadeInDone = false;
     
    }
    // Update is called once per frame
    void Update()
    {
        //kiem tra khi fade in trong game thi moi hien thi anim ready and go 
        if (isFadeInDone && SceneManager.GetActiveScene().name!="StartScene" && SceneManager.GetActiveScene().name != "LoadingScene" &&
            SceneManager.GetActiveScene().name !="WaitingRoom")
        {
        anim.SetBool("isFadeIn", true);

        }
        

    }
}
