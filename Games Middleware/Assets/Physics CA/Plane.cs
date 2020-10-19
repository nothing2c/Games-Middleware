using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : CustomCollider
{
    Vector3 PointOnPlane;
    [SerializeField]
    public Vector3 Normal { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DefinePoint(new Vector3(0, -1, 0), new Vector3(0.03f, 1, 0));
    }

    public void DefinePoint(Vector3 point, Vector3 normal)
    {
        PointOnPlane = transform.position;
        Normal = transform.up;
    }

    public float DistanceTo(Vector3 point)
    {
        Vector3 pointOnPlaneToPoint = point - PointOnPlane;
        return Vector3.Dot(pointOnPlaneToPoint, Normal);
    }

    public override bool IsCollidingWith(Plane s)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsCollidingWith(Sphere s)
    {
        throw new System.NotImplementedException();
    }
}
