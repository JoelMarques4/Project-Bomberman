using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] Animator animator;

    enum Movement
    {
        Horizontal,
        Vertical,
        IsWalking,
        Left,
        Right,
        Up,
        Down
    }
    enum Anim
    {
        Player_IdleLeft,
        Player_IdleRight,
        Player_IdleUp,
        Player_IdleDown,
        Player_WalkLeft,
        Player_WalkRight,
        Player_WalkUp,
        Player_WalkDown,
    }

    private void Start()
    {
        Player.OnMove += Player_OnMove;
        Player.OnStopMove += Player_OnStopMove;
    }

    private void Player_OnMove(object sender, Player.OnMoveEventArgs e)
    {
        Vector3 moveDir = e.moveDir;

        if (e.right == true && e.up == false && e.down == false)
        {
            animator.Play(Anim.Player_WalkRight.ToString());
        }
        if (e.left == true && e.up == false && e.down == false)
        {
            animator.Play(Anim.Player_WalkLeft.ToString());
        }
        if (e.up == true || e.up == true && e.right == true || e.up == true && e.left == true)
        {
            animator.Play(Anim.Player_WalkUp.ToString());
        }
        if (e.down == true || e.down == true && e.right == true || e.down == true && e.left == true)
        {
            animator.Play(Anim.Player_WalkDown.ToString());
        }
    }

    private void Player_OnStopMove(object sender, Player.OnStopEventArgs e)
    {
        Vector3 moveDir = e.moveDir;
        if (e.right == true && e.up == false && e.down == false)
        {
            animator.Play(Anim.Player_IdleRight.ToString());
        }
        if (e.left == true && e.up == false && e.down == false)
        {
            animator.Play(Anim.Player_IdleLeft.ToString());
        }
        if (e.up == true || e.up == true && e.right == true || e.up == true && e.left == true)
        {
            animator.Play(Anim.Player_IdleUp.ToString());
        }
        if (e.down == true || e.down == true && e.right == true || e.down == true && e.left == true)
        {
            animator.Play(Anim.Player_IdleDown.ToString());
        }
    }
}
