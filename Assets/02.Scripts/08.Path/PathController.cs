using System;
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
    [SerializeField] int maxRow;
    [SerializeField] int maxColumn;

    public Color color;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wallPrefab;


    [SerializeField] float ui_tile_width = 0;
    public float UI_Tile_Width { get { return ui_tile_width; } }
    [SerializeField] float ui_tile_height = 0;
    public float UI_Tile_Height { get { return ui_tile_height; } }


    public Action<PathNode> selectPathNodeEventHandler;




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

        var size = new Vector2(Screen.width, Screen.height - 300);

        ui_tile_width = size.x / maxRow;
        ui_tile_height = size.y / maxColumn;
        BindEvents();
    }

    void BindEvents()
    {
        LunarInputManager.Instance.mouseDownEventHandler += BindClickEvent;
    }

    private void BindClickEvent(Vector2 obj)
    {
        var nodeindex_x = (int)(obj.x / ui_tile_width);
        var nodeindex_y = (int)((obj.y - 300) / ui_tile_height);

        if (nodeindex_x > maxRow - 1 || nodeindex_x < 0)
            return;

        if (nodeindex_y > maxColumn - 1 || nodeindex_y < 0)
            return;

        var targetNodex = grid[nodeindex_x, nodeindex_y];

        // click ui 가 오른쪽 화면을 넘어갈떄 예외처리 ex) 왼쪽 버전
        selectPathNodeEventHandler?.Invoke(targetNodex);
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
                TileWallState walkable = !Physics.CheckSphere(worldPoint, 0.1f * size, mapMask) ? TileWallState.empty : TileWallState.wall; // Adjust the radius as needed

                var tile = Instantiate(tilePrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                tile.transform.parent = this.transform;

                var tileEventTrigger = tile.GetComponent<TileEventTrigger>();
                Material dummy = new Material(tileEventTrigger.meshRenderer.material);
                tileEventTrigger.meshRenderer.material = dummy;
                grid[x, y] = new PathNode(worldPoint, walkable, tile, tileEventTrigger.meshRenderer.material, x, y);

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



        var randomDir = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(NodeDirection)).Length);
        switch ((NodeDirection)randomDir)
        {
            case NodeDirection.left:
                randomStartPosX = 0;
                randomStartPosY = UnityEngine.Random.Range(0, maxColumn - 1);
                break;
            case NodeDirection.right:
                randomStartPosX = maxRow - 1;
                randomStartPosY = UnityEngine.Random.Range(0, maxColumn - 1);
                break;
            case NodeDirection.bottom:
                randomStartPosX = UnityEngine.Random.Range(0, maxRow - 1);
                randomStartPosY = 0;
                break;
            case NodeDirection.top:
                randomStartPosX = UnityEngine.Random.Range(0, maxRow - 1);
                randomStartPosY = maxColumn - 1;
                break;
        }

        startPathNode = grid[randomStartPosX, randomStartPosY];
        startPathNode.material.color = Color.white;
    }

    private void SetTargetPathNode()
    {
        var radomTargetPosX = UnityEngine.Random.Range(maxRow / 2 - 1, maxRow / 2 + 2);
        var radomTargetPosY = UnityEngine.Random.Range(maxColumn / 2 - 1, maxColumn / 2 + 2);

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
        wall.transform.rotation = Quaternion.Euler(0, -90, 0);
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
        return new Color(originalColor.r / 2, originalColor.g / 2, originalColor.b / 2, originalColor.a);
    }

    public bool isCanCreateWall(PathNode pathNode)
    {
        PathNode node =
        targetPath.Find((node) => node.row == pathNode.row && node.column == pathNode.column);
        if (node == null)
            return true;

        List<PathNode> prePath = new List<PathNode>();

        pathNode.ChangeWalkable(TileWallState.wall, true);

        prePath = FindPath(startPathNode, targetPathNode);

        if (prePath == null)
        {
            //건설못하게 처리
            pathNode.ChangeWalkable(TileWallState.empty, true);
            return false;
        }
        pathNode.ChangeWalkable(TileWallState.empty, true);
        return true;

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
                if (neighbor.walkable == TileWallState.wall || closedSet.Contains(neighbor))
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
        if (current.row > 0 && grid[current.row - 1, current.column].walkable == TileWallState.empty)
            neighbors.Add(grid[current.row - 1, current.column]); // Left neighbor
        if (current.row < maxRow - 1 && grid[current.row + 1, current.column].walkable == TileWallState.empty)
            neighbors.Add(grid[current.row + 1, current.column]); // Right neighbor
        if (current.column > 0 && grid[current.row, current.column - 1].walkable == TileWallState.empty)
            neighbors.Add(grid[current.row, current.column - 1]); // Bottom neighbor
        if (current.column < maxColumn - 1 && grid[current.row, current.column + 1].walkable == TileWallState.empty)
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
        if (StageManager.Instance.isDevelopMode == false)
            return;
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
        if (StageManager.Instance.isDevelopMode == false)
            return;

        if (list.Count < 1)
            return;

        for (int i = 0; i < list.Count - 1; i++)
        {
            int index = i;
            list[index].material.color = Color.green;
        }


    }





}
