using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    public Transform car;
    public LayerMask undriveable;
    public Vector2 worldGridSize;
    public float nodeRadius;
    public Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(worldGridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(worldGridSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * worldGridSize.x/2 - Vector3.up * worldGridSize.y/2;

        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool driveable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, undriveable));
                grid[x, y] = new Node(driveable, worldPoint);
                //Debug.Log(driveable);
            }
        }
    }

    public Node ObjectPositionNode(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + worldGridSize.x / 2) / worldGridSize.x;
        float percentY = (worldPosition.y + worldGridSize.y / 2) / worldGridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX) * percentX);
        int y = Mathf.RoundToInt((gridSizeY) * percentY);

        return grid[x,y];
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position, new Vector3(worldGridSize.x, worldGridSize.y));

    //    if (grid != null)
    //    {
    //        Node carNode = ObjectPositionNode(car.position);
    //        foreach (Node node in grid)
    //        {
    //            if(node.driveable)
    //            {
    //                Gizmos.color = Color.gray;
    //            }
    //            else
    //            {
    //                Gizmos.color = Color.red;
    //            }
                
    //            if(carNode == node)
    //            {
    //                Gizmos.color = Color.green;
    //            }
    //            Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f));

    //        }
    //    }
    //}

}


