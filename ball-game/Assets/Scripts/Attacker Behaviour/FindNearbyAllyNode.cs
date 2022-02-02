using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearbyAllyNode : Node
{
    private AttackerAI attackerAI;

    public FindNearbyAllyNode(AttackerAI attackerAI)
    {
        this.attackerAI = attackerAI;
    }

    public override NODE_STATE Evaluate()
    {
        List<AttackerAI> allAttackers = ObjectLoader.INSTANCE.AllAttackers;
        float closestDist = Mathf.Infinity;
        Vector3 currentPos = attackerAI.transform.position;
        AttackerAI closestAlly = null;
        foreach(AttackerAI ally in allAttackers)
        {
            if (ally == attackerAI)
                continue;
            if (ally.StillInactive())
                continue;

            Vector3 direction = ally.transform.position - currentPos;
            float distance = direction.sqrMagnitude;
            if(distance < closestDist)
            {
                closestDist = distance;
                closestAlly = ally;
            }
        }

        if(closestAlly == null)
        {
            nodeState = NODE_STATE.FAILED;
            return nodeState;
        }

        attackerAI.ClosestAlly = closestAlly;
        nodeState = NODE_STATE.SUCCESS;
        return nodeState;
    }
}
