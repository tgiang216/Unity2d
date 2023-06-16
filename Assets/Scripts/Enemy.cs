using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject effect;
           
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Get hit !!!!!!!");
        if (collision.collider.CompareTag("PlayerSword"))
        {
            Vector3 hitPos = collision.transform.position;
            GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
            Destroy(hit, 2f);

            Debug.Log("Get hit !");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerSword")
        {
            Vector3 hitPos = collision.transform.position;
            GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
            Destroy(hit, 2f);

            Debug.Log("Get hit !");
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
       
    //    if (collision.CompareTag("PlayerSword"))
    //    {
    //        Debug.Log("Get hit ! 3");
    //        Vector3 hitPos = collision.transform.position;
    //        GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
    //        Destroy(hit, 0.3f);
    //    }
    //}

    public void TakeDamage(float damage , Vector2 positon)
    {
       // Debug.Log("Get hit ! 3");
        Vector3 hitPos = positon;
        GameObject hit = Instantiate(effect, hitPos, Quaternion.identity);
        Destroy(hit, 0.2f);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }
}
