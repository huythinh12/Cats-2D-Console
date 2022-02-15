using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    public Animator anim;
    public GameObject[] items;
    GameObject itemObject;
    bool isClose, isOpen;
    public bool isTimeout;
    Vector3 nextPos;
    Coroutine saveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = new Vector3(transform.position.x, -6, 0);
    }
    public IEnumerator RandomOpen()
    {
        isTimeout = false;
        anim.SetTrigger("AutoDoorOpen");
        yield return new WaitUntil(() => isOpen);
        isOpen = false;
        var rd = Random.Range(0, items.Length - 1);
        itemObject = Instantiate(items[rd], nextPos, Quaternion.identity, transform);
        yield return null;
        var itemColor = itemObject.GetComponent<SpriteRenderer>();
        if (itemObject.tag == "Item")
            saveCoroutine = StartCoroutine(LifeTime(itemColor));
        yield return new WaitUntil(() => isClose);
        isClose = false;

    }

    IEnumerator LifeTime(SpriteRenderer itemColor)
    {
        var itemLife = itemObject.GetComponent<ItemLifeCycle>();
        yield return new WaitForSeconds(5);
        float currentValue = 255;
        float valuePerTime = (currentValue / 3);
        valuePerTime *= Time.fixedDeltaTime;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (itemColor)
            {
                byte a = (byte)(currentValue -= valuePerTime);
                itemColor.color = new Color32(255, 255, 255, a);
                if (currentValue <= 0)
                    break;
            }
            else
            {
                break;
            }
        }
        isTimeout = true;
        if (!itemLife.obtain)
        {
            Destroy(itemObject);
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("AutoDoorOpen");
        }
    }
    public void OpenCompletedDoor()
    {
        isOpen = true;
    }
    public void CloseTheDoor()
    {

        isClose = true;
    }
}
