using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerInfo: MonoBehaviour
    {
        public static PlayerInfo instanse = null;
        [SerializeField] private LayerMask serializeWhatIsGround;
        [SerializeField] private static LayerMask whatIsGround;
        private static GameObject player;
        private static PlayerHpManager playerHpManager;
        private static PlayerController playerController;
        private static PlayerClimbing playerClimbing;
        private static Rigidbody2D rigidBody2D;
        private static Animator animator;
        public static GameObject Player { get { return player; } }
        public static LayerMask WhatIsGround { get { return whatIsGround; } }
        public static PlayerHpManager PlayerHpManager { get { return playerHpManager; } }
        public static PlayerController PlayerController { get { return playerController; } }
        public static PlayerClimbing PlayerClimbing { get { return playerClimbing; } }
        public static Rigidbody2D RigidBody2D { get { return rigidBody2D; } }
        public static Animator Animator {get { return animator; } }
        private void OnEnable()
        {
            if (instanse = null)
            {
                instanse = this;
            }
            else if (instanse == this)
            {
                Destroy(gameObject);
            }
            InitializeManager();
        }
        private void InitializeManager()
        {
            player = GameObject.Find("Player");
            playerHpManager = player.GetComponent<PlayerHpManager>();
            playerController = player.GetComponent<PlayerController>();
            playerClimbing = player.GetComponent<PlayerClimbing>();
            rigidBody2D = player.GetComponent<Rigidbody2D>();
            animator = player.GetComponent<Animator>();
            playerHpManager.PlayerDied += closeScene;
            whatIsGround = serializeWhatIsGround;
            Debug.Log((int)whatIsGround);
        }

        private void closeScene()
        {
        }

    }

    public delegate void PlayerEventHandler();

}