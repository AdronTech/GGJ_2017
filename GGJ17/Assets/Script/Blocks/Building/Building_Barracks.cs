using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Barracks : AbstractBuildingBlock {

	// Use this for initialization
	void Start () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = bbi.down = true;
        bbi.left =  bbi.right = true;
        bbi.front = bbi.back = true;
        Init(bbi);
    }
}
