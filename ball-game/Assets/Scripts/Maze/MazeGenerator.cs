using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject prefab;
    [Range(1, 30)]
    public int width;
    [Range(1, 30)]
    public int height;
    [HideInInspector] public Tile[] tiles;
    [HideInInspector] public List<Edge> edges;
    [HideInInspector] public int edgeIndex = 0;

    [SerializeField] Transform mazeContainer = null;

    public void StartGeneratingMaze()
    {
        tiles = new Tile[width * height];
        for (int i = 0; i < width * height; i++)
        {
            tiles[i] = new Tile();
        }

        edges = new List<Edge>();

        SpawnLeftRightBoundaries();
        //SpawnUpDownBoundaries();
        SpawnInnerEdgesLeftRight();
        SpawnInnerEdgesUpDown();

        StartRemoveEdge();
    }

    // Generating frame (outter boundaries)
    public void SpawnLeftRightBoundaries()
    {
        for (int z = 1; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                if (x == 0 || x == width)
                {
                    //Instantiate(prefab, new Vector3((x * 5) + 2.5f, 0, (z * 5) + 2.5f), Quaternion.Euler(0, 90, 0));
                    GameObject edge = Instantiate(prefab, mazeContainer, false);
                    edge.transform.localPosition = new Vector3((x * 5), 0, (z * 5));
                    edge.transform.localRotation = Quaternion.Euler(0, 90, 0);
                }
            }
        }
    }

    public void SpawnUpDownBoundaries()
    {
        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (z == 0 || z == height)
                {
                    GameObject edge = Instantiate(prefab, mazeContainer, false);
                    edge.transform.localPosition = new Vector3((x * 5) + 2.5f, 0, (z * 5) + 2.5f);
                    edge.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    // Generating edges (innner Boundaries)
    public void SpawnInnerEdgesLeftRight()
    {
        edgeIndex = 0;

        for (int z = 1; z <= height; z++)
        {
            for (int x = 1; x < width; x++)
            {
                GameObject edge = Instantiate(prefab, mazeContainer, false);
                edge.transform.localPosition = new Vector3((x * 5), 0, (z * 5));
                edge.transform.localRotation = Quaternion.Euler(0, 90, 0);
                Edge edgeScript = edge.AddComponent<Edge>();

                edgeScript.tiles = new Tile[2];
                edgeScript.tiles[0] = tiles[edgeIndex];
                edgeScript.tiles[1] = tiles[edgeIndex + 1];

                edges.Add(edge.GetComponent<Edge>());
                edgeIndex++;
            }
            edgeIndex++;
        }
    }

    public void SpawnInnerEdgesUpDown()
    {
        edgeIndex = 0;

        for (int z = 1; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject edge = Instantiate(prefab, mazeContainer, false);
                edge.transform.localPosition = new Vector3((x * 5) + 2.5f, 0, (z * 5) + 2.5f);
                edge.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Edge edgeScript = edge.AddComponent<Edge>();

                edgeScript.tiles = new Tile[2];
                edgeScript.tiles[0] = tiles[edgeIndex];
                edgeScript.tiles[1] = tiles[edgeIndex + 1];

                edges.Add(edge.GetComponent<Edge>());
                edgeIndex++;
            }
        }
    }

    public void RemoveEdges()
    {
        // Get a random edge
        int randInt = Random.Range(0, edges.Count);
        Edge randomEdge = edges[randInt];

        // Remove random edge from list
        edges.RemoveAt(randInt);

        if (Tile.GetHighestParent(randomEdge.tiles[0]) == Tile.GetHighestParent(randomEdge.tiles[1]))
            return;

        Tile.GetHighestParent(randomEdge.tiles[1]).parent = randomEdge.tiles[0];
        randomEdge.DisableEdge();
    }

    public void StartRemoveEdge()
    {
        int loopNum = edges.Count;

        for (int i = 0; i < loopNum; i++)
        {
            RemoveEdges();
        }
    }

    public void ResetMaze()
    {
        for(int i = mazeContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(mazeContainer.GetChild(i).gameObject);
        }
    }
}
