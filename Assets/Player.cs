using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private int m_NbProjectiles = 0;

    private Gamemanager m_Gamemanager;

    public int GetNbProjectiles()
    {
        return m_NbProjectiles;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_Gamemanager = GameObject.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            m_NbProjectiles++;
            Destroy(other.gameObject); // Or whatever way you want to remove the coin.
        }

        if (other.CompareTag("CanyonTrigger"))
        {
            
        }
    }
}
