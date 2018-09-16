using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerController : MonoBehaviour
    {


        public float speed = 20f;
        public float forse = 200f;

        public int jumpFrames = 5;
        private int currentJumpFrames = 0;

        public Transform CornerCheckTransform;
        public Transform GroundCheckLeft;
        public Transform GroundCheckRight;
        public LayerMask WhatIsGround;
        private float groundRadius = 0.2f;

        private Rigidbody2D rb;
        private Animator animator;

        private bool mooving;
        private bool isGrounded = false;
        private bool frezemoving = false;
        private bool[] allowedJumps = { false, false };

        private Vector3 forward = Vector3.forward;
        private float moveX;
        private float moveY;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            getMoveX();
            getMoveY();
        }

        void FixedUpdate()
        {
            MoveAnalize();
            if(!!isGrounded) CornerCheck();
            if (!frezemoving)
            {
                MoveAnimate();
                Move();
            }
        }

        private void CornerCheck()
        {
            RaycastHit2D сornerUpHit = Physics2D.Raycast(CornerCheckTransform.position, new Vector3(0, 1, 0), 10f);
            RaycastHit2D сornerDownHit = Physics2D.Raycast(CornerCheckTransform.position, new Vector3(0, -1, 0), 5f);
            if ((сornerDownHit.collider != null && сornerDownHit.collider.tag == "Ground") &&
                (сornerUpHit.collider == null || сornerUpHit.collider.tag != "Ground"))
            {
                StartCoroutine(FrezeMoving(1f));
                transform.position = transform.position + new Vector3(2*forward.x, 5,5f-сornerDownHit.distance);
            }

        }

        private IEnumerator FrezeMoving(float seconds)
        {
            frezemoving = true;
            yield return new
                WaitForSeconds(seconds);
            Debug.Log("Kekosiki");
            frezemoving = false;
        }

        private void Move()
        {
            rb.MovePosition(rb.position + new Vector2(speed * moveX * Time.deltaTime, forse * moveY * Time.deltaTime));
        }

        private void MoveAnalize()
        {
            moveXAnalize();
            moveYAnalize();
        }

        private void MoveAnimate()
        {
            moveXAnimate();
            moveYAnimate();
        }

        private void getMoveX()
        {
            moveX = Input.GetAxis("Horizontal");
        }

        private void moveXAnalize()
        {
            if (moveX != 0)
            {
                
                if (mooving == false)
                {
                    mooving = true;
                }
                if (moveX > 0)
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                else transform.rotation = new Quaternion(0, -180, 0, 1);
                forward.x = Mathf.Sign(transform.rotation.y);
            }
            else if (mooving == true)
            {
                mooving = false;
            }
        }

        private void moveXAnimate()
        {
            animator.SetBool("dekuKrodet`sya", mooving & isGrounded);
        }

        private float getMoveY()
        {
            calculateJumpFrames();
            if (currentJumpFrames > 0)
            {
                currentJumpFrames--;
                moveY = 1;
            }
            else if (!isGrounded)
            {
                moveY = -1;
            }
            else
            {
                moveY = 0;
            }
            return moveY;
        }

        private void calculateJumpFrames()
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheckLeft.position, groundRadius, WhatIsGround) ||
                Physics2D.OverlapCircle(GroundCheckRight.position, groundRadius, WhatIsGround);
            if (isGrounded) { allowedJumps[0] = true; allowedJumps[1] = true; }
            else { allowedJumps[0] = false; }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (allowedJumps[0] == true)
                {
                    currentJumpFrames = jumpFrames;
                    allowedJumps[0] = false;
                }
                else if (allowedJumps[1] == true)
                {
                    currentJumpFrames = jumpFrames;
                    allowedJumps[1] = false;
                }
            }
        }

        private void moveYAnimate()
        {
            animator.SetInteger("moveY", (int)moveY);
        }

        private void moveYAnalize()
        {

        }

    }
}