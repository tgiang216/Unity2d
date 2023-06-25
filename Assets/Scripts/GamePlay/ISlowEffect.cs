using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlowEffect
{
    public float SlowTime { get; set; }
    public float SlowRate { get; set;}
    public void SlowColorChange() { }
}
