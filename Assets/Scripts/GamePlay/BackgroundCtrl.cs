using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Color thunerOriginColor;
    [SerializeField] Color thunderFlashColor;
    [SerializeField] int number;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }
    void Start()
    {      
        ScaleCameraSize();
        AudioManager.Instance.PlayBGM("BGM_05");
    }

    // Update is called once per frame
    void Update()
    {
        BGFollowCamera();
    }

    void ScaleCameraSize()
    {
        //Debug.Log("Scale Camera");
        float cameraHeight = virtualCamera.m_Lens.OrthographicSize * 2f;
        float cameraWidth = cameraHeight * 19f/6;
        //Debug.Log(cameraWidth +"  "+cameraHeight); 
        //Debug.Log(virtualCamera.m_Lens.Aspect);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector3 scale = transform.localScale;
        scale.x = cameraWidth / spriteSize.x;
        scale.y = cameraHeight / spriteSize.y;
       // Debug.Log(scale.x + "  " + scale.y);

        transform.localScale = scale;
    }
    void BGFollowCamera()
    {
        Vector3 bgPos = new Vector3(virtualCamera.transform.position.x, virtualCamera.transform.position.y, -5);
        transform.position = bgPos;


    }

    public void CameraShake()
    {

        StartCoroutine(CameraShake(0.1f));
    }

    public void ThunderFlash()
    {
        StartCoroutine(ThunderFlashEffect());
    }
    private IEnumerator ThunderFlashEffect()
    {
        spriteRenderer.color = thunderFlashColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = thunerOriginColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = thunderFlashColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = thunerOriginColor;
    }

    public IEnumerator CameraShake(float duration)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 2f;
        yield return new WaitForSeconds(duration);
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
