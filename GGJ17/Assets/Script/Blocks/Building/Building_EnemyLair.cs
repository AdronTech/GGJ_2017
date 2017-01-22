using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_EnemyLair : AbstractBuildingBlock {

    #region Instantiate
    public GameObject spawnPrefab;
    #endregion
    public float spawnFrequence = 20, startDelay = 1;

    // Use this for initialization
    void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = false;
        bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }
    
    void Start()
    {
        StartCoroutine(GetComponentInChildren<LairCannon>().SpawnDrops(this, startDelay));
    }




}
