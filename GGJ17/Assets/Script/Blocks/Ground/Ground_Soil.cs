﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Soil : AbstractBuildingBlock {

	// Use this for initialization
	void Start () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = true;
        bbi.down = bbi.sides = false;
        Init(bbi);
	}

}
