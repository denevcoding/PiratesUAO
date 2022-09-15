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
        
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Jump();
    }


    void IsGrounded()
    {
        Vector2 rayStartPos = capsuleColldier.bounds.extents;

        float dist = capsuleColldier.bounds.extents.y;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, dist + 0.05f, obstacleMask);
        Debug.DrawRay(transform.position, Vector2.down  * (dist + 1f), Color.cyan);

        if (hit)
        {
            isGrounded = true;
            animController.SetBool("Jump", false);
        }
        else
        {
            isGrounded = false;
        }


        animController.SetBool("isGrounded", isGrounded);
    }


    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                rigibBodie.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animController.SetBool("Jump", true);
               

            }
        }
    }

    public void Runningninja()

    {
        if (Input.GetKeyDown(KeyCode.D))


            if (Input.GetKeyDown(KeyCode.A))

                

        {
            if (isGrounded == true)

            {
                animController.SetBool("Runningninja", true);


            }
        }
    }
}
