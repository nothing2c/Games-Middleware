using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsControl : MonoBehaviour
{
    PlaneScript plane;

    Vector3 Velocity  = new Vector3(0,0,0);
    Vector3 Accelleration = new Vector3(0, -9.8f, 0);

    readonly float radius = 0.5f;
    readonly float coefficiantOfRestitution = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        plane = FindObjectOfType<PlaneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float d1 = plane.DistanceTo(transform.position) - radius;
        Velocity += Accelleration * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;
        float distanceFromCenterToPlane = plane.DistanceTo(transform.position);
        float d2 = distanceFromCenterToPlane - radius;

        if(d2 <= 0)
        {
            Vector3 para = ParallellComp(Velocity, plane.NormalToPlane);
            Vector3 perp = PerpendicularComp(Velocity, plane.NormalToPlane);

            float timeOfImpact = Time.deltaTime * (d1 / (d1 - d2));
            transform.position -= Velocity * (Time.deltaTime - timeOfImpact);

            Velocity = perp - coefficiantOfRestitution * para;

            transform.position += Velocity * (Time.deltaTime - timeOfImpact);
        }
    }

    private Vector3 ParallellComp(Vector3 vec, Vector3 normal)
    {
        return Vector3.Dot(vec, normal) * normal;
    }

    private Vector3 PerpendicularComp(Vector3 vec, Vector3 normal)
    {
        return vec - ParallellComp(vec, normal);
    }
}
