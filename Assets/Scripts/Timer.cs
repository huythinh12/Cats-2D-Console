using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text minText, secText;
    int minutes = 0;
    int second = 50;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.min>0 || GameManager.sec >0)
        {
            minutes = GameManager.min;
            second = GameManager.sec;
        }
   
        string min = ConvertNumber(minutes);
        string sec = ConvertNumber(second);
        minText.text = min;
        secText.text = sec;
        StartCoroutine(CountDownTime());
    }
    IEnumerator CountDownTime()
    {

        yield return new WaitUntil(() =>GameManager.isGameReady);
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (second <= 0)
            {
                second = 59;
                minutes--;
            }
            second--;
            minText.text = ConvertNumber(minutes);
            secText.text = ConvertNumber(second);
            if (minutes == 0 && second == 0) break;

        }
        GameManager.isGameReady = false;
        GameManager.isTimeOut = true;
        GameObject.Find("StatusInGame").transform.GetChild(0).gameObject.SetActive(true);

    }
    private string ConvertNumber(int number)
    {
        if (number <= 9)
            return "0" + number.ToString();
        else
            return number.ToString();
    }

    // Update is called once per frame
}
