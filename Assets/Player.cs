using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    private int m_NbProjectiles = 0;

    private Gamemanager m_Gamemanager;


    public GameObject projectilePrefab;
    
    
    public int GetNbProjectiles()
    {
        return m_NbProjectiles;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        m_Gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamemanager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, gameObject.GetComponent<Transform>());
        projectile.GetComponent<Rigidbody>().velocity = Vector3.forward;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            m_NbProjectiles++;
            m_Gamemanager.UpdateProjectilesText(m_NbProjectiles);
            Destroy(other.gameObject); // Or whatever way you want to remove the coin.
        }

        if (other.CompareTag("CanyonTrigger"))
        {
            
        }
    }
}
