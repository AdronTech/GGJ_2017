using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_EnemyLair : AbstractBuildingBlock {

	// Use this for initialization
	void Awake () {
        BuildingBlockInit bbi = new BuildingBlockInit();
        bbi.up = true;
        bbi.down = true;
        bbi.sides = true;
        Init(bbi);
    }

    public new void ShowNodes(bool topN, bool botN) { }
    public new void HideNodes() { }
}
