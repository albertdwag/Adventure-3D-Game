using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;
    public string tagToCompare = "Player";

    private bool _checkpointActived = false;
    private string _checkPointKey = "CheckpoinKey";

    private void OnTriggerEnter(Collider other)
    {
        if (!_checkpointActived && other.CompareTag(tagToCompare))
            CheckCheckpoint();
    }

    private void CheckCheckpoint()
    {
        SaveCheckpoint();
        TurnItOn();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        //if(PlayerPrefs.GetInt(_checkPointKey, 0) > key )
        //    PlayerPrefs.SetInt(_checkPointKey, key);

        CheckpointManager.Instance.SaveCheckpoint(key);
        _checkpointActived = true;
    }
}
