using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartNodeDirection
{
    left,
    right,
    bottom,
    top,
        
}


public class PathController : MonoBehaviour
{
    [SerializeField] private PathNode startPathNode;
    [SerializeField] private PathNode targetPathNode;

    public PathNode GetStartPathNode { get { return startPathNode; } }
    public PathNode GetTargetPathNode { get { return targetPathNode; } }

    [SerializeField] private PathNode[,] grid;
    private List<PathNode> openSet;
    private HashSet<PathNode> closedSet;

    [SerializeField] List<PathNode> targetPath = new List<PathNode>();
    public List<PathNode> TargetPath { get { return targetPath; } }

    public int row;
    public int column;

    public Color color;


    [Header("Setting")]
    [SerializeField] private GameObject prefab;






    public void Init()
    {
        CreateGrid(row, column);
        
        SetTargetPathNode();
        SetStartPathNode();
        
        targetPath = FindPath(startPathNode, targetPathNode);
    }


    void CreateGrid(int _row, int _colomn)
    {
        grid = new PathNode[_row, _colomn];

        float size = prefab.transform.localScale.x;
        Vector3 worldBottomLeft = transform.position - Vector3.right * _row / 2 * size -
            Vector3.forward * _colomn / 2 * size;

        LayerMask mapMask = LayerMask.NameToLayer("Tile");

        for (int x = 0; x < _row; x++)
        {
            for (int y = 0; y < _colomn; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * size) + Vector3.forward * (y * size);
                bool walkable = !Physics.CheckSphere(worldPoint, 0.1f * size, mapMask); // Adjust the radius as needed
                var tile = Instantiate(prefab, worldPoint, Quaternion.Euler(90, 0, 0));
                tile.transform.parent = this.transform;
                var node = tile.GetComponent<MeshRenderer>();
                Material dummy = new Material(node.material);
                node.material = dummy;
                grid[x, y] = new PathNode()
                {
                    position = worldPoint,
                    walkable = walkable,
                    ground = tile,
                    material = node.material,
                    row = x,
                    column = y
                };

                SetTileColor(x, y, node, color);

            }
        }

        

    }

    private void SetStartPathNode()
    {
        // var randomStartPosX = Random.Range();
        //var randomStartPosY = Random.Range();
        // 0,0 ~ row - 1,0
        // 0,0 ~ 0,column - 1
        // row-1 , 0 ~ row -1 , column -1
        // 0,column -1 ~ row-1, column -1
        var randomStartPosX = 0;
        var randomStartPosY = 0;
        var randomDir = Random.Range(0, System.Enum.GetValues(typeof(StartNodeDirection)).Length);
        switch ((StartNodeDirection)randomDir)
        {
            case StartNodeDirection.left:
                randomStartPosX = 0;
                randomStartPosY = Random.Range(0, column - 1);
                break;
            case StartNodeDirection.right:
                randomStartPosX = row - 1;
                randomStartPosY = Random.Range(0, column - 1);
                break;
            case StartNodeDirection.bottom:
                randomStartPosX = Random.Range(0, row - 1);
                randomStartPosY = 0;
                break;
            case StartNodeDirection.top:
                randomStartPosX = Random.Range(0, row - 1);
                randomStartPosY = column - 1;
                break;
        }

        startPathNode = grid[randomStartPosX, randomStartPosY];
    }

    private void SetTargetPathNode()
    {
        var radomTargetPosX = Random.Range(row / 2 - 1, row / 2 + 2);
        var radomTargetPosY = Random.Range(column / 2 - 1, column / 2 + 2);

        targetPathNode = grid[radomTargetPosX, radomTargetPosY];
    }

    private void SetTileColor(int x, int y, MeshRenderer node, Color color)
    {
        if (x % 2 == 0)
        {
            if (y % 2 == 0)
            {
                node.material.color = color;
            }
            else
            {
                node.material.color = InvertColor(color);
            }
        }
        else
        {
            if (y % 2 == 0)
            {
                node.material.color = InvertColor(color);
            }
            else
            {
                node.material.color = color;
            }
        }
    }


    Color InvertColor(Color originalColor)
    {
        return new Color(originalColor.r /2 , originalColor.g / 2, originalColor.b / 2, originalColor.a);
    }


    public List<PathNode> FindPath(PathNode startPos, PathNode targetPos)
    {
        PathNode startPathNode = startPos;
        startPathNode.material.color = Color.white;
        PathNode targetPathNode = targetPos;
        targetPathNode.material.color = Color.black;

        openSet = new List<PathNode>();
        closedSet = new HashSet<PathNode>();
        openSet.Add(startPathNode);

        while (openSet.Count > 0)
        {
            PathNode currentPathNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentPathNode.FCost ||
                    (openSet[i].FCost == currentPathNode.FCost && openSet[i].hCost < currentPathNode.hCost))
                {
                    currentPathNode = openSet[i];
                  
                }
            }

            openSet.Remove(currentPathNode);
            closedSet.Add(currentPathNode);

            if (currentPathNode == targetPathNode)
            {
                return RetracePath(startPathNode, targetPathNode);
            }

            foreach (PathNode neighbor in GetPathNode(currentPathNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newCostToNeighbor = currentPathNode.gCost + GetDistance(currentPathNode, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetPathNode);
                    neighbor.parent = currentPathNode;
              

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    List<PathNode> GetPathNode(PathNode current)
    {
        List<PathNode> neighbors = new List<PathNode>();

        // Check neighboring nodes in cardinal directions
        if (current.row > 0 && grid[current.row - 1, current.column].walkable)
            neighbors.Add(grid[current.row - 1, current.column]); // Left neighbor
        if (current.row < row - 1 && grid[current.row + 1, current.column].walkable)
            neighbors.Add(grid[current.row + 1, current.column]); // Right neighbor
        if (current.column > 0 && grid[current.row, current.column - 1].walkable)
            neighbors.Add(grid[current.row, current.column - 1]); // Bottom neighbor
        if (current.column < column -1 && grid[current.row, current.column + 1].walkable)
            neighbors.Add(grid[current.row, current.column + 1]); // Top neighbor

        return neighbors;
    }





   List<PathNode> RetracePath(PathNode startPathNode, PathNode endPathNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentPathNode = endPathNode;

        while (currentPathNode != startPathNode)
        {
            path.Add(currentPathNode);
            if (currentPathNode != endPathNode)
                currentPathNode.material.color = Color.green;
            currentPathNode = currentPathNode.parent;
        }
        path.Reverse();

        return path;
    }

    int GetDistance(PathNode PathNodeA, PathNode PathNodeB)
    {
        // Calculate the distance between two PathNodes (could be Manhattan distance, Euclidean distance, etc.)
        return Mathf.RoundToInt(Vector3.Distance(PathNodeA.position, PathNodeB.position));
    }



}
