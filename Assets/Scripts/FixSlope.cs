using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSlope : MonoBehaviour
{
    public LayerMask groundLayer;
    public float maxRayLength = 3;
    public float offset = .5f; // set it to half of your character height
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    public void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 4, groundLayer);

        if (hit.collider !=null)
        {
            //transform.up = ground.normal;
            Vector2 desiredPos = hit.point + new Vector2(0f, offset);
            Vector2 currentPosition = rb.position + boxCollider.offset;
            Vector2 newPosition = currentPosition + (desiredPos - currentPosition) * Time.fixedDeltaTime;
            rb.MovePosition(newPosition - boxCollider.offset);
            
        }
        
    }
}
