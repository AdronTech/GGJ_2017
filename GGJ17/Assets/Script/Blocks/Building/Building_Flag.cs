using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Flag : AbstractBaseBuilding {

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = false;
        bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }
}
