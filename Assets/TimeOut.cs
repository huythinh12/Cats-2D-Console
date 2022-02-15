using UnityEngine;

public class TimeOut : MonoBehaviour
{
    Animator animFadeOut;
    // Start is called before the first frame update
    private void OnEnable()
    {
        animFadeOut = GameObject.Find("CrossFade").GetComponent<Animator>();
        
    }
  
    public void TimeOutInGame()
    {
        animFadeOut.SetTrigger("FadeOut");
    }
}
