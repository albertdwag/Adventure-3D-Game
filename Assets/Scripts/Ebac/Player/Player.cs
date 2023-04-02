using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private SOPlayerSetup _playerSetup;
    [SerializeField] private Rigidbody myRigidbody;

    private void FixedUpdate()
    {
        HandleMove();
        HandleJump();
    }

    private void HandleMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * _playerSetup.speed * Time.deltaTime;

        myRigidbody.MovePosition(transform.position + movement);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_playerSetup.jumpButton))
        {
            myRigidbody.AddForce(Vector3.up * _playerSetup.forceJump, ForceMode.Impulse);
        }
    }
}
