using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverEventSystem : MonoBehaviour
{

    public float timeUntilTitleSec;
    public GameObject Image, Text;

    // Use this for initialization
    void Start()
    {
        Image.SetActive(false);
        Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeUntilTitleSec)
        {
            Image.SetActive(true);
            Text.SetActive(true);
            enabled = false;
        }
    }
}
