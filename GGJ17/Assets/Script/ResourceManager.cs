using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float ousaResource, ousaDelta, timeWait;

    // Use this for initialization
    void Start()
    {
        ousaResource = 0f;
        ousaDelta = 0f;
        timeWait = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeWait += Time.deltaTime;

        if (timeWait >= 1.0f)
        {
            --timeWait;
            updateResources();
        }
    }

    private void updateResources()
    {
        ousaResource += ousaDelta;
    }

    public void RegisterResources(float resource_value)
    {
        ousaDelta += resource_value;
    }

    public void DeregisterResources(float resource_value)
    {
        ousaDelta -= resource_value;
    }
}
