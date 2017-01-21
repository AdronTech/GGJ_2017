using System;
using UnityEngine;

public class MyPhysics : MonoBehaviour {

    public Vector3 pos;
    public Vector3 vel;
    public Vector3 acc;

    public float ang;
    public float ang_vel;
    public float ang_acc;

    public float maxVel;
    public float maxAngVel;
    public float drag = 0.01f;

	void Update () {

        // physics stuff :D
        pos = transform.position;
        pos += vel * Time.deltaTime;
        vel += acc * Time.deltaTime;
        acc *= 0;

        //ang = transform.rotation.y;
        ang += ang_vel * Time.deltaTime;
        ang_vel += ang_acc * Time.deltaTime;
        ang_acc *= 0;

        // do not go too fast
        Vector3.ClampMagnitude(vel, maxVel);
        ang = ((((ang + 180) % 360) + 360) % 360) -180 ;
        ang_vel = Mathf.Clamp(ang_vel, -maxAngVel, maxAngVel);

        // drag
        Vector3 d = -vel;
        d *= drag;
        applyForce(d);

        float ang_d = -ang_vel;
        ang_d *= drag;
        ang_acc += ang_d;

        // collect steering behaviours
        foreach (Steering steering in GetComponents<Steering>())
        {
            SteeringOutput so = steering.getSteering();
            applyForce(so.linear * steering.weight);
            ang_acc += so.angular * steering.weight;
        }

        // move
        GetComponent<Rigidbody>().MovePosition(pos);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, ang, 0));
    }

    public void applyForce(Vector3 f)
    {
        acc += f;
    }

}
