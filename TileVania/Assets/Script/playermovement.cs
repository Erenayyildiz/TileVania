using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class playermovement : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    CapsuleCollider2D capsule;
    Vector2 moveInput;

    [SerializeField] Transform groundcheck;
    [SerializeField] LayerMask groundlayer;
    [SerializeField] float speed;
    [SerializeField] float jumpingPower;
    [SerializeField] float climbSpeed;
    [SerializeField] Vector2 deadKick = new Vector2();
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float delay;

    private float vertical;
    private float horizontal;
    private bool isfacingright = true;
    private bool isAlive = true;
    private float gravityscaleAtStart;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gravityscaleAtStart = rb.gravityScale;
    }
    void Update()
    {
        Die();
        climbladder();

        if (!isAlive)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(!isfacingright && horizontal >0f)
        {
            flip();
        }
        else if(isfacingright && horizontal <0f)
        {
            flip();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!isAlive)
        {
            return;
        }

        if(context.performed)
        {
           anim.SetBool("Fire", true);
            Instantiate(bullet, gun.position, transform.rotation);
        }
        if(context.canceled)
        {
            anim.SetBool("Fire", false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!isAlive)
        {
            return;
        }

        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(!capsule.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
    }

    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
    }

    public void flip()
    {
        if (!isAlive)
        {
            return;
        }

        isfacingright = !isfacingright;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!isAlive)
        {
            return;
        }
        horizontal = context.ReadValue<Vector2>().x;

        if ( horizontal == 0f)
        {
            anim.SetBool("isrunning", false);
        }
        else
        {
            anim.SetBool("isrunning", true);
            anim.SetBool("isclimbing", false);
        }
    }
    void climbladder()
    {
        if(!capsule.IsTouchingLayers(LayerMask.GetMask("climbing")))
        {
            anim.SetBool("isclimbing", false);
            rb.gravityScale = gravityscaleAtStart;
            return;
        }
        else
        {
            anim.SetBool("isclimbing", true);
        }

        Vector2 climbvelocity = new Vector2(rb.velocity.x, rb.gravityScale * climbSpeed);
        rb.velocity = climbvelocity;

    }

    void Die()
    {
        if (capsule.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
                FindObjectOfType<Gamesession>().ScoreReset();
                rb.velocity = deadKick;
                isAlive = false;
                anim.SetTrigger("Dying");
                anim.SetBool("isrunning", false);
                FindObjectOfType<Gamesession>().PlayerDeath();
        }
    }
}
