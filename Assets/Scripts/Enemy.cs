using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public abstract class Enemy : MonoBehaviour
    {

        public int health = 1;

        public float speedX = 30f;

        private float deltaX;
        private float deltaY;

        public float borderOfReaching = 1f;
        public float slowingCoof = 0.7f;

        private float currentSpeed;
        private float nearCoof = 2f;
        private float almostReachedCoof = 4f;

        private bool fliped;

        public GameObject Player;

        private float playerSpeed;

        private bool playerReached;
        private bool playerNear;
        private bool playerAllmostReached;
        private bool fasterThenPlayer;
        private bool atachedOnPlayer;
        private bool protectedAreaEngaged;
        private bool onStart;

        public GameObject AtachedRange;
        private PlayerOnTrigger atachChecker;

        public GameObject ProtectedArea;
        private PlayerOnTrigger protectedAreaChecker;

        private Vector3 startPosition;
        private Rigidbody2D rb;
        private SpriteRenderer spriteR;
        // Use this for initialization

        protected bool Atached
        {
            get
            {
                return checkAtach();
            }
        }
        protected bool AreaEngaged
        {
            get
            {
                return checkProtectedArea();
            }
        }

        private float moveX { get { return Mathf.Sign(deltaX); } }
        private float moveY { get { return Mathf.Sign(deltaY); } }

        protected void EnemyStart()
        {
            Player = GameObject.Find("Player");
            rb = GetComponent<Rigidbody2D>();
            spriteR = GetComponent<SpriteRenderer>();
            startPosition = transform.position;
            atachChecker = AtachedRange.GetComponent<PlayerOnTrigger>();
            protectedAreaChecker = ProtectedArea.GetComponent<PlayerOnTrigger>();
            fliped = spriteR.flipX;
            playerSpeed = Player.GetComponent<PlayerController>().speed;
            print(Player);
            currentSpeed = speedX;
        }

        protected void AnalyzePlayerTransform()
        {
            setDeltaX(Player.transform.position);
            setDeltaY(Player.transform.position);
            playerReachedCheck();
            getPlayerSpeed();
            chasingPlayer();
        }

        protected void ProtectingArea()
        {
            checkProtectedArea();
            if (protectedAreaEngaged)
            {
                checkAtach();
                if (atachedOnPlayer)
                {
                    onStart = false;
                    AnalyzePlayerTransform();
                    chasingPlayer();
                }
            }
            else if (!onStart)
            {
                setDeltaX(startPosition);
                returnToPosition();
            }
            fliping();
        }

        private bool checkProtectedArea()
        {
            return protectedAreaEngaged = protectedAreaChecker.OnEnter;
        }

        private bool checkAtach()
        {
            Vector3 LaserStartPosition = transform.position;
            return atachedOnPlayer = atachChecker.OnEnter;
        }

        private void setDeltaX(Vector3 position)
        {
            deltaX = position.x - transform.position.x;
        }

        private void setDeltaY(Vector3 position)
        {
            deltaY = position.y - transform.position.y;
        }

        private void playerReachedCheck()
        {
            playerReached = (Mathf.Abs(deltaX) > borderOfReaching) ? false : true;
            playerAllmostReached = (Mathf.Abs(deltaX) > borderOfReaching * almostReachedCoof) ? false : true;
        }

        private void getPlayerPosition()
        {
            setDeltaX(Player.transform.position);
            playerReached = (Mathf.Abs(deltaX) > borderOfReaching) ? false : true;
            playerAllmostReached = (Mathf.Abs(deltaX) > borderOfReaching * almostReachedCoof) ? false : true;
        }

        private void getPlayerSpeed()
        {
            playerSpeed = Player.GetComponent<PlayerController>().speed;
            fasterThenPlayer = (speedX > playerSpeed * slowingCoof) ? true : false;
        }

        private void returnToPosition()
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

        private void chasingPlayer()
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

        private void fliping()
        {
            float y;
            if (moveX > 0) y = System.Convert.ToInt32(!fliped) * 180;
            else y = System.Convert.ToInt32(fliped) * 180;
            transform.rotation = new Quaternion(0, y, 0, 1);
        }
    }
}