﻿using System;
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

    protected Rigidbody myRigit;

    void Awake()
    {
        myRigit = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {

        // collision
        Vector3 dir = vel.normalized;
        RaycastHit rh = new RaycastHit();
        if (myRigit.SweepTest(dir, out rh, vel.magnitude * Time.deltaTime) && !rh.collider.isTrigger)
        {
            if (rh.collider.GetComponent<AbstractBuildingBlock>() != null)
            {
                Vector3 off = rh.normal * 0.01f;
                vel = dir * rh.distance + off;
            }
        }

        // physics stuff :D
        pos = transform.position;
        pos += vel * Time.deltaTime;
        vel += acc * Time.deltaTime;
        acc = Vector3.zero;

        //ang = transform.rotation.y;
        ang += ang_vel * Time.deltaTime;
        ang_vel += ang_acc * Time.deltaTime;
        ang_acc = 0;

        // do not go too fast
        vel = Vector3.ClampMagnitude(vel, maxVel);
        //vel.y = 0;
        ang = ((((ang + 180) % 360) + 360) % 360) -180 ;
        ang_vel = Mathf.Clamp(ang_vel, -maxAngVel, maxAngVel);

        // drag
        Vector3 d = -vel;
        applyForce(d * drag);

        float ang_d = -ang_vel;
        ang_d *= drag;
        ang_acc += ang_d;

        // collect steering behaviours
        foreach (Steering steering in GetComponents<Steering>())
        {
            if (steering.Enabled)
            {
                SteeringOutput so = steering.getSteering();
                applyForce(so.linear * steering.weight);
                ang_acc += so.angular * steering.weight;
            }
        }

        // move
        myRigit.MovePosition(pos);
        myRigit.MoveRotation(Quaternion.Euler(0, ang, 0));
    }

    public void applyForce(Vector3 f)
    {
        acc += f;
    }

    public void stop()
    {
        vel = Vector3.zero;
        acc = Vector3.zero;
    }

}
