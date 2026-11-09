using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private float forceMultiplier = 5f;

    private Rigidbody rb;
    // A 7 x 7 area in the middle of the floor (Bounds takes the full size, not half)
    private Bounds spawnBounds = new Bounds(Vector3.zero, new Vector3(7f, 0f, 7f));

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Don't carry speed over from the last episode
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Local positions are relative to TrainingArea, so copies of the area work too
        transform.localPosition = GetRandomPosition(spawnBounds);
        target.localPosition = GetRandomPosition(spawnBounds);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 3 + 3 + 1 + 1 = 8 values, matching the observation size in the Inspector
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rb.linearVelocity.x);
        sensor.AddObservation(rb.linearVelocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Two continuous actions between -1 and 1: how hard to push along x and z
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        rb.AddForce(new Vector3(moveX, 0f, moveZ) * forceMultiplier);

        // Close enough to count as touching the cube
        if (Vector3.Distance(transform.localPosition, target.localPosition) < 1.42f)
        {
            SetReward(1f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Each key pair gives -1, 0 or 1, the same range as the policy's actions
        var continuousActions = actionsOut.ContinuousActions;
        Keyboard keyboard = Keyboard.current;
        continuousActions[0] = (keyboard.dKey.isPressed ? 1f : 0f) - (keyboard.aKey.isPressed ? 1f : 0f);
        continuousActions[1] = (keyboard.wKey.isPressed ? 1f : 0f) - (keyboard.sKey.isPressed ? 1f : 0f);
    }

    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, randomY, randomZ);
    }
}
