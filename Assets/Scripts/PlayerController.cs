using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public Transform groundPoint;
    public float groundCheckRadius;
    public LayerMask walkableSurface;

    AudioSource playerAS;
    AudioSource groundCheckAS;
    bool inAir;
    bool facingRight = true;
    float walkInput = 0f;
    float jumpInput = 0f;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerAS = GetComponent<AudioSource>();
        groundCheckAS = groundPoint.gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        walkInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    private void FixedUpdate()
    {
        GroundCheck();

        // In air state and jump initiation
        if (inAir)
        {
            animator.SetFloat("verticalSpeed", rb.velocity.y);
        }
        else if (!inAir && jumpInput > 0f)
        {
            Jump();
        }

        // Horizontal movement if possible
        Walk();
    }

    bool CheckFlip()
    {
        if (facingRight && rb.velocity.x < -0.01f)
        {
            return true;
        }
        else if (!facingRight && rb.velocity.x > 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Walk()
    {
        // Manipulating on rigid body velocity, which allows usage of external forces
        Vector2 speedChange = Vector2.right * playerSpeed * walkInput;

        // if speed change and velocity have different signs - add
        if (speedChange.x * rb.velocity.x < 0f)
        {
            rb.velocity += speedChange;
        }
        // if speed change and velocity have the same sign and velocity is less than speed change - set maximal horizontal velocity
        else if (Mathf.Abs(rb.velocity.x) <= Mathf.Abs(speedChange.x))
        {
            rb.velocity = new Vector2(speedChange.x, rb.velocity.y);
        }

        // Adjusting walk animation
        if (walkInput != 0f)
        {
            if (inAir)
            {
                playerAS.Pause();
            }
            else
            {
                playerAS.UnPause();
            }
            animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        }
        else
        {
            animator.SetFloat("speed", 0f);
            playerAS.Pause();
        }

        // Flipping to change direction if needed
        if (CheckFlip())
        {
            Flip();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Fly()
    {
        inAir = true;
        animator.ResetTrigger("isLanding");
        animator.SetTrigger("isJumping");
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void Land()
    {
        if (inAir)
        {
            groundCheckAS.Play();
        }
        inAir = false;
        animator.ResetTrigger("isJumping");
        animator.SetTrigger("isLanding");
    }

    void GroundCheck()
    {
        Collider2D other = Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, walkableSurface);
        if (other != null)
        {
            Land();
        }
        else
        {
            Fly();
        }
    }

    public void SetDisabled()
    {
        playerAS.Pause();
        groundCheckAS.Pause();
        animator.SetFloat("speed", 0f);
        enabled = false;
    }
}
