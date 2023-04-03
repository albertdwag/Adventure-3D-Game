using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private SOPlayerSetup _playerSetup;
    [SerializeField] private CharacterController _characterController;
    public float gravity = -9.8f;

    private float vSpeed = 0f;

    //[SerializeField] private Rigidbody myRigidbody;

    private void Update()
    {
        HandleJump();
        HandleMove();
    }

    private void HandleMove()
    {
        var vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, Input.GetAxis("Horizontal") * _playerSetup.turnSpeed * Time.deltaTime, 0);
        var speedVector = transform.forward * vertical * _playerSetup.speed;

        vSpeed += gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        _characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", vertical != 0);

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //transform.Rotate(0, horizontal * _playerSetup.turnSpeed * Time.deltaTime, 0);
        //Vector3 movement = new Vector3(horizontal, 0f, vertical) * _playerSetup.speed * Time.deltaTime;

        //_characterController.Move(movement);
        ////myRigidbody.MovePosition(transform.position + movement);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_playerSetup.jumpButton) && _characterController.isGrounded)
        {
            vSpeed = _playerSetup.forceJump;
        }
    }

    //private void checkstates()
    //{
    //    if (myrigidbody.velocity.magnitude > _playersetup.minvelocitymagnitude)
    //        playerstatemachine.instance.statemachine.switchstate(playerstatemachine.playerstates.moving);
    //    else if (myrigidbody.velocity.magnitude < _playersetup.minvelocitymagnitude)
    //        playerstatemachine.instance.statemachine.switchstate(playerstatemachine.playerstates.idle);
    //    else if (!_playersetup.isgrounded)
    //        playerstatemachine.instance.statemachine.switchstate(playerstatemachine.playerstates.jumping);
    //}
}
