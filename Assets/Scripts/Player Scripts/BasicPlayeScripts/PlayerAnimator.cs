using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo{
    public class PlayerAnimator : MonoBehaviour {
        private void Start()
        {
            PlayerInfo.PlayerClimbing.playerIsClimbing += ClimbAnimate;
        }
        void LateUpdate() {
            MoveAnimate();
        }

        private void ClimbAnimate()
        {
            PlayerInfo.Animator.SetTrigger("climbing");
        }
        private void MoveAnimate()
        {
            moveXAnimate();
            moveYAnimate();
        }

        private void moveXAnimate()
        {
            PlayerInfo.Animator.SetBool("dekuKrodet`sya", (PlayerInfo.PlayerController.MoveX != 0) & PlayerInfo.PlayerController.IsGrounded);
        }

        private void moveYAnimate()
        {
            PlayerInfo.Animator.SetInteger("moveY", (int)PlayerInfo.PlayerController.MoveY);
        }
    }
}