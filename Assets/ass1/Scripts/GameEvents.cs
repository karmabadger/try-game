using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public event Action OnZone1TriggerEnter;
    public void Zone1TriggerEnter()
    {
        if (OnZone1TriggerEnter != null)
        {
            OnZone1TriggerEnter();
        }
    }

    public event Action OnZone1TriggerExit;
    public void Zone1TriggerExit()
    {
        if (OnZone1TriggerExit != null)
        {
            OnZone1TriggerExit();
        }
    }

    public event Action OnZone2TriggerEnter;
    public void Zone2TriggerEnter()
    {
        if (OnZone2TriggerEnter != null)
        {
            OnZone2TriggerEnter();
        }
    }

    public event Action OnZone2TriggerExit;
    public void Zone2TriggerExit()
    {
        if (OnZone2TriggerExit != null)
        {
            OnZone2TriggerExit();
        }
    }
    
    public event Action OnZone3TriggerEnter;
    public void Zone3TriggerEnter()
    {
        if (OnZone3TriggerEnter != null)
        {
            OnZone3TriggerEnter();
        }
    }

    public event Action OnZone3TriggerExit;
    public void Zone3TriggerExit()
    {
        if (OnZone3TriggerExit != null)
        {
            OnZone3TriggerExit();
        }
    }
}
