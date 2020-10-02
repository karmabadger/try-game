using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Destroy(other.transform.parent.gameObject);
            gameObject.SetActive(false);
            // Destroy(this.gameObject); 
        }
        else if (other.CompareTag("Player"))
        {
        }
        else if (other.CompareTag("PlatformWall"))
        {
            Destroy(other.transform.parent.gameObject);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Zone"))
        {
        }
        else
        {
            // Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}