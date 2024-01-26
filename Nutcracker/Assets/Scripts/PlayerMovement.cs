using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] GameObject swordR;
    [SerializeField] GameObject swordL;
    [SerializeField] GameObject swordD;

    [SerializeField] private LayerMask jumpableGround;

    private float dirx = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float swordMoveSpeed = 15f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling };

    [SerializeField] private AudioSource jumpSoundEffect;
 
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
        SwordManagement();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirx > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void SwordManagement()
    {
        if (Input.GetKey("left shift") && (Input.GetKey(KeyCode.RightArrow)))
        {
            swordR.SetActive(true);
        }

        else
        {
            swordR.SetActive(false);
        }

        if (Input.GetKey("left shift") && (Input.GetKey(KeyCode.LeftArrow)))
        {
            swordL.SetActive(true);
        }

        else
        {
            swordL.SetActive(false);
        }

        if (!isGrounded() && (Input.GetKey("left shift") && (Input.GetKey(KeyCode.DownArrow))))
        {
            swordD.SetActive(true);
        }

        else
        {
            swordD.SetActive(false);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround );
    }
}
