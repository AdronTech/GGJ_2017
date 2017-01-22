using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Extractor : AbstractBaseBuilding {

    public float normalResourceGain = 0.1f, energizedResourceGain = 5f;
    public bool isEnergized = false;

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }

    void Start()
    {
        isEnergized = neighbors[down] is Ground_Resource;
        ResourceManager manager = FindObjectOfType<ResourceManager>();
        manager.RegisterResources(isEnergized ? energizedResourceGain : normalResourceGain);
    }

    public new void DestroyBuilding()
    {
        ResourceManager manager = FindObjectOfType<ResourceManager>();
        manager.DeregisterResources(isEnergized ? energizedResourceGain : normalResourceGain);
    }
}
