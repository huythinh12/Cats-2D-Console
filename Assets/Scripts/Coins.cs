using System.Collections;
using UnityEngine;

public class Coins : MonoBehaviour
{
    GameObject[] coinsPos;
    public GameObject coin;
    GameObject coinObject;
    bool isTimeout;
    Coroutine saveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        coinsPos = GameObject.FindGameObjectsWithTag("CoinsPos");
        StartCoroutine(RandomCoins());
    }
    IEnumerator RandomCoins()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            isTimeout = false;
            var rd = Random.Range(0, coinsPos.Length - 1);
             coinObject = Instantiate(coin, coinsPos[rd].transform.position, Quaternion.identity, coinsPos[rd].transform);
            yield return null;
            var coinColor = coinObject.GetComponent<SpriteRenderer>();
            saveCoroutine = StartCoroutine(LifeTime(coinColor));
            yield return new WaitUntil(() => isTimeout);
        }
    }
    IEnumerator LifeTime(SpriteRenderer coinColor)
    {
        var  coinLife = coinObject.GetComponent<CoinLifeCycle>();
        StartCoroutine(CheckObtain(coinLife));
        yield return new WaitForSeconds(8);
        float currentValue = 255;
        float valuePerTime = (currentValue / 3);
        valuePerTime *= Time.fixedDeltaTime;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            byte a = (byte)(currentValue -= valuePerTime);
            coinColor.color = new Color32(255, 255, 255, a);
            if (currentValue <= 0)
                break;
        }
        isTimeout = true;
        if (!coinLife.obtainCoin)
        {
            Destroy(coinObject);
        }
    }
    IEnumerator CheckObtain(CoinLifeCycle coinLife)
    {
        while (true)
        {
            yield return null;
            if (coinLife.obtainCoin)
            {
                StopCoroutine(saveCoroutine);
                break;
            }
        }
        isTimeout = true;
        Destroy(coinObject);
    }
    // Update is called once per frame

}
