using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 20f;
    public float force = 200f;

    public int jumpFrames = 5;
    private int currentJumpFrames = 0;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteR;

    private bool mooving;
    private bool isGrounded = false;
	private bool krodet_sya;

    private float moveX;
    private float groundRadius = 0.2f;
    private float mass;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D> () ;
        animator = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        mass = rb.mass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		playerMoving ();
		PlayerJumping ();
		rb.MovePosition(rb.position + new Vector2( speed * moveX, force* Mathf.Sign(currentJumpFrames--))* Time.deltaTime);

	}
    
    private void playerMoving() {
        moveX = Input.GetAxis("Horizontal");
		if (moveX != 0 )
        {
			if (mooving == false && isGrounded)
            {
				if (isGrounded) krodet_sya =true;
                mooving = true;
            }
            if (moveX > 0) spriteR.flipX = false;
            else spriteR.flipX = true;
        }
		else if (mooving == true || !isGrounded)
        {
			krodet_sya = false;
            mooving = false;
        }
		animator.SetBool("dekuKrodet`sya",krodet_sya);
    }


    private void PlayerJumping() {
		if (isGrounded) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				currentJumpFrames = jumpFrames;
				rb.mass = 0f;
			} else
				rb.mass = mass;
		}

    }
    
}
