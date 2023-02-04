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
    private bool _isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        _nameplateInstance = Instantiate(nameplatePrefab, GameObject.Find("Nameplates").transform);

        _nameplateInstance.Initialize(hits);
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    public void Interact()
    {
        hits -= 1;

        _nameplateInstance.SetHealth(hits);

        if (hits <= 0)
        {
            Destroyed?.Invoke(this);

            Destroy(_nameplateInstance.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var carretilla = other.GetComponent<Carretilla>();

        if (carretilla != null)
        {
            _isMoving = false;
            _isAttacking = true;
        }
    }
}
