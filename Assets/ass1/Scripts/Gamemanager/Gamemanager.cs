using System;
// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Gamemanager : MonoBehaviour
{
    // private int m_NbProjectiles = 0;

    private GameObject playerGameObject;
    private Player playerScript;

    private Canvas canvas;
    private TextMeshProUGUI projectilesText;
    private TextMeshProUGUI gameStateText;

    private MazeNode[,] Unimaze;

    private MazeNode[,] Maze;

    //This is the blockprefab. Drag the block prefab here on the unity editor.
    public GameObject BlockPrefab;
    public GameObject ProjectilePrefab;
    public GameObject BlockandProjectilePrefab;

    public GameObject PlatformPrefab;

    public GameObject NorthWall;
    public GameObject SouthWall;
    public GameObject EastWall;
    public GameObject WestWall;


    private List<MazeNode> mazeStack;
    private List<GameObject> platformList;

    private List<GameObject> SpawnedList;


    private bool statedCondition1;
    private bool statedCondition2;
    private bool statedCondition3;


    public void UpdateProjectilesText(int nb_Projectiles)
    {
        projectilesText.text = "Projectiles: " + nb_Projectiles;
    }

    public GameObject Spawn(GameObject gameObject, float Xpos, float Ypos, float Zpos)
    {
        return Instantiate(gameObject, new Vector3(Xpos, Ypos, Zpos), Quaternion.identity);
    }


    public void SpawnMaze(MazeNode[,] Maze, float height)
    {
        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int j = 0; j < Maze.GetLength(1); j++)
            {
                if (Maze[i, j].BlockType == 1)
                {
                    Spawn(BlockPrefab, 5 * i - 297, 2, 5 * j - 122);
                }
            }
        }
    }

    public void GenerateUniMaze()
    {
        Random.InitState(System.Environment.TickCount);
        ArrayList path = new ArrayList();
        ArrayList pathNodes = new ArrayList();

        int lastX = 0;
        int lastY = 0;

        int curX = 0;
        int curY = 0;

        int XLeft = 49;
        int YLeft = 25;

        int leftAllowed = -1;
        int rightAllowed = 50;
        int upAllowed = 25;
        int downAllowed = -1;

        SpawnBlock(0, 0, 1);

        int i = 0;

        int blockType = 1;


        int nextstep = Random.Range(1, 5);
        while (XLeft != 0 || YLeft != 0)
        {
            if (i > 500)
            {
                break;
            }

            if (i % 4 == 0)
            {
                blockType = 2;
            }
            else
            {
                blockType = 1;
            }

            if (nextstep == 1) // right
            {
                if (rightAllowed > 0 && (curX - lastX) != -1)
                {
                    lastX = curX;
                    curX++;
                    SpawnBlock(curX * 5, curY * 5, blockType);

                    XLeft--;

                    leftAllowed++;
                    rightAllowed--;
                }
            }
            else if (nextstep == 2 && (curY - lastY) != -1) // up
            {
                if (upAllowed > 0)
                {
                    lastY = curY;
                    curY++;
                    SpawnBlock(curX * 5, curY * 5, blockType);

                    YLeft--;

                    downAllowed++;
                    upAllowed--;
                }
            }
            else if (nextstep == 3 && (curX - lastX) != 1) // left
            {
                if (leftAllowed > 0)
                {
                    lastX = curX;
                    curX--;
                    SpawnBlock(curX * 5, curY * 5, blockType);

                    XLeft++;

                    rightAllowed++;
                    leftAllowed--;
                }
            }
            else if (nextstep == 4) // down
            {
                if (downAllowed > 0 && (curY - lastY) != 1)
                {
                    lastY = curY;
                    curY--;
                    SpawnBlock(curX * 5, curY * 5, blockType);

                    YLeft++;

                    upAllowed++;
                    downAllowed--;
                }
            }

            nextstep = Random.Range(1, 5);

            i++;
        }
    }


    void GenerateMaze()
    {
        List<MazeNode> stack = new List<MazeNode>(25);

        platformList = new List<GameObject>();
        // stack.Add();

        Random.InitState(System.Environment.TickCount);
        ArrayList path = new ArrayList();
        ArrayList pathNodes = new ArrayList();

        int lastX = 0;
        int lastY = 0;

        int curX = 0;
        int curY = 0;

        int XLeft = 5;
        int YLeft = 5;

        int leftAllowed = -1;
        int rightAllowed = 50;
        int upAllowed = 25;
        int downAllowed = -1;

        // SpawnBlock2(0, 0);

        stack.Add(Maze[0, 0]);


        int i = 0;


        int nextstep = Random.Range(1, 5);

        MazeNode current = Maze[0, 0];
        while (current.XPos1 != 4 || current.YPos1 < 3)
        {
            if (i > 500)
            {
                break;
            }

            current.IsUsed = true;

            List<MazeNode> unusedNeighbors = GetUnusedNeighbors(current);

            // Debug.Log(unusedNeighbors.Count);

            if (unusedNeighbors.Count > 0)
            {
                nextstep = Random.Range(0, unusedNeighbors.Count);

                // if (unusedNeighbors[nextstep].XPos1 < 4 && unusedNeighbors[nextstep].YPos1 < 3)
                // {
                //     // unusedNeighbors.RemoveAt(nextstep);
                //     // nextstep = Random.Range(0, unusedNeighbors.Count);
                //
                //     // if (unusedNeighbors.Count == 0)
                //     // {
                //     //     stack.RemoveAt(stack.Count - 1);
                //     //
                //     // }
                // }
                // else
                // {
                //     stack.Add(unusedNeighbors[nextstep]);
                // }

                stack.Add(unusedNeighbors[nextstep]);
            }
            else
            {
                stack.RemoveAt(stack.Count - 1);
            }

            // if (unusedNeighbors.Count > 0)
            // {
            //     // Debug.Log("Current:" + current.XPos1 + "," + current.YPos1);
            //     foreach (var mazeNode in unusedNeighbors)
            //     {
            //         Debug.Log("nei:" + mazeNode.XPos1 + "," + mazeNode.YPos1);
            //     }
            //
            //     Debug.Log("Choice:" + unusedNeighbors[nextstep].XPos1 + "," + unusedNeighbors[nextstep].YPos1);
            // }

            i++;

            current = stack[stack.Count - 1];
        }

        List<int> pathList = new List<int>();

        // Debug.Log("stackcount: " + stack.Count);
        for (int j = 1; j < stack.Count; j++)
        {
            // Debug.Log("i: " + j);
            int Xdiff = stack[j].XPos1 - stack[j - 1].XPos1;
            int Ydiff = stack[j].YPos1 - stack[j - 1].YPos1;

            if (Xdiff == 1)
            {
                pathList.Add(1);
            }
            else if (Xdiff == -1)
            {
                pathList.Add(3);
            }
            else if (Ydiff == 1)
            {
                pathList.Add(2);
            }
            else if (Ydiff == -1)
            {
                pathList.Add(4);
            }
        }

        // Debug.Log("Path: ");
        // foreach (int pat in pathList)
        // {
        //     // Debug.Log(pat);
        // }

        int index = 0;
        int path1 = 1;
        int path2 = pathList[index];


        // Debug.Log("Stack is: ");
        foreach (var mazeNode in stack)
        {
            // Debug.Log("stackel:" + mazeNode.XPos1 + "," + mazeNode.YPos1);

            mazeNode.IsSpawned = true;
            GameObject block = SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);

            mazeNode.CellGameObject = block;

            platformList.Add(block);

            if (path1 == path2)
            {
                if (path1 % 2 == 1)
                {
                    SpawnWall(NorthWall, block.GetComponent<Transform>());
                    SpawnWall(SouthWall, block.GetComponent<Transform>());
                }
                else
                {
                    SpawnWall(EastWall, block.GetComponent<Transform>());
                    SpawnWall(WestWall, block.GetComponent<Transform>());
                }
            }
            else
            {
                if ((path1 == 1 && path2 == 2) || (path1 == 4 && path2 == 3))
                {
                    SpawnWall(EastWall, block.GetComponent<Transform>());
                    SpawnWall(SouthWall, block.GetComponent<Transform>());
                }
                else if ((path1 == 1 && path2 == 4) || (path1 == 2 && path2 == 3))
                {
                    SpawnWall(EastWall, block.GetComponent<Transform>());
                    SpawnWall(NorthWall, block.GetComponent<Transform>());
                }
                else if ((path1 == 2 && path2 == 1) || (path1 == 3 && path2 == 4))
                {
                    SpawnWall(WestWall, block.GetComponent<Transform>());
                    SpawnWall(NorthWall, block.GetComponent<Transform>());
                }
                else if ((path1 == 4 && path2 == 1) || (path1 == 3 && path2 == 2))
                {
                    SpawnWall(WestWall, block.GetComponent<Transform>());
                    SpawnWall(SouthWall, block.GetComponent<Transform>());
                }
            }

            index++;

            path1 = path2;

            if (index > (stack.Count - 2))
            {
                path2 = 1;
            }
            else
            {
                path2 = pathList[index];
            }
        }

        mazeStack = stack;
    }

    private List<MazeNode> GetUnusedNeighbors(MazeNode mazeNode)
    {
        List<MazeNode> unusedNeighbors = new List<MazeNode>();
        if (mazeNode.XPos1 < 4 && !Maze[mazeNode.XPos1 + 1, mazeNode.YPos1].IsUsed)
        {
            unusedNeighbors.Add(Maze[mazeNode.XPos1 + 1, mazeNode.YPos1]);
        }

        if (mazeNode.XPos1 > 0 && !Maze[mazeNode.XPos1 - 1, mazeNode.YPos1].IsUsed)
        {
            unusedNeighbors.Add(Maze[mazeNode.XPos1 - 1, mazeNode.YPos1]);
        }

        if (mazeNode.YPos1 < 4 && !Maze[mazeNode.XPos1, mazeNode.YPos1 + 1].IsUsed)
        {
            unusedNeighbors.Add(Maze[mazeNode.XPos1, mazeNode.YPos1 + 1]);
        }

        if (mazeNode.YPos1 > 0 && !Maze[mazeNode.XPos1, mazeNode.YPos1 - 1].IsUsed)
        {
            // Debug.Log("down: " + Maze[mazeNode.XPos1, mazeNode.YPos1 - 1].IsUsed);
            // Debug.Log();
            unusedNeighbors.Add(Maze[mazeNode.XPos1, mazeNode.YPos1 - 1]);
        }

        return unusedNeighbors;
    }

    public void SpawnBlock(float x, float y, int blockType)
    {
        if (blockType == 1)
        {
            Spawn(BlockPrefab, x - 298, 2, y - 122);
        }
        else
        {
            Spawn(BlockandProjectilePrefab, x - 298, 2, y - 122);
        }
    }

    public GameObject SpawnBlock2(float x, float y)
    {
        return Spawn(PlatformPrefab, x * 20 - 40, 2, y * 20 + 2);
    }

    public GameObject SpawnWall(GameObject wall, Transform parentTransform)
    {
        GameObject wallObject = Instantiate(wall, parentTransform);
        var localScale = wall.transform.localScale;
        // wallObject.transform.parent = parent;
        wallObject.transform.localScale = localScale;

        return wallObject;
    }

    private void FillMaze()
    {
        bool allSpawned = false;

        while (!allSpawned)
        {
            Random.InitState(System.Environment.TickCount);
            allSpawned = true;
            foreach (MazeNode mazeNode in Maze)
            {
                if (!mazeNode.IsSpawned)
                {
                    allSpawned = false;
                    MazeNode neighbor = GetSpawnedNeighbor(mazeNode);

                    if (neighbor != null)
                    {
                        int Xdiff = mazeNode.XPos1 - neighbor.XPos1;
                        int Ydiff = mazeNode.YPos1 - neighbor.YPos1;
                        int path = 0;

                        if (Xdiff == 1)
                        {
                            path = 1;
                        }
                        else if (Xdiff == -1)
                        {
                            path = 3;
                        }
                        else if (Ydiff == 1)
                        {
                            path = 2;
                        }
                        else if (Ydiff == -1)
                        {
                            path = 4;
                        }
                        
                        AddCellIntoMaze(mazeNode, neighbor.CellGameObject, path);
                    }
                }
            }
        }
    }

    public void AddCellIntoMaze(MazeNode mazeNode, GameObject neighbor, int direction)
    {
        if (direction == 1)
        {
            Destroy(neighbor.transform.Find("EastWall"));
            
            mazeNode.IsSpawned = true;
            GameObject block = SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);

            mazeNode.CellGameObject = block;
            
            SpawnWall(NorthWall, block.GetComponent<Transform>());
            SpawnWall(EastWall, block.GetComponent<Transform>());
            SpawnWall(SouthWall, block.GetComponent<Transform>());


        }
        else if (direction == 2)
        {
            Destroy(neighbor.transform.Find("NorthWall"));
            
            mazeNode.IsSpawned = true;
            GameObject block = SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);

            mazeNode.CellGameObject = block;

            SpawnWall(WestWall, block.GetComponent<Transform>());
            SpawnWall(NorthWall, block.GetComponent<Transform>());
            SpawnWall(EastWall, block.GetComponent<Transform>());
        }
        else if (direction == 3)
        {
            Destroy(neighbor.transform.Find("WestWall"));
            
            mazeNode.IsSpawned = true;
            GameObject block = SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);

            mazeNode.CellGameObject = block;

            SpawnWall(SouthWall, block.GetComponent<Transform>());
            SpawnWall(WestWall, block.GetComponent<Transform>());
            SpawnWall(NorthWall, block.GetComponent<Transform>());
        }
        else
        {
            Destroy(neighbor.transform.Find("SouthWall"));
            
            mazeNode.IsSpawned = true;
            GameObject block = SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);

            mazeNode.CellGameObject = block;

            SpawnWall(EastWall, block.GetComponent<Transform>());
            SpawnWall(SouthWall, block.GetComponent<Transform>());
            SpawnWall(WestWall, block.GetComponent<Transform>());
        }

    }

    public MazeNode GetSpawnedNeighbor(MazeNode mazeNode)
    {
        List<MazeNode> spawnedNeighbors = new List<MazeNode>();
        if (mazeNode.XPos1 < 4 && Maze[mazeNode.XPos1 + 1, mazeNode.YPos1].IsSpawned)
        {
            spawnedNeighbors.Add(Maze[mazeNode.XPos1 + 1, mazeNode.YPos1]);
        }

        if (mazeNode.XPos1 > 0 && Maze[mazeNode.XPos1 - 1, mazeNode.YPos1].IsSpawned)
        {
            spawnedNeighbors.Add(Maze[mazeNode.XPos1 - 1, mazeNode.YPos1]);
        }

        if (mazeNode.YPos1 < 4 && Maze[mazeNode.XPos1, mazeNode.YPos1 + 1].IsSpawned)
        {
            spawnedNeighbors.Add(Maze[mazeNode.XPos1, mazeNode.YPos1 + 1]);
        }

        if (mazeNode.YPos1 > 0 && Maze[mazeNode.XPos1, mazeNode.YPos1 - 1].IsSpawned)
        {
            // Debug.Log("down: " + Maze[mazeNode.XPos1, mazeNode.YPos1 - 1].IsUsed);
            // Debug.Log();
            spawnedNeighbors.Add(Maze[mazeNode.XPos1, mazeNode.YPos1 - 1]);
        }
        Random.InitState(System.Environment.TickCount);

        if (spawnedNeighbors.Count == 0)
        {
            return null;

        }
        else
        {
            int index = Random.Range(0, spawnedNeighbors.Count - 1);
            return spawnedNeighbors[index];
        }
    }


    void Awake()
    {
        Unimaze = new MazeNode[25, 25];
        for (int i = 0; i < Unimaze.GetLength(0); i++)
        {
            for (int j = 0; j < Unimaze.GetLength(1); j++)
            {
                Unimaze[i, j] = new MazeNode(i, j, false);
            }
        }

        Maze = new MazeNode[5, 5];
        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int j = 0; j < Maze.GetLength(1); j++)
            {
                Maze[i, j] = new MazeNode(i, j, false);
            }
        }

        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        projectilesText = GameObject.FindGameObjectWithTag("ProjectileText").GetComponent<TextMeshProUGUI>();
        gameStateText = GameObject.FindGameObjectWithTag("GameStateText").GetComponent<TextMeshProUGUI>();
        playerGameObject = GameObject.FindWithTag("Player");
        playerScript = playerGameObject.GetComponent<Player>();


        SpawnedList = new List<GameObject>();
    }

    void Start()
    {
        GameEvents.current.OnZone1TriggerEnter += OnZone1Show;
        GameEvents.current.OnZone1TriggerExit += OnZone1UnShow;

        GameEvents.current.OnZone2TriggerEnter += OnZone2Show;
        GameEvents.current.OnZone2TriggerExit += OnZone2UnShow;

        GameEvents.current.OnZone3TriggerEnter += OnZone3Show;
        GameEvents.current.OnZone3TriggerExit += OnZone3UnShow;
        // Generate();

        gameStateText.text = "";


        GenerateUniMaze();

        GenerateMaze();

        FillMaze();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (CheckIfWin())
        {
            Win();
        }
        else
        {
            // Debug.Log("count: " + platformList.Count);
        }
    }

    public bool CheckIfWin()
    {
        // Debug.Log("winside: " + playerScript.IsOnWinSide);
        if (playerScript.IsOnWinSide)
        {
            bool win = false;
            // int left = 0;
            foreach (var plat in platformList)
            {
                if (plat == null)
                {
                    win = true;
                }
            }

            return win;
        }
        else
        {
            return false;
        }
    }

    public void Gameover()
    {
        gameStateText.text = "You lost!";
        Time.timeScale = 0;
    }

    public void Win()
    {
        gameStateText.text = "You won!";
        Time.timeScale = 0;
    }

    // public void StateCondition1()
    // {
    //     gameStateText.alpha = Single.MaxValue;
    //     gameStateText.text = "Goal: Cross the Canyon";
    //
    //     float beginTime = Time.time;
    //
    //     if (Time.time - beginTime > 5)
    //     {
    //         gameStateText.text = "";
    //     }
    // }

    private void OnZone1Show()
    {
        gameStateText.text = "Follow The Path And Collect The Projectiles";
    }

    private void OnZone1UnShow()
    {
        gameStateText.text = "";
    }

    private void OnZone2Show()
    {
        gameStateText.text = "Solve The Maze And Traverse It";
    }

    private void OnZone2UnShow()
    {
        gameStateText.text = "";
    }

    private void OnZone3Show()
    {
        gameStateText.text = "Destroy The Maze";
    }

    private void OnZone3UnShow()
    {
        gameStateText.text = "";
    }
}