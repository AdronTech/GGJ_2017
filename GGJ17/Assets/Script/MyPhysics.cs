using System;
using UnityEngine;

public class MyPhysics : MonoBehaviour {

    public Vector3 pos;
    public Vector3 vel;
    public Vector3 acc;
    public float maxVel;

	void Update () {
        pos = transform.position;

        GetComponent<Rigidbody>().MovePosition(transform.position + vel * Time.deltaTime);

        vel += acc * Time.deltaTime;

        if (vel.magnitude > maxVel)
        {
            vel.Normalize();
            vel *= maxVel;
        }

        acc = new Vector3();

        foreach (Steering steering in GetComponents<Steering>())
        {
            SteeringOutput so = steering.getSteering();
            acc += so.linear * steering.weight;
        }

    }

}
