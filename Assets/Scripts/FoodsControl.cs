using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsControl : MonoBehaviour
{
    public GameObject[] foods;
    Animator animt;
    float animTime;
    bool endTime = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnFoods());
        animt = GetComponent<Animator>();
    }
    IEnumerator RandomSpawnFoods()
    {
        int d = Random.Range(3,6);
        yield return new WaitUntil(() => animt);

        yield return new WaitForSeconds(2);
        while (true)
        {
            animt.SetBool("makefood", true);
            yield return null;
            yield return new WaitForSeconds(animTime * 2);
            Instantiate(foods[Random.Range(0, 11)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(animTime);
            animt.SetBool("makefood", false);
            yield return new WaitForSeconds(3);
            d--;
            //spawn crazy
            if (d == 0)
            {
                d = Random.Range(3, 6);
                if (Random.Range(0, 3) == 1)
                {
                    animt.SetBool("crazy", true);
                    yield return null;
                    StartCoroutine(CrazySpawnTime());
                    yield return new WaitForSeconds(animTime);
                    endTime = true;
                    animt.SetBool("crazy", false);
                    yield return new WaitForSeconds(6);
                }
            }
            
        }
    }
    IEnumerator CrazySpawnTime()
    {
        while (true)
        {
            yield return null;
            if (endTime)
            {
                endTime = false;
                break;
            }
            yield return new WaitForSeconds(0.3f);
            Instantiate(foods[Random.Range(0, 11)], transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        animTime = animt.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }
}
