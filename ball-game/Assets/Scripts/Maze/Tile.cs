using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public Tile parent;

    public static Tile GetHighestParent(Tile tile)
    {
        if (tile.parent == null)
        {
            return tile;
        }
        else
        {
            return GetHighestParent(tile.parent);
        }
    }
}
