using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    private List<Node> childNodes = new List<Node>();

    public SequenceNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NODE_STATE Evaluate()
    {
        bool isChildRunning = false;

        foreach (Node child in childNodes)
        {
            switch (child.Evaluate())
            {
                case NODE_STATE.RUNNING:
                    isChildRunning = true;
                    break;
                case NODE_STATE.SUCCESS:
                    break;
                case NODE_STATE.FAILED:
                    nodeState = NODE_STATE.FAILED;
                    return nodeState;
                default:
                    break;
            }
        }

        nodeState = isChildRunning ? NODE_STATE.RUNNING : NODE_STATE.SUCCESS;
        return nodeState;
    }
}
