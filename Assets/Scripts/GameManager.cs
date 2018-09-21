using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instanse = null;

        private static GameObject player;
        private static PlayerHpManager playerHpManager;
        public static GameObject Player { get { return player; } }
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

        private void Start()
        {
            
        }
        private void InitializeManager()
        {
            player = GameObject.Find("Player");
            playerHpManager = player.GetComponent<PlayerHpManager>();
            playerHpManager.PlayerHasDied += closeScene;
            Debug.Log("kek");
        }

        private void closeScene()
        {
            Debug.Log("kek");
        }

    }

}