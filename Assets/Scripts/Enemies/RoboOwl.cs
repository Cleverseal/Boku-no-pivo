using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//82931
namespace BokuNoPivo
{
    public class RoboOwl : WatcherEnemy
    {

        [SerializeField] protected float reloadDuration = 1;
        [SerializeField] protected GameObject LaserPrefab;
        [SerializeField] protected Transform LaserSpot;

        private ParticleSystem ps;
        private bool reloading = false;
        private bool charging = false;
        // Use this for initialization
        void Start()
        {
            EnemyStart();
            ps = LaserSpot.GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            lookOutArea();
        }

        override public void atackPlayer()
        {
            if (!reloading & !charging)
            {
                StartCoroutine(charge());
                float deltaX = Player.transform.position.x - LaserSpot.position.x;
                float deltaY = Player.transform.position.y - LaserSpot.position.y;
                Vector2 vectorToPlayer = new Vector2(deltaX, deltaY);
                float angle = Vector2.Angle(vectorToPlayer, Vector2.right) * Mathf.Sign(deltaY);
                Instantiate(LaserPrefab, LaserSpot.position, Quaternion.AngleAxis(angle, Vector3.forward));
                StartCoroutine(reload());
            }
        }
        private IEnumerator charge()
        {
            charging = true;
            ps.Play();
            yield return new
               WaitForSeconds(ps.main.duration + ps.main.startLifetime.constant - 0.2f);
            charging = false;
        }
        private IEnumerator reload()
        {
            reloading = true;
            yield return new
                    WaitForSeconds(reloadDuration);
            reloading = false;
        }
    }
}

