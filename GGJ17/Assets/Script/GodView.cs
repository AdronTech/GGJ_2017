using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodView : MonoBehaviour {

    Camera myCam;
    [SerializeField]
    private GameObject constructionPrefab;

    public void enableConstructionMode(GameObject prefab)
    {

    }

	// Use this for initialization
	void Start () {
        myCam = GetComponent<Camera>();
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
                    Instantiate(constructionPrefab, node.SpawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
