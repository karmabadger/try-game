using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.Zone2TriggerEnter();
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.Zone2TriggerExit();
    }
}
