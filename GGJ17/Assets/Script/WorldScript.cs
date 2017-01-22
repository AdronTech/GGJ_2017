using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {

    #region Instantiation
    public GameObject groundSoil, groundResource;
    public GameObject flagPrefab;
    #endregion
    [Range(0f, 1f)]
    public float resourceAbundance;
    public Vector2 size = new Vector2(64,64);

    public AbstractBuildingBlock[,] blocks;

    void Awake()
    {
        GenerateWorld();
    }

    void Start()
    {
        Vector3 flagSpawn = blocks[(int)size.x / 2, (int)size.y / 2].transform.position;
        flagSpawn.y = 1;
        AbstractBuildingBlock flag = Instantiate(flagPrefab).GetComponent<AbstractBuildingBlock>();
        flag.transform.position = flagSpawn;
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return null;
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
                float rng = Random.value;
                if (rng <= resourceAbundance)
                {
                    blocks[x, y] = Instantiate(groundResource, transform).GetComponent<AbstractBuildingBlock>();
                }
                else
                {
                    blocks[x, y] = Instantiate(groundSoil, transform).GetComponent<AbstractBuildingBlock>();
                }
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
        {
            for (int y = 0; y < size.y; y++)
            {
                if (blocks[x, y].BlockOccupied)
                {
                    blocks[x, y].ShowNodes(true, false);
                    if (x > 0) blocks[x - 1, y].ShowNodes(true, false);
                    if (x < size.x - 1) blocks[x + 1, y].ShowNodes(true, false);
                    if (y > 0) blocks[x, y - 1].ShowNodes(true, false);
                    if (y < size.y - 1) blocks[x, y + 1].ShowNodes(true, false);
                }
            }
        }
            
    }

    public void HideNodes()
    {
        foreach (AbstractBuildingBlock a in FindObjectsOfType<AbstractBuildingBlock>())
        {
            a.HideNodes();
        }
    }
}
