using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Gamemanager : MonoBehaviour
{
    
    // private int m_NbProjectiles = 0;

    private GameObject playerGameObject;
    private Player playerScript;

    private Canvas canvas;
    public TextMeshProUGUI projectilesText;
    private TextMeshProUGUI gameStateText;

    private Node[,]Unimaze;

    private Node[,] Maze;


    void Awake()
    {
        Unimaze = new Node[50, 50];
        for (int i = 0; i < Unimaze.GetLength(0); i++)
        {
            for (int j = 0; j < Unimaze.GetLength(1); j++)
            {
                Unimaze[i, j] = new Node();
            }
        }
        
        Maze = new Node[5, 5];
        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int j = 0; j < Maze.GetLength(1); j++)
            {
                Maze[i, j] = new Node();
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
        
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        projectilesText.text = "Projectiles: " + playerScript.GetNbProjectiles();
    }
}
