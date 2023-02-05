using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
}
public class Goblin : MonoBehaviour, IInteractable
{
    public event Action<IInteractable> Destroyed;

    [SerializeField]
    private int hits;

    [SerializeField]
    private HealthBar nameplatePrefab;

    [SerializeField]
    private Transform nameplatePosition;

    HealthBar _nameplateInstance;

    public MoveDirection MoveDirection = MoveDirection.Left;

    [SerializeField]
    private float moveSpeed;

    private bool _isMoving = true;
    private bool _isAttacking = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip deadClip;

    [SerializeField]
    private AudioClip carretillaHit;

    // Start is called before the first frame update
    void Start()
    {
        _nameplateInstance = Instantiate(nameplatePrefab, GameObject.Find("Nameplates").transform);

        _nameplateInstance.Initialize(hits);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacking)
            return;

        _nameplateInstance.transform.position = Camera.main.WorldToScreenPoint(nameplatePosition.transform.position);

        if (_isMoving)
        {
            if (MoveDirection == MoveDirection.Left)
            {
                transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }

        }

        animator.SetBool("isMoving", _isMoving);

    }

    private void Attack()
    {
        GameObject.Find("SFXAudioSource").GetComponent<AudioSource>().PlayOneShot(carretillaHit);

        _carretilla.Attack(1);
    }

    public void Interact()
    {
        hits -= 1;

        _nameplateInstance.SetHealth(hits);

        if (hits <= 0)
        {
            Destroyed?.Invoke(this);

            GameObject.Find("SFXAudioSource").GetComponent<AudioSource>().PlayOneShot(deadClip);

            Destroy(_nameplateInstance.gameObject);
            Destroy(gameObject);
        }
    }

    private Carretilla _carretilla;

    private void OnTriggerEnter(Collider other)
    {
        var carretilla = other.GetComponent<Carretilla>();

        if (carretilla != null)
        {
            _carretilla = carretilla;
            _isMoving = false;
            _isAttacking = true;

            animator.SetBool("isHitting", true);
            InvokeRepeating(nameof(Attack), 1f, 1f);
        }
    }
}
