using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointInGame : MonoBehaviour
{
    public Slider pointSlider,statusSlider;
    public Text textScore;
    float point;
    // Start is called before the first frame update
    void Start()
    {
        pointSlider.value = 50;
        statusSlider.value = 2;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = point.ToString();
      
    }

  
   
    public void SetStatus(float value)
    {
        statusSlider.value = value;
    }
   
    public void SetPoint(float value)
    {
        point = value;
    }
    public void SetState(float value)
    {
        pointSlider.value = value;
    }
  
}
