using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PirateState
{
    Alive,
    Dead,


}

public class CharacterController : MonoBehaviour
{
    public PirateState state;

    CapsuleCollider2D capsuleColldier;
    Rigidbody2D rigibBodie;
    Animator animController;

    LayerMask obstacleMask;

    public bool isGrounded= false;

    public float jumpForce = 10f;

    Vector2 colliderDefaultSize = Vector2.zero;

    public float Accel;
    public float moveSpeed;
    public float maxSpeed;

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
        // Make the game run as fast as possible
        Application.targetFrameRate = 60;
        colliderDefaultSize = capsuleColldier.size;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

        if (state == PirateState.Alive)
        {
            Jump();

            Slide();

            if (isGrounded == false)
                animController.SetBool("sliding", false);
        }else if (state == PirateState.Dead)
        {
            animController.SetBool("sliding", false);
            animController.SetBool("jumping", false);
            animController.SetBool("Dead", true);
        }
        
    }

    private void FixedUpdate()
    {
        //Move();

        if (state == PirateState.Alive)
        {
            MoveVelocity();

            if (animController.GetBool("jumping"))
            {
                if (rigibBodie.velocity.y <= -1)
                {
                    capsuleColldier.size = colliderDefaultSize;
                    animController.SetBool("jumping", false);
                    animController.SetBool("sliding", false);
                }
            }

            //Debug.Log(rigibBodie.velocity.magnitude);
        }


    }



    private void Move()
    {
        rigibBodie.AddForce(Vector2.right * moveSpeed , ForceMode2D.Force);

        if (rigibBodie.velocity.x > maxSpeed)
        {
            rigibBodie.AddForce(rigibBodie.velocity * -1, ForceMode2D.Force);
        }
    }

    public void MoveVelocity()
    {
    
        rigibBodie.velocity = new Vector2(moveSpeed , rigibBodie.velocity.y);
        //if (rigibBodie.velocity.x > maxSpeed)
        //{
        //    rigibBodie.velocity = maxSpeed;
        //}
    }


    //Chek if pirate character is touching the floor
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
        if (state == PirateState.Dead)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isGrounded == true)
            {
                capsuleColldier.size = colliderDefaultSize;

                rigibBodie.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                //rigibBodie.velocity = new Vector2(rigibBodie.velocity.x, jumpForce);
                animController.SetBool("jumping", true);

            }
        }
    }




    public void Slide()
    {
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


    public void Dead()
    {
        state = PirateState.Dead;
        animController.SetBool("Dead", true);

    }



}
