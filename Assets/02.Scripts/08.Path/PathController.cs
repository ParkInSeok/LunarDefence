using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeDirection
{
    left,
    right,
    bottom,
    top,
        
}


public class PathController : MonoBehaviour
{
    [Header("Setting")]
    public int maxRow;
    public int maxColumn;

    public Color color;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;


    [Header("Debugging")]
    [SerializeField] private PathNode startPathNode;
    [SerializeField] private PathNode targetPathNode;

    public PathNode GetStartPathNode { get { return startPathNode; } }
    public PathNode GetTargetPathNode { get { return targetPathNode; } }

    [SerializeField] private PathNode[,] grid;
    private List<PathNode> openSet;
    private HashSet<PathNode> closedSet;

    [SerializeField] List<PathNode> targetPath = new List<PathNode>();
    public List<PathNode> TargetPath { get { return targetPath; } }









    public void Init()
    {
        CreateGrid(maxRow, maxColumn);

        CreateWall();

        SetStartPathNode();
        SetTargetPathNode();
        


        targetPath = FindPath(startPathNode, targetPathNode);
        DrawPathTileColor(targetPath);
    }


    void CreateGrid(int _row, int _colomn)
    {
        grid = new PathNode[_row, _colomn];

        float size = tilePrefab.transform.localScale.x;
        Vector3 worldBottomLeft = transform.position - Vector3.right * _row / 2 * size -
            Vector3.forward * _colomn / 2 * size;

        LayerMask mapMask = LayerMask.NameToLayer("Tile");

        for (int x = 0; x < _row; x++)
        {
            for (int y = 0; y < _colomn; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * size) + Vector3.forward * (y * size);
                bool walkable = !Physics.CheckSphere(worldPoint, 0.1f * size, mapMask); // Adjust the radius as needed
                
                var tile = Instantiate(tilePrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                tile.transform.parent = this.transform;

                var tileEventTrigger = tile.GetComponent<TileEventTrigger>();
                Material dummy = new Material(tileEventTrigger.meshRenderer.material);
                tileEventTrigger.meshRenderer.material = dummy;
                grid[x, y] = new PathNode()
                {
                    position = worldPoint,
                    walkable = walkable,
                    ground = tile,
                    material = tileEventTrigger.meshRenderer.material,
                    row = x,
                    column = y
                };
                tileEventTrigger.pathNode = grid[x, y];

                SetTileColor(x, y, tileEventTrigger, color);

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
        var randomDir = Random.Range(0, System.Enum.GetValues(typeof(NodeDirection)).Length);
        switch ((NodeDirection)randomDir)
        {
            case NodeDirection.left:
                randomStartPosX = 0;
                randomStartPosY = Random.Range(0, maxColumn - 1);
                break;
            case NodeDirection.right:
                randomStartPosX = maxRow - 1;
                randomStartPosY = Random.Range(0, maxColumn - 1);
                break;
            case NodeDirection.bottom:
                randomStartPosX = Random.Range(0, maxRow - 1);
                randomStartPosY = 0;
                break;
            case NodeDirection.top:
                randomStartPosX = Random.Range(0, maxRow - 1);
                randomStartPosY = maxColumn - 1;
                break;
        }

        startPathNode = grid[randomStartPosX, randomStartPosY];
        startPathNode.material.color = Color.white;
    }

    private void SetTargetPathNode()
    {
        var radomTargetPosX = Random.Range(maxRow / 2 - 1, maxRow / 2 + 2);
        var radomTargetPosY = Random.Range(maxColumn / 2 - 1, maxColumn / 2 + 2);

        targetPathNode = grid[radomTargetPosX, radomTargetPosY];
        targetPathNode.material.color = Color.black;
    }

    void CreateWall()
    {
        CreateTopWall();
        CreateRightWall();
        CreateLeftWall();
        CreateBottomWall();
    }

    private void CreateTopWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.parent = this.transform;
        float tileSize = tilePrefab.transform.localScale.x;
        wall.transform.localScale = new Vector3((maxRow + 1) * tileSize, 30, 1);
        float wallZ = (maxColumn - 1) / 2 * tileSize;
        wall.transform.position = new Vector3(-tileSize / 2, 14, wallZ);
    }

    private void CreateRightWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.rotation = Quaternion.Euler(0, 90, 0);
        wall.transform.parent = this.transform;
        float tileSize = tilePrefab.transform.localScale.x;
        wall.transform.localScale = new Vector3((maxColumn + 1) * tileSize, 30, 1);
        float wallX = (maxRow) / 2 * tileSize;
        wall.transform.position = new Vector3(wallX, 14, -tileSize / 2);
    }

