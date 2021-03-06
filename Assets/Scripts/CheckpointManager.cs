﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint;
    public float TimeLeft;
    
    public KartAgent kartAgent;
    public Checkpoint nextCheckPointToReach;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;
    private Checkpoint lastCheckpoint;

    public event Action<Checkpoint> reachedCheckpoint; 

    void Start()
    {
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        ResetCheckpoints();
    }

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;
        SetNextCheckpoint();
    }

    private void Update()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft < 0f)
        {
            kartAgent.AddReward(-1f);
            kartAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        if (nextCheckPointToReach != checkpoint) return;
        
        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            kartAgent.AddReward(10f);
            kartAgent.EndEpisode();
        }
        else
        {
            // kartAgent.AddReward((0.5f) / Checkpoints.Count);
            kartAgent.AddReward(1f);
            SetNextCheckpoint();
        }
    }

    private void SetNextCheckpoint()
    {
        // Debug.Log("Reach a checkpoint");
        if (Checkpoints != null && Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
        }
    }
}
