using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public abstract class Enemy : MonoBehaviour
    {

        public int health = 1;
        protected float deltaX;
        protected float deltaY;

        [SerializeField] protected float borderOfReaching = 1f;
        protected float nearCoof = 2f;
        protected float almostReachedCoof = 4f;


        protected bool atachedOnPlayer;
        protected bool playerReached;
        protected bool playerNear;
        protected bool playerAllmostReached;
        protected bool fliped;

        protected GameObject Player;

        [SerializeField] protected GameObject AtachedRange;
        protected PlayerOnTrigger atachChecker;

        protected SpriteRenderer spriteR;
        // Use this for initialization

        protected bool Atached
        {
            get
            {
                return checkAtach();
            }
        }

        public abstract void atackPlayer();
        protected void EnemyStart()
        {
            Player = GameObject.Find("Player");
            spriteR = GetComponent<SpriteRenderer>();
            atachChecker = AtachedRange.GetComponent<PlayerOnTrigger>();
            fliped = spriteR.flipX;
            print(Player);
        }

        protected bool checkAtach()
        {
            Vector3 LaserStartPosition = transform.position;
            return atachedOnPlayer = atachChecker.OnEnter;
        }

        protected float setDeltaX(Vector3 position)
        {
            deltaX = position.x - transform.position.x;
            return deltaX;
        }

        protected float setDeltaY(Vector3 position)
        {
            deltaY = position.y - transform.position.y;
            return deltaY;
        }


        protected void analyzePlayerTransform()
        {
            setDeltaX(Player.transform.position);
            setDeltaY(Player.transform.position);
            playerReachedCheck();
        }
        protected void playerReachedCheck()
        {
            playerReached = (Mathf.Abs(deltaX) > borderOfReaching) ? false : true;
            playerAllmostReached = (Mathf.Abs(deltaX) > borderOfReaching * almostReachedCoof) ? false : true;
        }

        protected void fliping()
        {
            float y;
            if (setDeltaX(Player.transform.position) > 0) y = System.Convert.ToInt32(!fliped) * 180;
            else y = System.Convert.ToInt32(fliped) * 180;
            transform.rotation = new Quaternion(0, y, 0, 1);
        }
    }
}