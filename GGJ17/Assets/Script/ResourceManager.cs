using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public float ousiaResource, ousiaDelta, timeWait;
    public Text resourceText;

    // Use this for initialization
    void Start()
    {
        ousiaResource = 0f;
        ousiaDelta = 0f;
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
        ousiaResource += ousiaDelta;
        resourceText.text = ousiaResource.ToString();
    }

    public void RegisterResources(float resource_value)
    {
        ousiaDelta += resource_value;
    }

    public void DeregisterResources(float resource_value)
    {
        ousiaDelta -= resource_value;
    }
}
