using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PossProcessingTest : MonoBehaviour
{
    public Volume postProcessVolume;
    private float value = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDamage();
        }
    }

    private void OnDamage()
    {
        if(postProcessVolume.profile.TryGet(out Vignette vignette))
        {
            value += 0.2f;
            value = Mathf.Clamp(value, 0f, 0.6f);
            vignette.intensity.value = value;
        }
    }
   
}
