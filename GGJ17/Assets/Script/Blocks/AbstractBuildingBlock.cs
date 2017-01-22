using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBuildingBlock : MonoBehaviour {
    
    public struct BuildingBlockInit
    {
        public bool up, down, sides;
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
	public void Init(BuildingBlockInit init) {
        Rigidbody r = GetComponent<Rigidbody>();
        r.freezeRotation = !penismode;
        if (init.up)
        {
            nodes[up] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[up].Init(up, this, false);
        }
        if (init.down)
        {
            nodes[down] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[down].Init(down, this, true);
        }
        if (init.sides)
        {
            nodes[left] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[left].Init(left, this, false);
            nodes[right] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[right].Init(right, this, false);
            nodes[front] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[front].Init(front, this, false);
            nodes[back] = Instantiate(nodePrefab, transform).GetComponent<BuildNode>();
            nodes[back].Init(back, this, false);
        }
    }

    #region ActivateNodes
    public bool BlockOccupied
    {
        get
        {
            return neighbors[up];
        }
    }

    public void ShowNodes(bool topN, bool sidesN)
    {
        if (topN && !neighbors[up] && nodes[up])
        {
            nodes[up].Show();
        }
        if (sidesN)
        {
            if (!neighbors[left] && nodes[left])
            {
                nodes[left].Show();
            }
            if (!neighbors[right] && nodes[right])
            {
                nodes[right].Show();
            }
            if (!neighbors[front] && nodes[front])
            {
                nodes[front].Show();
            }
            if (!neighbors[back] && nodes[back])
            {
                nodes[back].Show();
            }
        }
    }

    public void HideNodes()
    {
        foreach(BuildNode n in nodes)
        {
            if(n) n.Hide();
        }
    }
    #endregion
}
