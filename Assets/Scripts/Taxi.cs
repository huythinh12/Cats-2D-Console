using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : MonoBehaviour
{
    Vector3 nextPos;
    SpriteRenderer objectSprite;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        objectSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(TimeLife());
        
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name == "LeftTaxi")
        {
            nextPos = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 5 * Time.deltaTime);
        }
        else if(transform.parent.name == "RightTaxi")
        {
            var flip = new Vector3(-1, 1, 1);
            transform.localScale = flip;
            nextPos = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, nextPos, 5 * Time.deltaTime);
        }
    }
    IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponent<Collider2D>().name.StartsWith("LifeZoneTaxi"))
        //{
        //    Destroy(gameObject);
        //}
        if (collision.transform.tag == "Player")
        {

             player = collision.transform.GetComponent<Player>();
            if (!player.dying)
            {
                player.isDead = true;
                if (player.point >= 5)
                    player.point -= 5;
                else
                {
                    player.point = 0;
                }
            }
           
        }
    }
}
