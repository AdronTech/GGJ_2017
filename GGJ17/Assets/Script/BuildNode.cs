using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    MeshRenderer renderer;
    public Material mouseEnter, mouseExit;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        renderer.material = mouseEnter;
    }

    void OnMouseExit()
    {
        renderer.material = mouseExit;
    }
}
