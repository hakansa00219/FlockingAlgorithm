using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        
        foreach (Transform t in original)
        {
            FlockAgent tAgent = t.GetComponent<FlockAgent>();
            if (tAgent != null && tAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(t);
            }
        }
        return filtered;

    }
}
