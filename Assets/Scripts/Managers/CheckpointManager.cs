using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int LastCheckpointKey
    {
        get { return lastCheckpointKey; }
        set { lastCheckpointKey = value; }
    }
    public List<CheckPointBase> checkpoints;

    [SerializeField] private int lastCheckpointKey = 0;

    protected override void Awake()
    {
        base.Awake();
        lastCheckpointKey = SaveManager.Instance.Setup.checkpoint;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }
}
