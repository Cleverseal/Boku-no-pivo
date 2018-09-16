using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {
    // Use this for initialization
    public float speed;
    float angle;
    Vector3 axis;
    Vector3 moveVector;
    Rigidbody2D rb;
	void Start() {
        rb = GetComponent<Rigidbody2D>();

        transform.rotation.ToAxisAngle(out axis,out angle);
        moveVector = new Vector2(Mathf.Cos(angle) * Mathf.Sign(axis.z), Mathf.Sin(angle))*Mathf.Sign(axis.z);
        
	}
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveVector * speed * Time.deltaTime + new KekHandler(kek => { return kek*2; })(axis) );
    }
    delegate Vector3 KekHandler(Vector3 kekVector);
}
