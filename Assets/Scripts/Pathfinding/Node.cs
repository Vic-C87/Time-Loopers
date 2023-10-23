using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool Walkable;
    public Vector3 WorldPosition;
    public int GridXCoordinate;
    public int GridYCoordinate;
    public int MovementPenalty;
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get { return GCost + HCost; }}
    public int HeapIndex { get; set; }

    public Node Parent;

    public Node(bool aWalkable, Vector3 aWorldPosition, int anXCoordinate, int aYCoordinate, int aPenalty)
    {
        Walkable = aWalkable;
        WorldPosition = aWorldPosition;
        GridXCoordinate = anXCoordinate;
        GridYCoordinate = aYCoordinate;
        MovementPenalty = aPenalty;
    }

    public int CompareTo(Node aNodeToCompare)
    {
        int compare = FCost.CompareTo(aNodeToCompare.FCost);
        if (compare == 0)
        {
            compare = HCost.CompareTo(aNodeToCompare.HCost);
        }
        return -compare;
    }
}
