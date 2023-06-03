using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFixed : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    public Sprite[] sprites;
    private void Awake()
    {
        
    }
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        ScaleCameraSize();
    }

    // Update is called once per frame
    void Update()
    {
        BGFollowCamera();
    }

    void ScaleCameraSize()
    {
        float cameraHeight = virtualCamera.m_Lens.OrthographicSize * 2f;
        float cameraWidth = cameraHeight * virtualCamera.m_Lens.Aspect;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector3 scale = transform.localScale;
        scale.x = cameraWidth / spriteSize.x;
        scale.y = cameraHeight / spriteSize.y;

        transform.localScale = scale;
    }
    void BGFollowCamera()
    {
        Vector3 bgPos = new Vector3(virtualCamera.transform.position.x, virtualCamera.transform.position.y, -5);
        transform.position = bgPos;


    }
}
