using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneral : MonoBehaviour
{
    public static bool rd;
    public static bool RanDomDoorAuto()
    {
        if (Random.Range(0, 2) == 0)
        {
            return rd = true;
        }
        else return rd =false;
    }
    // Start is called before the first frame update
    // Update is called once per frame
}
