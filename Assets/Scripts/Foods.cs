using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Color foodcolor;
    bool isRunning = false, onGround = false;
    bool expiryFood = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (Random.Range(0, 2) == 1)
        {
            expiryFood = true;
            foodcolor = sprite.color = Color.green;
        }
        else
        {
            expiryFood = false;
            foodcolor = sprite.color = Color.white;
        }

        if (rb)
            rb.velocity = new Vector2(Random.Range(3, 6), Random.Range(8, 15));
    }
    //0.6666667
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(1);
        float x = 0.6666667f;
        float time = 0.2f;
        int d = 5;
        while (d > 0)
        {
            sprite.color = new Color(foodcolor.r, foodcolor.g, foodcolor.b, x);
            yield return new WaitForSeconds(time);
            sprite.color = foodcolor;
            yield return new WaitForSeconds(time);

            d--;
            if (d == 0)
            {
                sprite.color = new Color(foodcolor.r, foodcolor.g, foodcolor.b, x);
            }
        }

        float t = 3;
        while (sprite.color.a > 0.1)
        {
            yield return null;
            sprite.color = Color.Lerp(sprite.color, Color.clear, t * Time.fixedDeltaTime);
        }
        Destroy(gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground") onGround = true;
        else onGround = false;
        if (collision.transform.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (!player.getFat)
            {
                if (!expiryFood)
                {
                    player.point++;
                    player.givingPoint = true;
                    player.state++;
                }
                else
                {
                    player.givingPoint = false;
                    player.state-=3;
                }
            }
            else
            {
                player.point++;
            }

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (onGround)
        {
            if (!isRunning)
            {
                isRunning = true;
                StartCoroutine(LifeTime());
            }
        }
    }
}
