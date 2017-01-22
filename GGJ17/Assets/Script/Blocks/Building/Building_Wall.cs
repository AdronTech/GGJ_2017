using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Wall : AbstractBaseBuilding {

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = true;
        bbi.down = true;
        bbi.sides = true;
        Init(bbi);
    }
}
