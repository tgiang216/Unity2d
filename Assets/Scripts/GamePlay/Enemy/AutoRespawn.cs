using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRespawn : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject respawnObj;
    [SerializeField] private GameObject respawnBoar;
    [SerializeField] private Transform respawnPos;

    private void Start()
    {
        respawnObj = this.gameObject;
        respawnPos = this.transform;
    }

    private void OnDestroyGameObj()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(duration);
        Instantiate(respawnObj,respawnPos.position, respawnPos.rotation);
    }
}
