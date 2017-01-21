using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleEventSystem : MonoBehaviour
{

    public float timeUntilTitleSec;
    private GameObject Image;

    // Use this for initialization
    void Start()
    {
        Image = GameObject.FindGameObjectWithTag("Title Image");
        Image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeUntilTitleSec)
        {
            Image.SetActive(true);
            enabled = false;
        }
    }
}
