using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public Settings[] settings;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 move = Vector3.zero;
        float totalWeight = 0f;
        foreach (var s in settings)
        {
            Vector3 partialMove = s.behaviour.CalculateMove(agent, context, flock) * s.weight;
            totalWeight += s.weight;


            if(partialMove != Vector3.zero)
            {
                if(partialMove.sqrMagnitude > s.weight * s.weight)
                {
                    partialMove.Normalize();
                    partialMove *= s.weight;
                }

                move += partialMove;
            }
        }
        move.y = 0;
        return move;
    }
    [Serializable]
    public class Settings
    {
        public FlockBehaviour behaviour;
        public float weight;
    }
}
