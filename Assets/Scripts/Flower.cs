using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D colider;
    Animator anim;
    SpriteRenderer sprite;
    public bool isRunning = false;
    bool isLeft = true, onExitGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check coli with other side 
        if (isLeft)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        //check move and idle to anim
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        if (rb.velocity.y < -3)
        {
            //dang roi
            anim.SetBool("isFall", true);
            if (!isLeft)
            {
                sprite.flipX = true;
            }
            if (isLeft)
            {
                sprite.flipX = false;
            }
        }
        else if (rb.velocity.y <= 0 && onExitGround)
        {
            //cham dat
            anim.SetBool("isFall", false);
            anim.SetBool("isBreak", true);
            if (!isRunning)
            {
                isRunning = true;
                StartCoroutine(EndLife());
            }
        }
    }
    IEnumerator EndLife()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var player = collision.collider.GetComponent<Player>();
            var rb = collision.collider.GetComponent<Rigidbody2D>();
            if (collision.GetContact(0).normal.x == -1)
                isLeft = true;
            else
            {
                isLeft = false;
            }

            if (collision.GetContact(0).normal.y >= 0.5f)
            {
                rb.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
                StartCoroutine(player.WaitForAnimationFinish());
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            onExitGround = true;
        }
    }
}
