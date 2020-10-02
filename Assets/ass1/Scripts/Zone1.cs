using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.Zone1TriggerEnter();
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.current.Zone1TriggerExit();
    }
}
