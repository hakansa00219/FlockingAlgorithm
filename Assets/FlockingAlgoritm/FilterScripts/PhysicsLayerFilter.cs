using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {

        List<Transform> filtered = new List<Transform>();
        int temp;
        foreach (Transform t in original)
        {
            temp = (mask | (1 << t.gameObject.layer));
            if (mask == temp)
            {
                filtered.Add(t);
            }
        }
        return filtered;

    }
}
