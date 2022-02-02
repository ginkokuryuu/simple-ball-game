using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    protected List<Node> childNodes = new List<Node>();

    public SelectorNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NODE_STATE Evaluate()
    {
        foreach(Node child in childNodes)
        {
            switch (child.Evaluate())
            {
                case NODE_STATE.RUNNING:
                    nodeState = NODE_STATE.RUNNING;
                    return nodeState;
                case NODE_STATE.SUCCESS:
                    nodeState = NODE_STATE.SUCCESS;
                    return nodeState;
                case NODE_STATE.FAILED:
                    break;
                default:
                    break;
            }
        }

        nodeState = NODE_STATE.FAILED;
        return nodeState;
    }
}
