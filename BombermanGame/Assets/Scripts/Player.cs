using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] PlayerInputActions playerInputActions;
    [SerializeField] private int speedPowerUps = 1;
    public static event EventHandler<OnMoveEventArgs> OnMove;
    public static event EventHandler<OnStopEventArgs> OnStopMove;
    private Vector3 lastDir;
    private bool left = false;
    private bool right = false;
    private bool up = false;
    private bool down = true;
    private bool isMoving = false;

    public class OnMoveEventArgs : EventArgs
    {
        public Vector3 moveDir;
        public bool left;
        public bool right;
        public bool up;
        public bool down;

    }
    public class OnStopEventArgs : EventArgs
    {
        public Vector3 moveDir;
        public bool left;
        public bool right;
        public bool up;
        public bool down;
        public bool isMoving;
    }

    private void Awake()
    {

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();


    }

    private void FixedUpdate()
    {
        HandleMovement();

    }

    public void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        float speedPowerUpStrenght = speedPowerUps / 5f;
        float moveDistance = (moveSpeed + speedPowerUpStrenght) * Time.deltaTime;
        transform.position += moveDir * moveDistance;
        if (moveDir != Vector3.zero)
        {

            if (moveDir.y < 0)
            {
                down = true;
                up = false;
            }
            else if (moveDir.y > 0)
            {
                up = true;
                down = false;
            }
            if (moveDir.x < 0)
            {
                if (moveDir.y == 0)
                {
                    up = false;
                    down = false;
                }
                left = true;
                right = false;
            }
            else if (moveDir.x > 0)
            {
                if (moveDir.y == 0)
                {
                    up = false;
                    down = false;
                }
                right = true;
                left = false;
            }


            OnMove?.Invoke(this, new OnMoveEventArgs { moveDir = moveDir, down = down, up = up, left = left, right = right });
        }
        else
        {

            OnStopMove?.Invoke(this, new OnStopEventArgs { moveDir = lastDir, down = down, up = up, left = left, right = right, isMoving = isMoving });

        }
    }



}