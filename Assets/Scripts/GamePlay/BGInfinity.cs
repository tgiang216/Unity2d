using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BGInfinity : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private float starPos;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        starPos = transform.position.x;
        length = GetComponent<TilemapRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        BGLoop();
    }
    void BGLoop()
    {
        float temp = virtualCamera.transform.position.x;
        if(temp > starPos+length)
        {
            starPos += 2*length;
        }else if (temp < starPos-length)
        {
            starPos -= 2*length;
        }
        transform.position= new Vector3(starPos, transform.position.y, transform.position.z);
    }
}
