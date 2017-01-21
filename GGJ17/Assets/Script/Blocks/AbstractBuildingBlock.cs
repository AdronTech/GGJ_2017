﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuildingBlock : MonoBehaviour {
    
    public struct BuildingBlockInit
    {
        public bool up , down, left, right, front, back;
    }

    #region Static
    public const int up = 0, down = 1, left = 2, right = 3, front = 4, back = 5;
    public static Vector3 SpawnPoint(Vector3 parent, int orientation)
    {
        switch (orientation)
        {
            case AbstractBuildingBlock.up:
                return parent + Vector3.up;
            case AbstractBuildingBlock.down:
                return parent + Vector3.down;
            case AbstractBuildingBlock.left:
                return parent + Vector3.left;
            case AbstractBuildingBlock.right:
                return parent + Vector3.right;
            case AbstractBuildingBlock.front:
                return parent + Vector3.back;
            case AbstractBuildingBlock.back:
                return parent + Vector3.forward;
        }
        return Vector3.zero;
    }
    #endregion

    #region Instantiation
    [SerializeField]
    private GameObject nodePrefab;
    #endregion

    public bool penismode = false;

    public AbstractBuildingBlock[] neighbors = new AbstractBuildingBlock[6];
    protected BuildNode[] nodes = new BuildNode[6];

	// Use this for initialization
	protected void Init(BuildingBlockInit init) {
        Rigidbody r = GetComponent<Rigidbody>();
        r.freezeRotation = !penismode;
        if (init.up)
        {
            nodes[up] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[up].Init(up, this);
        }
        if (init.down)
        {
            nodes[down] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[down].Init(down, this);
        }
        if (init.left)
        {
            nodes[left] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[left].Init(left, this);
        }
        if (init.right)
        {
            nodes[right] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[right].Init(right, this);
        }
        if (init.front)
        {
            nodes[front] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[front].Init(front, this);
        }
        if (init.back)
        {
            nodes[back] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[back].Init(back, this);
        }
    }

    #region ActivateNodes
    public bool nodesActive = false;
    public void EnableNodes(bool topN, bool downN, bool sidesN)
    {
        nodesActive = true;
        if (topN)
        {
            nodes[up].Enable = true;
        }
        if (downN)
        {
            nodes[down].Enable = true;
        }
        if (sidesN)
        {
            nodes[left].Enable = true;
            nodes[right].Enable = true;
            nodes[front].Enable = true;
            nodes[back].Enable = true;
        }
        foreach(AbstractBuildingBlock a in neighbors)
        {
            if (!a.nodesActive) a.EnableNodes(topN, downN, sidesN);
        }
    }
    public void Disable()
    {
        foreach(BuildNode n in nodes)
        {
            n.Enable = false;
        }
    }
    #endregion
}
