using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLifeCycle : MonoBehaviour
{
    public bool obtain = false;
    Player player;
    SpriteRenderer itemColor;
    private void Start()
    {
        if (transform.name.Contains("Clone") && transform.tag =="HeadItem")
        {
            player = transform.parent.GetComponent<Player>();
            itemColor = GetComponent<SpriteRenderer>();
            StartCoroutine(CountDownItem());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.tag == "Item" && collision.transform.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            player.nameItem = transform.name;
            player.isColiderItem = true;
            obtain = true;
            Destroy(gameObject, 0.02f);//
        }
    }


    IEnumerator CountDownItem()
    {
        yield return new WaitForSeconds(3);
        float currentValue = 255;
        float valuePerTime = (currentValue / 2);
        valuePerTime *= Time.fixedDeltaTime;
        while (true)
        {
            yield return new WaitForFixedUpdate();
            byte a = (byte)(currentValue -= valuePerTime);
            itemColor.color = new Color32(255, 255, 255, a);
            if (currentValue <= 0)
                break;
        }
        player.listItemObj.Remove(gameObject);
        player.nameItem = "";
        player.movementSpeed = 3;
        player.isExtra = false;
        player.isMinus = false;
        Destroy(gameObject, 0.02f);

    }
  
    // Update is called once per frame
    private void OnDestroy()
    {
        if (player)
            player.isColiderItem = false;
    }
}
