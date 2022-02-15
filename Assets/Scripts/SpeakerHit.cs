using UnityEngine;

public class SpeakerHit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            var rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
            StartCoroutine(player.WaitForAnimationFinish());
        }
    }
    // Update is called once per frame

}
