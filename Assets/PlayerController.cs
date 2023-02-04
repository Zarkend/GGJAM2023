using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private InputAction Left;

    [SerializeField]
    private float verticalMoveSpeed;

    [SerializeField]
    private float horizontalMoveSpeed;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float jumpDuration;

    [SerializeField]
    private Ease jumpEase;

    private bool _isJumping;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CustomMovement();

        Debug.Log(Left.IsPressed());
    }

    private float groundedTimer;
    private float verticalVelocity;
    [SerializeField]
    private float gravityValue = 9.81f;

    void CustomMovement()
    {
        bool groundedPlayer = _characterController.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        // slam into the ground
        if (groundedPlayer && verticalVelocity < 0)
        {
            // hit ground
            verticalVelocity = 0f;
        }

        // apply gravity always, to let us track down ramps properly
        verticalVelocity -= gravityValue * Time.deltaTime;


        // gather lateral input control
        Vector3 move = Vector3.zero;
        //new Vector3((Input.GetAxis("Horizontal") * Time.deltaTime * horizontalMoveSpeed), 0, (Input.GetAxis("Vertical") * Time.deltaTime * horizontalMoveSpeed));

        // scale by speed
        move *= horizontalMoveSpeed;

        // only align to motion if we are providing enough input
        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        // allow jump as long as the player is on the ground
        //if (Input.GetButtonDown("Jump"))
        //{
        //    // must have been grounded recently to allow jump
        //    if (groundedTimer > 0)
        //    {
        //        // no more until we recontact ground
        //        groundedTimer = 0;

        //        // Physics dynamics formula for calculating jump up velocity based on height and gravity
        //        verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        //    }
        //}

        // inject Y velocity before we use it
        move.y = verticalVelocity;

        move.z += -Section.MoveSpeed;

        // call .Move() once only
        _characterController.Move(move * Time.deltaTime);
    }

    private void OnJumpCompleted()
    {
        _isJumping = false;
    }
}
