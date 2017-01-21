using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour {

    public float panSpeed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 localMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 groundMove = Vector3.ProjectOnPlane(transform.TransformVector(localMove), Vector3.up);
        groundMove.Normalize();
        transform.Translate(groundMove * panSpeed * Time.deltaTime, Space.World);
	}
}
