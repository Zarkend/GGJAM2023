using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection
{
    Left,
    Right,
}
public class Goblin : MonoBehaviour
{
    public MoveDirection MoveDirection = MoveDirection.Left;

    [SerializeField]
    private float moveSpeed;

    private bool _isMoving = true;
    private bool _isAttacking;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
