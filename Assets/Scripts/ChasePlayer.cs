using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private float Kx = 1, Ky = 1;
    float dY, dX, newy, newx,
        prevPlayerX, prevPlayerY,
        playerX, playerY, zero; 
	// Use this for initialization
	void Start () {
        prevPlayerX = player.transform.position.x;
        prevPlayerY = player.transform.position.y;
        zero = newx = transform.position.x;
        newy = transform.position.y;
    }

	void Update () {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;

        dX = playerX - prevPlayerX;
        dY = playerY - prevPlayerY;
        if (player.transform.position.x > 0) newx = transform.position.x + dX * Kx ;
		else newx = zero;
        newy = transform.position.y + dY * Ky;
        transform.position = new Vector3(newx, newy, transform.position.z);
        prevPlayerX = playerX;
        prevPlayerY = playerY;
	}
}
