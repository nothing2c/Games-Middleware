using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : CustomCollider
{
    public Vector3 Velocity = new Vector3(0, 0, 0);
    public Vector3 Accelleration = new Vector3(0, -9.8f, 0);
    public Vector3 PrevPos;
    public float Mass = 1f;

    public float radius;

    void Awake()
    {
        PrevPos = transform.position;
        radius = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        PrevPos = transform.position;
        Velocity += Accelleration * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;
    }

    public override bool IsCollidingWith(Sphere s)
    {
        float d2 = Vector3.Distance(transform.position, s.transform.position) - (radius + s.radius);

        return d2 <= 0;
    }
    public override bool IsCollidingWith(Plane p)
    {
        float distanceFromCenterToPlane = p.DistanceTo(transform.position);
        float d2 = distanceFromCenterToPlane - radius;

        return d2 <= 0;
    }
}
