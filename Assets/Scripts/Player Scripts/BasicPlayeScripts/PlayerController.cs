using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerController : MonoBehaviour
    {


        public float speed = 20f;
        [SerializeField] private float forse = 200f;

        [SerializeField] private int jumpFrames = 5;
        private int currentJumpFrames = 0;

        [SerializeField] private Transform CornerCheckTransform;
        [SerializeField] private Transform GroundCheckLeft;
        [SerializeField] private Transform GroundCheckRight;
        [SerializeField] private LayerMask WhatIsGround;
        private float groundRadius = 0.2f;

        private Rigidbody2D rb;
        private Animator animator;

        private bool mooving;
        private bool isGrounded = false;
        private bool frezemoving = false;
        private bool[] allowedJumps = { false, false };
        private bool climbed = false;

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
            if (!frezemoving)
            {
                if (!isGrounded) CornerCheck();
                flipping();
                MoveAnimate();
                Move();
            }
        }

        private void CornerCheck()
        {
            RaycastHit2D cornerUpHit = Physics2D.Raycast(CornerCheckTransform.position, new Vector3(0, 1, 0), 10f, WhatIsGround);
            RaycastHit2D cornerDownHit = Physics2D.Raycast(CornerCheckTransform.position, new Vector3(0, -1, 0), 5f, WhatIsGround);
            if ((cornerDownHit.collider != null && cornerDownHit.collider.tag == "Ground") &&
                (cornerUpHit.collider == null || cornerUpHit.collider.tag != "Ground"))
            {
                StartCoroutine(CornerClimb(cornerDownHit));
            }

        }

        private IEnumerator CornerClimb(RaycastHit2D cornerDownHit)
        {
            frezemoving = true;
            transform.position = new Vector3(transform.position.x + 0f, cornerDownHit.point.y + 0.5f);
            animator.SetTrigger("climbing");

            yield return new
                             WaitUntil (() => { return climbed; });
            transform.position = new Vector3(cornerDownHit.point.x + 1f * forward.x,
                   transform.position.y + 1.5f);
            transform.Translate(0f, 0f,0f);
            climbed = false;
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

        private void flipping()
        {
            if (moveX != 0)
            {
                if (moveX > 0)
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                else transform.rotation = new Quaternion(0, -180, 0, 1);
                forward.x = Mathf.Sign(transform.rotation.y);
            }
        }

        private void moveXAnalize()
        {
            if (moveX != 0)
            {
                mooving = true;
            }
            else
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
                    Debug.Log(isGrounded);
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
        
        private void _climbed()
        {
            climbed = true;
        }
    }
}