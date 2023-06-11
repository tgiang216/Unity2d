using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatCtrl : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject thunderPrefab;
    [SerializeField] Transform thunderPos;
    [SerializeField] float thunderTime = 0.4f;

    public void CreateThunder()
    {
        GameObject effect = Instantiate(thunderPrefab,thunderPos.position,thunderPos.rotation);
        Destroy(effect, thunderTime);
    }
}
