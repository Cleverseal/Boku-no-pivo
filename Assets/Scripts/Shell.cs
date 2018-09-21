using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class Shell : MonoBehaviour
    {
        // Use this for initialization
        [SerializeField] private float speed;
        [SerializeField] uint damage = 1; 
        float angle;
        Vector3 axis;
        Vector3 moveVector;
        Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            transform.rotation.ToAxisAngle(out axis, out angle);
            moveVector = new Vector2(Mathf.Cos(angle) * Mathf.Sign(axis.z), Mathf.Sin(angle)) * Mathf.Sign(axis.z);

        }
        private void FixedUpdate()
        {
            rb.MovePosition(transform.position + moveVector * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHpManager>().GetDamage(damage); ;
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Destroy(this);
        }
    }
}
