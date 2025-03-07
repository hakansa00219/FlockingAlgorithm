using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    Vector3 position;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current alignment.
        if (context.Count == 0) return agent.transform.forward;

        Vector3 alignmentMove = Vector3.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filteredContext)
        {
            alignmentMove += t.transform.forward;
        }

        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
