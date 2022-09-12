using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    CapsuleCollider2D capsuleColldier;
    Rigidbody2D rigibBodie;
    Animator animController;

    LayerMask obstacleMask;

    public bool isGrounded= false;

    public float jumpForce = 10f;

    Vector2 colliderDefaultSize = Vector2.zero;

    private void Awake()
    {
        capsuleColldier = GetComponent<CapsuleCollider2D>();
        rigibBodie = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();

        obstacleMask = LayerMask.GetMask("Obstacle");
    }

    // Start is called before the first frame update
    void Start()
    {
        colliderDefaultSize = capsuleColldier.size;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Jump();

        //sliding prototype
        if (Input.GetMouseButton(1))
        {
            if (isGrounded == true)
            {
                animController.SetBool("sliding", true);
                Vector2 size = capsuleColldier.size;
                size.y = 1.26f;
                capsuleColldier.size = new Vector3(size.x, size.y);
            }
        }
        else
        {
            animController.SetBool("sliding", false);           
            capsuleColldier.size = colliderDefaultSize;
        }
    }

    private void FixedUpdate()
    {
        if (animController.GetBool("jumping"))
        {
            if (rigibBodie.velocity.y <= -1)
            {
                capsuleColldier.size = colliderDefaultSize;
                animController.SetBool("jumping", false);
                animController.SetBool("sliding", false);                
            }
        }
            
    }


    void IsGrounded()
    {
        float dist = capsuleColldier.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, dist + 0.05f, obstacleMask);
        Debug.DrawRay(transform.position, Vector2.down  * (dist + 0.05f), Color.cyan);

        if (hit)        
            isGrounded = true;       
        else        
            isGrounded = false;

        animController.SetBool("isGrounded", isGrounded);
    }


    public void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isGrounded == true)
            {
                capsuleColldier.size = colliderDefaultSize;
                rigibBodie.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animController.SetBool("jumping", true);
            }
        }
    }
}