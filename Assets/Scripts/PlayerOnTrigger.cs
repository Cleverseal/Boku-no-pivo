using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnTrigger : MonoBehaviour {

    public bool OnEnter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnEnter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnEnter = false;
    }
}
