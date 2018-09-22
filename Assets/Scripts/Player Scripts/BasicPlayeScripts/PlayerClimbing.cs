using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerClimbing : MonoBehaviour
    {
        public event PlayerEventHandler playerIsClimbing;
        private bool climbed = false;
        [SerializeField] private Transform CornerCheckTransform;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!PlayerInfo.PlayerController.IsGrounded && !PlayerInfo.PlayerController.FrezeMoving) CornerCheck();
        }
        private void CornerCheck()
        {
            RaycastHit2D cornerUpHit = Physics2D.Raycast(CornerCheckTransform.position, 
                new Vector3(0, 1, 0), 10f, PlayerInfo.WhatIsGround);
            RaycastHit2D cornerDownHit = Physics2D.Raycast(CornerCheckTransform.position, 
                new Vector3(0, -1, 0), 5f, PlayerInfo.WhatIsGround);
            if ((cornerDownHit.collider != null && cornerDownHit.collider.tag == "Ground") &&
                (cornerUpHit.collider == null || cornerUpHit.collider.tag != "Ground"))
            {
                StartCoroutine(CornerClimb(cornerDownHit));
            }

        }

        private IEnumerator CornerClimb(RaycastHit2D cornerDownHit)
        {
            PlayerInfo.PlayerController.FrezeMoving = true;
            playerIsClimbing();
            transform.position = new Vector3(transform.position.x + 0f, cornerDownHit.point.y + 0.5f);
            yield return new WaitUntil(() => { return climbed; });
            transform.position = new Vector3(cornerDownHit.point.x + 1f * PlayerInfo.PlayerController.playerDirection,
                   transform.position.y + 1.5f);
            climbed = false;
            PlayerInfo.PlayerController.FrezeMoving = false;
        }


        private void _climbed()
        {
            climbed = true;
        }
    }
}