using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleInput : MonoBehaviour
{

    public float waitBeforeInputSec;
    public string nextSceneName;
    private float sceneStartTime;

    // Use this for initialization
    void Start()
    {
        sceneStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - sceneStartTime >= waitBeforeInputSec && Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
