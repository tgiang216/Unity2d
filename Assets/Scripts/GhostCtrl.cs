using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCtrl : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float delay = 1.0f;
    float delta = 0;

    public PlayerStatesCtrl player;

    SpriteRenderer spriteRenderer;
    public float destroyTime = 0.2f;
    public Color color;
    public Material material = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delta > 0) { delta -= Time.deltaTime; }
        else
        {
            delta = delay;
            createGhost();
        }
    }

    private void createGhost()
    {
        //Debug.Log("Ghost");
        GameObject ghostObj = Instantiate(ghostPrefab,player.transform.position,player.transform.rotation);
        ghostObj.transform.localScale = player.transform.localScale;
        Destroy(ghostObj, destroyTime);
        spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = player.spriteRenderer.sprite;
        spriteRenderer.color = color;
        if(material!=null) spriteRenderer.material = material;

    }
}
