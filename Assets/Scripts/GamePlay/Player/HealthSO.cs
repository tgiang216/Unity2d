using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Custom/HealthData", order = 1)]
public class HealthSO : ScriptableObject
{
    public float maxHP;
    public float currentHP;
}
