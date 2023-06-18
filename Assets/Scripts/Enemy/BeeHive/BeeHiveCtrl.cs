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

    [SerializeField]
    private Transform player;
    [SerializeField]
    public bool IsPlayerInRange => Vector3.Distance(player.position, transform.position) <= range;
    [SerializeField]
    private float range;
    [SerializeField]
    private float DistaceToPlayer => Vector3.Distance(player.position, transform.position);

    private float timer = 0;

    [SerializeField]
    private float bornBeeCooldown;

    private void Start()
    {
        timer = 0;
        beeList= new List<GameObject>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (beeList.Count >= maxBee) return;
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
        //Debug.Log("Bee : " + beeList.Count);
        foreach(var b in beeList)
        {
            if(b == null) beeList.Remove(b);
        }
        
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
