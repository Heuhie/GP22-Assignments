using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool driveable;
    public Vector3 worldPosition;


    public Node(bool driveable, Vector3 worldPosition)
    {
        this.driveable = driveable;
        this.worldPosition = worldPosition;
    }

}
