using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private SOPlayerSetup _playerSetup;
    [SerializeField] private Rigidbody myRigidbody;

    private void Update()
    {
        CheckStates();
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
        if (Input.GetKeyDown(_playerSetup.jumpButton) && _playerSetup.isGrounded)
        {
            myRigidbody.AddForce(Vector3.up * _playerSetup.forceJump, ForceMode.Impulse);
            _playerSetup.isGrounded = false;
        }
    }

    private void CheckStates()
    {
        if (myRigidbody.velocity.magnitude > _playerSetup.minVelocityMagnitude)
            PlayerStateMachine.Instance.stateMachine.SwitchState(PlayerStateMachine.PlayerStates.MOVING);
        else if (myRigidbody.velocity.magnitude < _playerSetup.minVelocityMagnitude)
            PlayerStateMachine.Instance.stateMachine.SwitchState(PlayerStateMachine.PlayerStates.IDLE);
        else if (!_playerSetup.isGrounded)
            PlayerStateMachine.Instance.stateMachine.SwitchState(PlayerStateMachine.PlayerStates.JUMPING);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _playerSetup.isGrounded = true;
    }

}
