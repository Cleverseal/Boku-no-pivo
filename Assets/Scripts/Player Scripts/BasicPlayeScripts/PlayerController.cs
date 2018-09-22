using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        public float Speed { get { return speed; } }
        [SerializeField] private float forse = 200f;

        [SerializeField] private int jumpFrames = 5;
        private int currentJumpFrames = 0;
        [SerializeField] private Transform GroundCheckLeft;
        [SerializeField] private Transform GroundCheckRight; 

        private float groundRadius = 0.2f; 
        private bool mooving;
        private bool isGrounded = false;
        private bool frezemoving = false;
        private bool[] allowedJumps = { false, false };
        public bool FrezeMoving { get { return frezemoving; } set { frezemoving = value; } }
        public bool IsGrounded { get { return isGrounded; } }
        private Vector3 forward = Vector3.forward;
        public float playerDirection { get { return forward.x; } }

        private float moveX;
        private float moveY;
        public float MoveX { get { return moveX; } }
        public float MoveY { get { return moveY; } }

        void Start()
        {
        }

        private void Update()
        {
            getMoveX();
            getMoveY();
        }

        void FixedUpdate()
        {
            if (!frezemoving)
            {
                flipping();
                Move();
            }
        }

        private void Move()
        {
            PlayerInfo.RigidBody2D.MovePosition(
                PlayerInfo.RigidBody2D.position + 
                new Vector2(speed * moveX * Time.deltaTime, forse * moveY * Time.deltaTime));
        }

        private void getMoveX()
        {
            moveX = Input.GetAxis("Horizontal");
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

        private void calculateJumpFrames()
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheckLeft.position, groundRadius, PlayerInfo.WhatIsGround) ||
                Physics2D.OverlapCircle(GroundCheckRight.position, groundRadius, PlayerInfo.WhatIsGround);
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

    }
}