using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour {

    public float panSpeed = 10, rotateSpeed = 50, camFocusHeight = 0;
    private Vector3 focusPoint, mousePosition;

	// Use this for initialization
	void Start () {
        Vector3 globalCamForwardDir = transform.forward;
        Vector3 downProjectedCamForward = Vector3.Project(transform.TransformVector(Vector3.forward), Vector3.down);
        float m = (transform.position.y - camFocusHeight) / downProjectedCamForward.magnitude;
        focusPoint = transform.position + globalCamForwardDir * m;
    }
	
	// Update is called once per frame
	void Update () {
        #region pan
        Vector3 localMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 groundMove = Vector3.ProjectOnPlane(transform.TransformVector(localMove), Vector3.up);
        groundMove.Normalize();
        Vector3 translation = groundMove * panSpeed * Time.deltaTime;
        transform.Translate(translation, Space.World);
        focusPoint += translation;
        #endregion
        // poll the mouse
        if (Input.GetMouseButton(1))
        #region rotate
        {
            Vector2 rotation = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            rotation *= rotateSpeed * Time.deltaTime;
            // horizontal
            transform.RotateAround(focusPoint, Vector3.up, rotation.x);
            // vertical
            // no. just. no. ... definately no.
            // transform.RotateAround(focusPoint, Vector3.right, rotation.y);
        }
        #endregion
    }

    public void MoveFocusTo(Vector3 m)
    {
        m.y = camFocusHeight;
        transform.Translate(m - focusPoint);
        focusPoint = m;
    }
}
