using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Extractor : AbstractBaseBuilding {

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = bbi.down = true;
        bbi.sides = false;
        Init(bbi);
    }
}
