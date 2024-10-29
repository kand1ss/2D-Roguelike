using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movementVector = InputService.Instance.GetMovementVector();

        transform.position += movementVector * (moveSpeed * Time.deltaTime);
    }
}
