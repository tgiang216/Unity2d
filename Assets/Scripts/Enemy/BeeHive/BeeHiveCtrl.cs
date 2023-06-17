using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHiveCtrl : MonoBehaviour
{
    [SerializeField]
    private int maxBee = 5;
    [SerializeField]
    private GameObject beePrefab;
    [SerializeField]
    private List<GameObject> beeList;
    [SerializeField]
    private float radius;

    private float timer = 0;

    [SerializeField]
    private float bornBeeCooldown;

    private void Start()
    {
        timer = 0;
        beeList= new List<GameObject>();
    }

    private void Update()
    {
        if (beeList.Count >= 5) return;
        timer += Time.deltaTime;
        if(timer > bornBeeCooldown )
        {
            timer = 0;
            MakeBee();
        }
    }

    public void MakeBee()
    {
        GameObject bee = Instantiate(beePrefab, transform.position, transform.rotation);
        bee.GetComponent<BeeStateCtrl>().beeHive = this;
        beeList.Add(bee);
        Debug.Log("Bee : " + beeList.Count);
        foreach(var b in beeList)
        {
            if(b == null) beeList.Remove(b);
        }
        //BeeStateCtrl beectrl = bee.GetComponent<BeeStateCtrl>();
        //if(beectrl != null )
        //{
        //    beectrl.ChangeState(beectrl.moveState);
        //}
    }

    private Vector3 GetMakeBeePos()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        Vector3 randomPosition = transform.position + randomDirection * radius;
        return randomPosition;
    }

    public void OnABeeDeath(GameObject bee)
    {
        beeList.Remove(bee);
    }
}
