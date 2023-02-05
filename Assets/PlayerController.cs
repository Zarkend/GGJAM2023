using Assets.Scripts;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float verticalMoveSpeed;

    [SerializeField]
    private float horizontalMoveSpeed;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private float jumpDuration;

    private CharacterController _characterController;

    private Gamepad _gamePad;

    [SerializeField]
    private bool useGamePad;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _gamePad = Gamepad.current;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float keyboardLeft = Keyboard.current.aKey.isPressed ? -1 : 0;
        float keyboardRight = Keyboard.current.dKey.isPressed ? 1 : 0;

        float keyboardHorizontalAxis = keyboardLeft + keyboardRight;

        float keyboardUp = Keyboard.current.wKey.isPressed ? 1 : 0;
        float keyboardDown = Keyboard.current.sKey.isPressed ? -1 : 0;

        float keyboardVerticalAxis = keyboardUp + keyboardDown;

        horizontalAxis = useGamePad && _gamePad != null ? _gamePad.leftStick.ReadValue().x : keyboardHorizontalAxis;
        verticalAxis = useGamePad && _gamePad != null ? _gamePad.leftStick.ReadValue().y : keyboardVerticalAxis;

        CustomMovement();

        bool interactPressed = useGamePad && _gamePad != null ? _gamePad.bButton.wasPressedThisFrame : Mouse.current.leftButton.wasPressedThisFrame;

        if (interactPressed)
        {
            Debug.Log("Interact pressed");
            _interactables.FirstOrDefault()?.Interact();
            animator.SetTrigger("Hit");
            //_interactables.ForEach(x => x.Interact());
        }

        animator.SetBool("IsJumping", !_characterController.isGrounded);

    }

    private float groundedTimer;
    private float verticalVelocity;
    [SerializeField]
    private float gravityValue = 9.81f;

    private float horizontalAxis;
    private float verticalAxis;

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

        Vector3 move = new Vector3((horizontalAxis * Time.deltaTime * horizontalMoveSpeed), 0, (verticalAxis * Time.deltaTime * horizontalMoveSpeed));

        // scale by speed
        move *= horizontalMoveSpeed;

        // only align to motion if we are providing enough input
        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        bool jumpPressed = useGamePad && _gamePad != null ? _gamePad.aButton.isPressed : Keyboard.current.spaceKey.isPressed;

        // allow jump as long as the player is on the ground
        if (jumpPressed)
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }

        // inject Y velocity before we use it
        move.y = verticalVelocity;

        move.z += -Section.MoveSpeed;

        // call .Move() once only
        _characterController.Move(move * Time.deltaTime);

        animator.SetBool("IsWalking", move != Vector3.zero);
    }

    private List<IInteractable> _interactables = new List<IInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        Debug.Log($"Trigger ended {other.gameObject.name}");

        if (interactable is not null)
        {
            if (_interactables.Contains(interactable))
                return;
            Debug.Log("Interactable added");
            _interactables.Add(interactable);

            interactable.Destroyed += OnInteractableDestroyed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable is not null)
        {
            if (!_interactables.Contains(interactable))
                return;
            _interactables.Remove(interactable);
        }
    }

    private void OnInteractableDestroyed(IInteractable interactable)
    {
        _interactables.Remove(interactable);
    }
}
