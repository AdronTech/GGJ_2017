using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {

    public Vector2 size = new Vector2(64,64);
    public GameObject groundSoil, groundResource;
    public AbstractBuildingBlock[,] blocks;

    public GameObject flagPrefab;
    [HideInInspector]
    public AbstractBuildingBlock flag;

    void Start()
    {
        Vector3 flagSpawn = blocks[(int)size.x / 2, (int)size.y / 2].transform.position;
        flagSpawn.y = 1;
        flag = Instantiate(flagPrefab).GetComponent<AbstractBuildingBlock>();
        flag.transform.position = flagSpawn;
        HideNodes();
    }

    void OnValidate()
    {
        size = new Vector2(Mathf.Floor(Mathf.Abs(size.x)), Mathf.Floor(Mathf.Abs(size.y)));
    }

    public void GenerateWorld()
    {
        blocks = new AbstractBuildingBlock[(int)size.x, (int)size.y];
        foreach(AbstractBuildingBlock a in GetComponentsInChildren<AbstractBuildingBlock>())
        {
            DestroyImmediate(a.gameObject);
        }
        // create new ground
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                blocks[x,y] = Instantiate(groundSoil, transform).GetComponent<AbstractBuildingBlock>();
                blocks[x, y].transform.position = Vector3.forward * x + Vector3.right * y;
            }
        }
    }

    public void ShowNodes(bool topN, bool sidesN)
    {
        HideNodes();
        // activate all building nodes needed
        foreach(AbstractBuildingBlock a in FindObjectsOfType<AbstractBuildingBlock>())
        {
            if(a.tag == "Building")
            {
                a.ShowNodes(topN, sidesN);
            }
        }

        //sweep the floor
        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                if (blocks[x, y].isNodesActive)
                {
                    if (x - 1 >= 0) blocks[x - 1, y].ShowNodes(true, false);
                    if (y - 1 >= 0) blocks[x, y - 1].ShowNodes(true, false);
                }
                else
                {
                    if(x > 0)
                        if(blocks[x - 1, y].isNodesActive)
                        {
                            blocks[x, y].ShowNodes(true, false);
                        }
                    if(y > 0)
                        if(blocks[x, y - 1].isNodesActive)
                        {
                            blocks[x, y].ShowNodes(true, false);
                        }
                }
            }
    }

    public void HideNodes()
    {
        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++)
            {
                if (blocks[x, y].isNodesActive)
                {
                    blocks[x, y].HideNodes();
                }
            }
    }
}
