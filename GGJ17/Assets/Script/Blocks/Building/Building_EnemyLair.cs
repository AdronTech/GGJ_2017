using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_EnemyLair : AbstractBuildingBlock {

    #region Instantiate
    public GameObject spawnPrefab;
    #endregion
    public float spawnFrequence = 20;

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
        StartCoroutine(SpawnDrops());
    }

    IEnumerator SpawnDrops()
    {
        ParticleSystem spwner = GetComponent<ParticleSystem>();
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequence);
            if(spwner)spwner.Emit(1);
        }
    }

    void OnParticleCollision(GameObject go)
    {
        Debug.Log("Blubl");
        if (go.tag.Equals("Ground"))
        {
            Debug.Log("Blubl" + go.transform.position);
            GameObject obj = Instantiate(spawnPrefab);
            obj.transform.position = go.transform.position + Vector3.up * 1.2f;
        }
    }
}
