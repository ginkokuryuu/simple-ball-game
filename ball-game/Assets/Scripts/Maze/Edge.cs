﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Edge : MonoBehaviour
{
    public Tile[] tiles;

    public void DisableEdge()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<NavMeshObstacle>().enabled = false;
    }
}