    private void CreateLeftWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.rotation = Quaternion.Euler(0,-90, 0);
        wall.transform.parent = this.transform;
        float tileSize = tilePrefab.transform.localScale.x;
        wall.transform.localScale = new Vector3((maxColumn + 1) * tileSize, 30, 1);
        float wallX = (maxRow + 1) / 2 * tileSize;
        wall.transform.position = new Vector3(-wallX, 14, -tileSize / 2);
    }

    private void CreateBottomWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.rotation = Quaternion.Euler(90, 0, 0);
        wall.transform.parent = this.transform;
        float tileSize = tilePrefab.transform.localScale.x;
        wall.transform.localScale = new Vector3((maxRow + 1) * tileSize, (maxColumn + 1) * tileSize, 1);
        float bottomZ = (maxColumn + 1) * tileSize / 2;
        float bottomX = tileSize / 2;
        wall.transform.position = new Vector3(-bottomX, -0.1f, -bottomZ);
    }

    private void SetTileColor(int x, int y, TileEventTrigger tile, Color color)
    {
        if (x % 2 == 0)
        {
            if (y % 2 == 0)
            {
                tile.pathNode.material.color = color;
                tile.pathNode.origineColor = color;
            }
            else
            {
                tile.pathNode.material.color = InvertColor(color);
                tile.pathNode.origineColor = InvertColor(color);
            }
        }
        else
        {
            if (y % 2 == 0)
            {
                tile.pathNode.material.color = InvertColor(color);
                tile.pathNode.origineColor = InvertColor(color);
            }
            else
            {
                tile.pathNode.material.color = color;
                tile.pathNode.origineColor = color;
            }
        }
    }


    Color InvertColor(Color originalColor)
    {
        return new Color(originalColor.r /2 , originalColor.g / 2, originalColor.b / 2, originalColor.a);
    }

    public void ReFindPath(TileEventTrigger trigger)
    {
        //벽 건설이 targetPath에 있는 경로 타일에 건설된다면
        PathNode node =
            targetPath.Find((node) => node.row == trigger.pathNode.row && node.column == trigger.pathNode.column);
        if (node == null)
            return;
       
        // 해당 타일이 막혔을때 경로가 없으면 건설 못하게 해야함
        List<PathNode> prePath = targetPath;
        targetPath = FindPath(startPathNode, targetPathNode);

        if(targetPath == null)
        {
            //건설못하게 처리
            targetPath = prePath;
            return;
        }

        ResetPathTileColor(prePath);

        DrawPathTileColor(targetPath);
    }
    public void ReFindPath()
    {
        ResetPathTileColor(targetPath);

        targetPath = FindPath(startPathNode, targetPathNode);

        DrawPathTileColor(targetPath);
    }


    List<PathNode> FindPath(PathNode startPos, PathNode targetPos)
    {
        PathNode startPathNode = startPos;
        PathNode targetPathNode = targetPos;

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
        if (current.row < maxRow - 1 && grid[current.row + 1, current.column].walkable)
            neighbors.Add(grid[current.row + 1, current.column]); // Right neighbor
        if (current.column > 0 && grid[current.row, current.column - 1].walkable)
            neighbors.Add(grid[current.row, current.column - 1]); // Bottom neighbor
        if (current.column < maxColumn -1 && grid[current.row, current.column + 1].walkable)
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


    void ResetPathTileColor(List<PathNode> list)
    {
        //debugging 용 함수
        if (list.Count < 1)
            return;

        for (int i = 0; i < list.Count - 1; i++)
        {
            int index = i;
            list[index].material.color = targetPath[index].origineColor;
        }

    }

    void DrawPathTileColor(List<PathNode> list)
    {
        if (list.Count < 1)
            return;

        for (int i = 0; i < list.Count - 1; i++)
        {
            int index = i;
            list[index].material.color = Color.green;
        }


    }



}
