using System.Collections;
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
        if(prefab.tag == "DESTRUCTION")
        {
            w.HideNodes();
            constructionPrefab = null;
            isDestruct = true;
        }
        else if (a is Building_Wall)
        {
            w.ShowNodes(true, true);
        }
        else if (a is Building_Extractor)
        {
            w.ShowNodes(true, false);
        }
        else if (a is Building_Barracks)
        {
            w.ShowNodes(true, false);
        }
    }

	// Use this for initialization
	void Start () {
        myCam = GetComponent<Camera>();
        w = FindObjectOfType<WorldScript>();
        StartCoroutine(MouseLeftClick());
        StartCoroutine(MouseRightClick());
    }

    IEnumerator MouseLeftClick()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            {
                GameObject construction = constructionPrefab;
                RaycastHit hit;
                if (Physics.Raycast(myCam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (constructionPrefab)
                    {
                        BuildNode node = hit.collider.GetComponent<BuildNode>();
                        if (node)
                        {
                            Instantiate(construction, node.GetSpawnPosition(), Quaternion.identity);
                        }
                        yield return new WaitForFixedUpdate();
                        enableConstructionMode(constructionPrefab);
                    }
                    else if (isDestruct)
                    {
                        AbstractBaseBuilding a = hit.collider.GetComponent<AbstractBaseBuilding>();
                        if (a)
                        {
                            if (a is Building_Flag)
                            {}
                            else
                            {
                                a.HP = -1;
                            }
                        }
                    }
                }
            }
        }
    }
    public bool isDestruct = true;
    IEnumerator MouseRightClick()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
            constructionPrefab = null;
            isDestruct = false;
            w.HideNodes();
        }
    }
}
