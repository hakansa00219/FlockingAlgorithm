using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [Range(10, 500)]
    public int startingCount = 250;
    const float AGENT_DENSITY = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius => squareAvoidanceRadius;
    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * startingCount * AGENT_DENSITY;
            randomPosition.y = 2;
            FlockAgent agent = Instantiate(agentPrefab, randomPosition, Quaternion.Euler(Vector3.up * UnityEngine.Random.Range(0f, 360f)), transform);

            agent.name = "Agent " + i;
            agent.Initialize(this);
            agents.Add(agent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //MeshRenderer[] renderers = agent.GetComponentsInChildren<MeshRenderer>();
            //Array.ForEach(renderers, (r) => r.material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f));

            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius, LayerMask.GetMask("Obstacle") | 1 << agentPrefab.gameObject.layer);

        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
