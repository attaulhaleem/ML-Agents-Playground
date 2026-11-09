using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    [SerializeField] private Transform target;

    void Start()
    {
        Bounds spawnBounds = new Bounds(new Vector3(0f, 0.5f, 0f), new Vector3(4f, 0f, 4f));
        transform.position = GetRandomPosition(spawnBounds);
        target.position = GetRandomPosition(spawnBounds);
    }

    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(randomX, randomY, randomZ);
    }

    public override void OnEpisodeBegin()
    {
        Bounds spawnBounds = new Bounds(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(4.5f, 0.0f, 4.5f));
        transform.position = GetRandomPosition(spawnBounds);
        target.position = GetRandomPosition(spawnBounds);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

    }
}
