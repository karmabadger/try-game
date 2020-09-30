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
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
        projectilesText = GameObject.FindGameObjectWithTag("ProjectileText").GetComponent<TextMeshProUGUI>();
        gameStateText = GameObject.FindGameObjectWithTag("GameStateText").GetComponent<TextMeshProUGUI>();
        playerGameObject = GameObject.FindWithTag("Player");
        playerScript = playerGameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        projectilesText.text = "Projectiles: " + playerScript.GetNbProjectiles();
    }
}
