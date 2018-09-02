using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//82931
namespace BokuNoPivo
{
    public class RoboOwl : Enemy
    {
        public GameObject LaserPrefab;
        public Transform LaserSpot;

        private bool reloading = false;
        // Use this for initialization
        void Start()
        {
            EnemyStart();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ProtectingArea();
            StartCoroutine(LaserAtacking());
        }

        private IEnumerator LaserAtacking()
        {
            if (Atached && !reloading)
            {
                reloading = true;
                float deltaX = Player.transform.position.x - LaserSpot.position.x;
                float deltaY = Player.transform.position.y - LaserSpot.position.y;
                Vector2 vectorToPlayer = new Vector2(deltaX, deltaY);
                float angle = Vector2.Angle(vectorToPlayer, Vector2.left) * Mathf.Sign(deltaY) * -1f;
                Instantiate(LaserPrefab, LaserSpot.position, Quaternion.AngleAxis(angle, Vector3.forward));
                yield return new
                   WaitForSeconds(1.5f);
                reloading = false;
            }
        }
    }
}

