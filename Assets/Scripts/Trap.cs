using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    [SerializeField] private float x = 0;
    [SerializeField] private float y = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Teleport")
        {
            if (collision.tag == "Player") collision.gameObject.transform.position = new Vector2(x, y);
        }
    }
}
