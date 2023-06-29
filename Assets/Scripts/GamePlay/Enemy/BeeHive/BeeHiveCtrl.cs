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
    private AudioSource audio;

    private void Start()
    {
        timer = 0;
        beeList= new List<GameObject>();
        player = GameObject.FindWithTag("Player").transform;
        audio = GetComponent<AudioSource>();
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
        if (DistaceToPlayer < range*2)
        {
            audio.mute = false;
        }else
        {
            audio.mute = true;
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


    public void OnABeeDeath(GameObject bee)
    {
        beeList.Remove(bee);
    }
}
