using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitEffect : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Material flashMaterial;
    Material originalMaterial;
    [SerializeField]
    float duration;
    [SerializeField]
    Color flashColor;
    private Coroutine flashRoutine;

    public bool isBlinking;
    private bool blinking = false;
    public float blinkduration;
    private float timer;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalMaterial = sprite.material;
        flashMaterial = new Material(flashMaterial);
        isBlinking= false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isBlinking) return;
       
        timer += Time.deltaTime;
        sprite.enabled = blinking;
        blinking = !blinking;
        
        if(timer >= blinkduration)
        {
            sprite.enabled = true;
            isBlinking= false;
        }
    }
    public void Blink(float duration)
    {
        timer = 0;
        blinkduration= duration;
        isBlinking = true;
    }
    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine(flashColor));
    }
    private IEnumerator FlashRoutine(Color color)
    {
        // Swap to the flashMaterial.
        sprite.material = flashMaterial;
        //color.a = 255f;
        flashMaterial.color= color;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        sprite.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
