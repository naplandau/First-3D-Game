using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float forwardRotateSpeed = 30f;

    private bool isMoving;

    private void Update()
    {
        Vector2 input = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            input.y = +1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x = +1;
        }

        input = input.normalized;
        Vector3 moveDir = new Vector3(input.x, 0f, input.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isMoving = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, forwardRotateSpeed * Time.deltaTime);
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
