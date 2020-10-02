using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int m_NbProjectiles = 110;

    private Gamemanager m_Gamemanager;


    public GameObject projectilePrefab;

    private GameObject bullet;


    public int GetNbProjectiles()
    {
        return m_NbProjectiles;
    }


    // Start is called before the first frame update
    void Start()
    {
        // projectilePrefab = Resources.Load("Projectile") as GameObject;
        m_Gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Gamemanager>();

        bullet = Instantiate(projectilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        bullet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_NbProjectiles > 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector3 vector = gameObject.GetComponent<Transform>().position;
        var forward = Camera.main.transform.forward;


        // GameObject projectile = Instantiate(projectilePrefab, vector + forward * 2, Quaternion.identity);
        GameObject projectile = bullet;
        bullet.GetComponent<Transform>().position = vector + forward * 2;


        bullet.GetComponent<Transform>().rotation = Camera.main.transform.rotation * Quaternion.Euler(1,1,1);


        bullet.SetActive(true);

        projectile.GetComponent<Rigidbody>().velocity = forward * 40;

        // projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1));

        Debug.Log(projectile.GetComponent<Rigidbody>().velocity);

        m_NbProjectiles--;
        m_Gamemanager.UpdateProjectilesText(m_NbProjectiles);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            m_NbProjectiles++;
            m_Gamemanager.UpdateProjectilesText(m_NbProjectiles);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CanyonTrigger"))
        {
            m_Gamemanager.Gameover();
        }
    }
}