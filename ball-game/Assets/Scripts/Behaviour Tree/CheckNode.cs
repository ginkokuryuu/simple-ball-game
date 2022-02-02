using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNode : Node
{
    public override NODE_STATE Evaluate()
    {
        Debug.Log("Checked");
        return NODE_STATE.SUCCESS;
    }
}
