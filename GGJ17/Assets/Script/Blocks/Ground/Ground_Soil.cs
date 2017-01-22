using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Soil : AbstractBuildingBlock {

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = true;
        bbi.down = bbi.sides = false;
        Init(bbi);
	}

}
