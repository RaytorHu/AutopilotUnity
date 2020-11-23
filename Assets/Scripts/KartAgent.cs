using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class KartAgent : Agent
{
    private CarController _carController;
    public CheckpointManager _checkpointManager;

    public override void Initialize()
    {
        _carController = GetComponent<CarController>();
        _checkpointManager.ResetCheckpoints();
    }

    public override void OnEpisodeBegin()
    {
        _checkpointManager.ResetCheckpoints();
        _carController.Respawn();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;

        _carController.Steering(input[0]);
        _carController.Accleration(input[1]);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }
}
