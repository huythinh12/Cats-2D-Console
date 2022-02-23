using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public GameObject Bg1, Bg2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Bg1.transform.Translate(Vector3.down * Time.deltaTime);
        Bg2.transform.Translate(Vector3.down * Time.deltaTime);
        if (Bg1.transform.position.y <= -16f)
        {
            Bg1.transform.position = new Vector2(Bg1.transform.position.x, 16f);
        }
        if (Bg2.transform.position.y <= -16f)
        {
            Bg2.transform.position = new Vector2(Bg2.transform.position.x, 16f);

        }
    }
}
