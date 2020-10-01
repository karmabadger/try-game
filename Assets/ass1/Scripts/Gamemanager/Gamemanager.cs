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


    private List<MazeNode> mazeStack;


    public void UpdateProjectilesText(int nb_Projectiles)
    {
        projectilesText.text = "Projectiles: " + nb_Projectiles;
    }

    public void Spawn(GameObject gameObject, float Xpos, float Ypos, float Zpos)
    {
        Instantiate(gameObject, new Vector3(Xpos, Ypos, Zpos), Quaternion.identity);
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

        // Debug.Log("yoyyoyoyo");
        // Debug.Log(curX);
        // Debug.Log(curY);
        // Debug.Log(XLeft);
        // Debug.Log(YLeft);
    }


    void GenerateMaze()
    {
        List<MazeNode> stack = new List<MazeNode>(25);
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
        while (current.XPos1 != 4)
        {
            if (i > 500)
            {
                break;
            }

            current.IsUsed = true;

            List<MazeNode> unusedNeighbors = GetUnusedNeighbors(current);

            Debug.Log(unusedNeighbors.Count);

            if (unusedNeighbors.Count > 0)
            {
                nextstep = Random.Range(0, unusedNeighbors.Count);
                stack.Add(unusedNeighbors[nextstep]);
            }
            else
            {
                stack.RemoveAt(stack.Count - 1);
            }

            if (unusedNeighbors.Count > 0)
            {
                Debug.Log("Current:" + current.XPos1 + "," + current.YPos1);
                foreach (var mazeNode in unusedNeighbors)
                {
                    Debug.Log("nei:" + mazeNode.XPos1 + "," + mazeNode.YPos1);
                }
                Debug.Log("Choice:" + unusedNeighbors[nextstep].XPos1 + "," + unusedNeighbors[nextstep].YPos1);
            }

            i++;

            current = stack[stack.Count - 1];
        }

        Debug.Log("Stack is: ");
        foreach (var mazeNode in stack)
        {
            Debug.Log("stackel:" + mazeNode.XPos1 + "," + mazeNode.YPos1);
            SpawnBlock2(mazeNode.XPos1, mazeNode.YPos1);
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
            Debug.Log("down: " + Maze[mazeNode.XPos1, mazeNode.YPos1 - 1].IsUsed);
            // Debug.Log();
            unusedNeighbors.Add(Maze[mazeNode.XPos1, mazeNode.YPos1 - 1]);
        }

        return unusedNeighbors;
    }

    // void Generate()
    // {
    //     Random.InitState(System.Environment.TickCount);
    //     List<Vector2Int> a = new List<Vector2Int>();
    //
    //     int x = 5;
    //     int y = 5;
    //     a.Add(new Vector2Int(5, 5));
    //     CreateFloor(5, 5);
    //     int i = Random.Range(1, 5); //genereate 1,2,3,4
    //     while (x != 7*5 || y != 7*5)
    //     {
    //         if (i == 2 && y - 3 * 5 >= 1 * 5 && !a.Contains(new Vector2Int(x, y - 3 * 5)) && !(x==7*5 && y==4*5))//move backward
    //         {
    //
    //             CreateFloor(x, y - 3 * 5);
    //             CreateFloor(x, y - 2 * 5);
    //             CreateFloor(x, y - 1 * 5);
    //             y = y - 3 * 5;
    //             a.Add(new Vector2Int(x * 5, y * 5));
    //
    //         }
    //         else if (i == 1 && y + 3 * 5 <= 7 * 5 && !a.Contains(new Vector2Int(x, y + 3 * 5)))//move forward
    //         {
    //             CreateFloor(x, y + 3 * 5);
    //             CreateFloor(x, y + 2 * 5);
    //             CreateFloor(x, y + 1 * 5);
    //             y = y + 3 * 5;
    //             a.Add(new Vector2Int(x, y));
    //             
    //         }
    //          
    //         else if (i == 3 && x - 3 * 5 >= 1 * 5 && !a.Contains(new Vector2Int(x - 3 * 5, y)))//move left
    //         {
    //
    //             CreateFloor(x - 3 * 5, y);
    //             CreateFloor(x - 2 * 5, y);
    //             CreateFloor(x - 1 * 5, y);
    //             x = x - 3 * 5;
    //             a.Add(new Vector2Int(x, y));
    //             
    //         }
    //         else if (i == 4 && x + 3 * 5 <= 7 * 5 && !a.Contains(new Vector2Int(x+3*5, y )))//move forward
    //         {
    //
    //             CreateFloor(x + 3 * 5, y);
    //             CreateFloor(x + 2 * 5, y);
    //             CreateFloor(x + 1 * 5, y);
    //             x = x + 3 * 5;
    //             a.Add(new Vector2Int(x, y));
    //             
    //         }
    //         i = Random.Range(1, 5);//genereate 1,2,3,4
    //
    //
    //     }
    //
    // }


    public void SpawnBlock(float x, float y, int blockType)
    {
        if (blockType == 1)
        {
            Spawn(BlockPrefab, x - 298, 2, y - 122);
        }
        else
        {
            Spawn(BlockandProjectilePrefab, x - 297, 2, y - 122);
        }
    }

    public void SpawnBlock2(float x, float y)
    {
        Spawn(PlatformPrefab, x * 20 - 40, 2, y * 20 + 2);
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
    }

    void Start()
    {
        // Generate();

        gameStateText.text = "";


        GenerateUniMaze();

        GenerateMaze();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
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
}