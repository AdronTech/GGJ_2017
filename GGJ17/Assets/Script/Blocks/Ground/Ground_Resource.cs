using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Resource : AbstractBuildingBlock {

	// Use this for initialization
	void Start () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = true;
        bbi.down = false;
        bbi.left = true;
        bbi.right = true;
        bbi.front = true;
        bbi.back = true;
        Init(bbi);
	}

}
