using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;

    [Header("Movement Settings")]
    public float gravity = -9.8f;
    [SerializeField] private SOPlayerSetup _playerSetup;
    [SerializeField] private CharacterController _characterController;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;

    public List<Collider> colliders;
     
    private float vSpeed = 0f;
    private bool _isWalking;
    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Update()
    {
        HandleJump();
        HandleMove();
    }


    #region LIFE
    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }

    private void Revive()
    {
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        _alive = true;
        Respawn();
        Invoke(nameof(TurnOnCollicers), .1f);
    }

    private void TurnOnCollicers()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }
    #endregion

    private void HandleMove()
    {
        var vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, Input.GetAxis("Horizontal") * _playerSetup.turnSpeed * Time.deltaTime, 0);
        var speedVector = transform.forward * vertical * _playerSetup.speed;

        _isWalking = vertical != 0;
        if (_isWalking && Input.GetKey(_playerSetup.runButton))
        {
            speedVector *= _playerSetup.speedRun;
            animator.speed = _playerSetup.speedRun;
        }
        else
        {
            animator.speed = 1;
        }

        vSpeed += gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        _characterController.Move(speedVector * Time.deltaTime);
        animator.SetBool("Run", _isWalking);

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
