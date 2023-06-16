using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarCtrl : MonoBehaviour
{
    public Image healbarImg;
    public float MaxValue { get; set; }

 
    public void SetHealthBar(float value)
    {
        healbarImg.fillAmount = value / MaxValue;
    }
}
