using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour {

    public Vector2 size = new Vector2(64,64);

    public GameObject groundSoil, groundResource;

    void OnValidate()
    {
        size = new Vector2(Mathf.Floor(Mathf.Abs(size.x)), Mathf.Floor(Mathf.Abs(size.y)));
    }

    public void GenerateWorld()
    {
        // clean previous ground
        foreach (Transform t in transform.GetComponentInChildren<Transform>())
        {
            if(transform != t)
            {
                Destroy(t.gameObject);
            }
        }

        // create new ground
        for(int x = 0; x<size.x; x++)
        {
            for(int y = 0; y<size.y; y++)
            {
                GameObject o = Instantiate(groundSoil, transform);
                o.transform.position = Vector3.forward * x + Vector3.right * y;
            }
        }
    }
}
