using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBackGround : MonoBehaviour
{

    public GameObject Player;
    public GameObject Left;
    public GameObject Right;
    private GameObject left, middle, right, buf1, buf2;
    float length;
    void Start()
    {
        middle = gameObject;
        left = Left;
        right = Right;
        length = transform.position.x - left.transform.position.x;
        switchOnRight();
    }
    void Update()
    {
        if (Mathf.Abs(middle.transform.position.x - Player.transform.position.x) >
            Mathf.Abs(right.transform.position.x - Player.transform.position.x)) switchOnRight();
        else if (Mathf.Abs(middle.transform.position.x - Player.transform.position.x) >
            Mathf.Abs(left.transform.position.x - Player.transform.position.x)) switchOnLeft();
    }

    private void switchOnRight()
    {
        left.transform.position = new Vector3(left.transform.position.x + 3 * length,
            left.transform.position.y, left.transform.position.z);
        SpriteRenderer spr= left.GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX; 
        buf1 = left;
        left = middle;
        buf2 = right;
        right = buf1;
        middle = buf2;
    }

    private void switchOnLeft()
    {
        right.transform.position = new Vector3(right.transform.position.x - 3 * length,
            right.transform.position.y, right.transform.position.z);
        SpriteRenderer spr = right.GetComponent<SpriteRenderer>();
        spr.flipX = !spr.flipX;
        buf1 = right;
        right = middle;
        buf2 = left;
        left = buf1;
        middle = buf2;
    }
}
