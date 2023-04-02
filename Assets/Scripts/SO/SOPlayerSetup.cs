using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Key Binding")]
    public KeyCode moveLeftButton = KeyCode.D;
    public KeyCode moveRightButton = KeyCode.A;
    public KeyCode jumpButton = KeyCode.Space;

    [Header("Movement Settings")]
    public float speed = 10;
    public float runningSpeed = 15;
    public float forceJump = 18;
    public float minJumpHeight = 5;

    [Header("Animation Settings")]
    public string triggerDeath = "Death";
    public string runBool = "Running";
    public float JumpScaleY = 1.5f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;
    //[SerializeField] private float jumpScaleX = 0.7f;
}
