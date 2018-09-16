using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public abstract class MoovingEnemy : Enemy
    {
        public float speedX = 30f;
        public float slowingCoof = 0.7f;

        protected float currentSpeed;
        protected float playerSpeed;

        protected float moveX { get { return Mathf.Sign(deltaX); } }
        protected float moveY { get { return Mathf.Sign(deltaY); } }
        
        protected bool fasterThenPlayer;
        protected bool onStart;

        public GameObject ProtectedArea;
        protected PlayerOnTrigger protectedAreaChecker;
        protected bool protectedAreaEngaged;
        
        protected bool AreaEngaged
        {
            get
            {
                return checkProtectedArea();
            }
        }

        protected Vector3 startPosition;
        protected Rigidbody2D rb;

        protected void MovingEnemyStart()
        {
            EnemyStart();
            rb = GetComponent<Rigidbody2D>();
            startPosition = transform.position;
            protectedAreaChecker = ProtectedArea.GetComponent<PlayerOnTrigger>();
            currentSpeed = speedX;
        }

        protected void PatrulArea()
        {
            checkProtectedArea();
            if (protectedAreaEngaged)
            {
                checkAtach();
                if (atachedOnPlayer)
                {
                    onStart = false;
                    analyzePlayerTransform();
                    chasingPlayer();
                    atackPlayer();
                }
            }
            else if (!onStart)
            {
                setDeltaX(startPosition);
                returnToPosition();
            }
            fliping();
        }


        protected bool checkProtectedArea()
        {
            return protectedAreaEngaged = (protectedAreaChecker == null) ? true : protectedAreaChecker.OnEnter;
        }

        protected void AnalyzePlayerSpeed()
        {
            playerSpeed = Player.GetComponent<PlayerController>().speed;
            fasterThenPlayer = (speedX > playerSpeed * slowingCoof) ? true : false;
        }

        protected void chasingPlayer()
        {
            if (!playerNear)
            {
                if (playerReached)
                {
                    currentSpeed = 0f;
                    playerNear = true;
                }
                else if (playerAllmostReached & fasterThenPlayer)
                {
                    currentSpeed = playerSpeed * slowingCoof;
                }
                else currentSpeed = speedX;
                rb.MovePosition(rb.position + new Vector2(currentSpeed * moveX, 0) * Time.deltaTime);
            }
            else playerNear = (Mathf.Abs(deltaX) > borderOfReaching * nearCoof) ? false : true;
        }
        protected void returnToPosition()
        {
            if (Mathf.Abs(deltaX) > 1f)
            {
                currentSpeed = 0.5f * speedX;
                rb.MovePosition(rb.position + new Vector2(currentSpeed * moveX, 0) * Time.deltaTime);
            }
            else
            {
                onStart = true;
                currentSpeed = speedX;
            }

        }
    }
}
