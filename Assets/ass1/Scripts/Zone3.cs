using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.Zone3TriggerEnter();
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.Zone3TriggerExit();
    }
}
