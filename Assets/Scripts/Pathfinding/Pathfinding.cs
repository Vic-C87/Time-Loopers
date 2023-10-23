using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    PathManager myPathManager;

    NodeGrid myGrid;

    private void Awake()
    {
        myGrid = GetComponent<NodeGrid>();
        myPathManager = GetComponent<PathManager>();
    }

    public void StartFindPath(Vector3 aStartingPosition, Vector3 aTargetPosition)
    {
        StartCoroutine(FindPath(aStartingPosition, aTargetPosition));
    }

    IEnumerator FindPath(Vector3 aStartingPosition, Vector3 aTargetPosition)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = myGrid.NodeFromWorldPoint(aStartingPosition);
        Node targetNode = myGrid.NodeFromWorldPoint(aTargetPosition);


        if (startNode.Walkable && targetNode.Walkable)
        {
            Heap<Node> openSet = new Heap<Node>(myGrid.myMaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    pathSuccess = true;

                    break;
                }

                foreach (Node neighbour in myGrid.GetNeighbours(currentNode))
                {
                    int newMovementCostToNeighbour = 0;
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }


                    newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour) + neighbour.MovementPenalty;

                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }

            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        PathManager.myInstance.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node aStartNode, Node aTargetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = aTargetNode;

        while (currentNode != aStartNode)
        {
            currentNode.WorldPosition.y = 1f;
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> aPath)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < aPath.Count; i++)
        {
            Vector2 directionNew = new Vector2(aPath[i - 1].GridXCoordinate - aPath[i].GridXCoordinate, aPath[i - 1].GridYCoordinate - aPath[i].GridYCoordinate);
            if (directionNew != directionOld)
            {
                waypoints.Add(aPath[i].WorldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node aNodeA, Node aNodeB)
    {
        int distanceX = Mathf.Abs(aNodeA.GridXCoordinate - aNodeB.GridXCoordinate);
        int distanceY = Mathf.Abs(aNodeA.GridYCoordinate - aNodeB.GridYCoordinate);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);

    }

}