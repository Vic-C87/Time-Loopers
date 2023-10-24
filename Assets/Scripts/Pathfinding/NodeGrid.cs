using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] bool DisplayGridGizmos;
    [SerializeField] float myNodeRadius;
    [SerializeField] TerrainType[] WalkableRegions;
    public Vector2 GridWorldSize;
    [SerializeField] LayerMask myUnwalkableMask;
    [SerializeField] float myUnwalkableBufferMultiplier;
    [SerializeField] LayerMask myWalkableMask;
    Dictionary<int, int> myRegions = new Dictionary<int, int>();

    float myNodeDiameter;
    int myGridSizeX, myGridSizeY;

    Node[,] myGrid;

    public int ObstacleProximityPenalty = 10;
    int myPenaltyMin = int.MaxValue;
    int myPenaltyMax = int.MinValue;



    void Awake()
    {
        myNodeDiameter = myNodeRadius * 2;
        myGridSizeX = Mathf.RoundToInt(GridWorldSize.x / myNodeDiameter);
        myGridSizeY = Mathf.RoundToInt(GridWorldSize.y / myNodeDiameter);

        foreach (TerrainType region in WalkableRegions)
        {
            myWalkableMask.value |= region.myTerrainMask.value;
            myRegions.Add((int)Mathf.Log(region.myTerrainMask.value, 2), region.myTerrainPenalty);
        }

        CreateGrid();
    }

    public void StartBattle()
    {
        CreateGrid();
    }

    public int myMaxSize
    {
        get
        {
            return myGridSizeX * myGridSizeY;
        }
    }

    void CreateGrid()
    {
        myGrid = new Node[myGridSizeX, myGridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;

        for (int x = 0; x < myGridSizeX; x++)
        {
            for (int y = 0; y < myGridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + (Vector3.right * (x * myNodeDiameter + myNodeRadius) + Vector3.forward * (y * myNodeDiameter + myNodeRadius));
                bool walkable = !Physics.CheckSphere(worldPoint, myNodeRadius * myUnwalkableBufferMultiplier, myUnwalkableMask);

                int movementPenalty = 0;


                Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, myWalkableMask))
                {
                    myRegions.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                }

                if (!walkable)
                {
                    movementPenalty += ObstacleProximityPenalty;
                }

                myGrid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }

    public List<Node> GetNeighbours(Node aNode)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = aNode.GridXCoordinate + x;
                int checkY = aNode.GridYCoordinate + y;

                if (checkX >= 0 && checkX < myGridSizeX && checkY >= 0 && checkY < myGridSizeY)
                {
                    neighbours.Add(myGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 aWorldPosition)
    {
        float percentX = (aWorldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (aWorldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((myGridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((myGridSizeY - 1) * percentY);

        return myGrid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, .1f, GridWorldSize.y));
        if (myGrid != null && DisplayGridGizmos)
        {
            foreach (Node n in myGrid)
            {
                Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(myPenaltyMin, myPenaltyMax, n.MovementPenalty));
                Gizmos.color = (n.Walkable) ? Gizmos.color : Color.red;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (myNodeDiameter - .1f));
            }
        }
    }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask myTerrainMask;
        public int myTerrainPenalty;
    }
}
