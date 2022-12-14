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

    public LevelManager lvlManager;
    public CharacterStats charStats;

    CapsuleCollider2D capsuleColldier;
    AudioSource pirateSource;
    Rigidbody2D rigibBodie;
    Animator animController;

    LayerMask obstacleMask;

    public AudioClip jumpSoundClip;
    public AudioClip DeadSoundClip;

    public bool isGrounded= false;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float coyoteTime;
    public float coyoteTimeCounter;

    [Header("Sliding")]
    public ParticleSystem dust;

    Vector2 colliderDefaultSize = Vector2.zero;

    public bool facingRight;

    //public float Accel;
    public float moveSpeed;
    public float maxSpeed;
    public float airSpeed;
    public float groundSpeed;

    public Vector2 inputDirection = Vector2.zero;

    private void Awake()
    {
        capsuleColldier = GetComponent<CapsuleCollider2D>();
        rigibBodie = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        pirateSource = GetComponent<AudioSource>();

        charStats = GetComponent<CharacterStats>();

        obstacleMask = LayerMask.GetMask("Obstacle");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = 60;
        facingRight = true;
        colliderDefaultSize = capsuleColldier.size;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

        HandleCoyoteTime();

        if (isGrounded)        
            moveSpeed = groundSpeed;        
        else        
            moveSpeed = airSpeed;
        

        CalculateInputDirection();

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
            Vector2 vel = new Vector2(0f, rigibBodie.velocity.y);
            rigibBodie.velocity = vel;
        }
        
    }

    public void CalculateInputDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(h, v);

        inputDirection.Normalize();

        if (inputDirection.magnitude > 0f)        
            animController.SetBool("Running", true);
        else        
            animController.SetBool("Running", false);
        
        

        Debug.Log("X: " + h + "   " + "Y: " + v);
    }

    private void FixedUpdate()
    {
        //Move();

        if (state == PirateState.Alive)
        {
            //MoveVelocity();
            MoveVelocityToInputDirection();

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

        animController.SetFloat("YVel", rigibBodie.velocity.y);
    }



    private void MoveByForce()
    {
        rigibBodie.AddForce(Vector2.right * moveSpeed , ForceMode2D.Force);

        if (rigibBodie.velocity.x > maxSpeed)
        {
            rigibBodie.AddForce(rigibBodie.velocity * -1, ForceMode2D.Force);
        }
    }

    public void MoveVelocity()
    {    
        rigibBodie.velocity = new Vector2(moveSpeed *  Time.deltaTime, rigibBodie.velocity.y);
        //if (rigibBodie.velocity.x > maxSpeed)
        //{
        //    rigibBodie.velocity = maxSpeed;
        //}
    }

    public void MoveVelocityToInputDirection()
    {
        rigibBodie.velocity = new Vector2(inputDirection.x * (moveSpeed * Time.deltaTime), rigibBodie.velocity.y);

        if (inputDirection.x < 0)
        {
            if (facingRight != false)
                Flip();


            //entityOwner.GetRenderer().flipX = true;
        }
        else if (inputDirection.x > 0)
        {
            if (facingRight != true)
                Flip();

            //entityOwner.GetRenderer().flipX = false;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180f, 0);
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coyoteTimeCounter > 0f)
            {
                capsuleColldier.size = colliderDefaultSize;

                rigibBodie.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                //rigibBodie.velocity = new Vector2(rigibBodie.velocity.x, jumpForce);
                animController.SetBool("jumping", true);
                PlayVFXSound(jumpSoundClip, 0.5f);

            }
        }
    }

    public void HandleCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }



    public void Slide()
    {
        //sliding prototype
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.DownArrow))
        {
            if (isGrounded == true && Mathf.Abs(inputDirection.x)  > 0f)
            {

                dust.Play();
                //dust.gameObject.SetActive(true);
                moveSpeed *= 2f;
                animController.SetBool("sliding", true);
                Vector2 size = capsuleColldier.size;
                size.y = 1.26f;
                capsuleColldier.size = new Vector3(size.x, size.y);
            }
        }
        else
        {
            dust.Stop();
            
            animController.SetBool("sliding", false);           
            capsuleColldier.size = colliderDefaultSize;
        }
    }

    public void ReceiveDamage()
    {
        charStats.CalculateDamage();
    }

    public void Dead()
    {
        state = PirateState.Dead;
        animController.SetBool("Dead", true);
        PlayVFXSound(DeadSoundClip, 0.5f);


    }

    public void Respawn()
    {
        lvlManager.Respawn();
    }

    public void DelayRespawn(float time)
    {
        Dead();
        Invoke("Respawn", time);
    }

    public void Reborn()
    {
        animController.SetBool("Dead", false);
        state = PirateState.Alive;
     
    }






    //Sonido
    public void PlayVFXSound(AudioClip clip, float volume)
    {
        pirateSource.PlayOneShot(clip, volume);
    }

}
