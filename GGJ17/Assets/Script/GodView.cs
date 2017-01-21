﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodView : MonoBehaviour {

    Camera myCam;
    [SerializeField]
    private GameObject constructionPrefab;
    WorldScript w;

    public void enableConstructionMode(GameObject prefab)
    {
        constructionPrefab = prefab;
        AbstractBuildingBlock a = prefab.GetComponent<AbstractBuildingBlock>();
        if (a is Building_Wall)
        {
            w.flag.ShowNodes(true, true);
        }
        else if (a is Building_Extractor)
        {
            w.flag.ShowNodes(true, false);
        }
        else if (a is Building_Barracks)
        {
            w.flag.ShowNodes(true, false);
        }
        w.ShowBuildNodes();
    }

	// Use this for initialization
	void Start () {
        myCam = GetComponent<Camera>();
        w = FindObjectOfType<WorldScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray r = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                BuildNode node = hit.collider.GetComponent<BuildNode>();
                if (node)
                {
                    Instantiate(constructionPrefab, node.GetSpawnPosition(), Quaternion.identity);
                }
            }
        }
    }
}
