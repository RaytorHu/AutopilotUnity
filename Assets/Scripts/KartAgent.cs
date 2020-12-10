using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class KartAgent : Agent
{
    private CarController carController;
    public CheckpointManager checkpointManager;

    public override void Initialize()
    {
        carController = GetComponent<CarController>();
        checkpointManager.ResetCheckpoints();
    }

    public override void OnEpisodeBegin()
    {
        checkpointManager.ResetCheckpoints();
        carController.Respawn();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // add one more sensor: distance to the next checkpoint
        Vector3 diff = checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff / 20f);
        AddReward(-0.001f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var input = actions.ContinuousActions;

        carController.Steering(input[0]);
        carController.Accleration(input[1]);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;

        action[0] = Input.GetAxis("Horizontal");
        // action[1] = Input.GetAxis("Vertical");
        action[1] = Input.GetKey(KeyCode.W) ? 1f : 0f;
    }
}
