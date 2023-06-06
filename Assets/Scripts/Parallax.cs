using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camera;
    public float relative = 0.3f;
    public bool lockY = false;

    private void Start()
    {
        camera = FindObjectOfType<CinemachineVirtualCamera>().transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(lockY)
        {
            transform.position = new Vector2(camera.position.x*relative, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(camera.position.x * relative, camera.position.y* relative);

        }
    }
}
