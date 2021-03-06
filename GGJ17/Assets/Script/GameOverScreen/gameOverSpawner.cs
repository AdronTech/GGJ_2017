﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverSpawner : MonoBehaviour
{

    public GameObject psiPrefab;
    public float spawnDuration;
    public float waitBetween;
    private float sceneStartTime;

    // Use this for initialization
    void Start()
    {
        sceneStartTime = Time.time;
        StartCoroutine(PsiRain(psiPrefab));
    }

    IEnumerator PsiRain(GameObject prefab)
    {
        do
        {
            Instantiate(prefab, new Vector3(0, 6, 0), Quaternion.Euler(new Vector3(Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f))));
            yield return new WaitForSeconds(waitBetween);
        } while (spawnDuration > Time.time - sceneStartTime);
    }
}
