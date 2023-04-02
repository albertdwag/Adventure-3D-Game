using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Key Binding")]
    public KeyCode jumpButton = KeyCode.Space;

    [Header("Movement Settings")]
    public float speed = 10;
    public float forceJump = 18;
    public bool isGrounded = true;
    public float minVelocityMagnitude = 0.1f;
}
