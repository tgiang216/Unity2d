using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float relative = 0.3f;
    public bool lockY = false;

    private void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(lockY)
        {
            transform.position = new Vector2(cam.position.x*relative, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(cam.position.x * relative, cam.position.y* relative);

        }
    }
}
