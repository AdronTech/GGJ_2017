
using System;
using UnityEngine;

[Serializable]
public class SeekBehaviour : Steering {

    public Transform target;

    public float maxAcc;
    public float maxSpeed;
    public float radius;

    public override SteeringOutput getSteering()
    {

        SteeringOutput steering = new SteeringOutput();

        if (target == null) return steering;

        Vector3 des = target.position - my.pos;
        float d = des.magnitude;
        des.Normalize();

        if(d < radius)
        {
            des *= d * maxSpeed / radius;
        }
        else
        {
            des *= maxSpeed;
        }

        steering.linear = des - my.vel;
        steering.linear = Vector3.ClampMagnitude(steering.linear, maxAcc);

        my.ang = Mathf.Atan2(-des.z, des.x) * Mathf.Rad2Deg;
        return steering;
    }

}
