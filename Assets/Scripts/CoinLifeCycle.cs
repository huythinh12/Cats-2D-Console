using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLifeCycle : MonoBehaviour
{
    public bool obtainCoin = false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            if (!player.isExtra && player.isMinus)
            {
                if (player.point <= 8)
                {
                    player.point = 0;
                }
                else if (player.point > 8)
                {
                    player.point -= 8;
                }
            }
            else if (!player.isMinus && player.isExtra)
            {
                if (player.point <= 92)
                {
                    player.point += 8;
                }
                else if (player.point > 92)
                {
                    player.point = 100;
                }
            }
            else
                player.point += 1;
            obtainCoin = true;
        }
    }

    // Update is called once per frame

}
