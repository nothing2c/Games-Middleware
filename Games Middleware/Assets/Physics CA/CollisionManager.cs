using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static List<Sphere> Spheres;
    public static List<Plane> Planes;
    public static List<CustomCollider> Colliders;

    public static float CoefficiantOfRestitution = 0.6f;

    void Start()
    {
        Spheres = new List<Sphere>(FindObjectsOfType<Sphere>());
        Planes = new List<Plane>(FindObjectsOfType<Plane>());
        Colliders = new List<CustomCollider>(FindObjectsOfType<CustomCollider>());
    }

    private void LateUpdate()
    {
        for (int i = 0; i < Spheres.Count; i++)
        {
            for (int j = 0; j < Planes.Count; j++)
            {
                Plane p = Planes[j];
                Sphere s1 = Spheres[i];

                if (s1.IsCollidingWith(p))
                {
                    float d1 = p.DistanceTo(s1.PrevPos) - s1.radius;
                    float distanceFromCenterToPlane = p.DistanceTo(s1.transform.position);
                    float d2 = distanceFromCenterToPlane - s1.radius;

                    Vector3 para = ParallellComp(s1.Velocity, p.Normal);
                    Vector3 perp = PerpendicularComp(s1.Velocity, p.Normal);

                    float timeOfImpact = Time.deltaTime * (d1 / (d1 - d2));
                    s1.transform.position -= s1.Velocity * (Time.deltaTime - timeOfImpact);
                    s1.PrevPos = s1.transform.position;

                    s1.Velocity = perp - CoefficiantOfRestitution * para;

                    s1.transform.position += s1.Velocity * (Time.deltaTime - timeOfImpact);
                }
            }

            for (int j = i + 1; j < Spheres.Count; j++)
            {
                Sphere s1 = Spheres[i];
                Sphere s2 = Spheres[j];

                if (s1.IsCollidingWith(s2))
                {
                    Vector3 s1Normal = (s2.transform.position - s1.transform.position).normalized;
                    Vector3 s2Normal = -s1Normal;

                    float d1 = Vector3.Distance(s1.PrevPos, s2.transform.position) - (s1.radius + s2.radius);
                    float d2 = Vector3.Distance(s1.transform.position, s2.transform.position) - (s1.radius + s2.radius);

                    Vector3 s1Para = ParallellComp(s1.Velocity, s1Normal);
                    Vector3 s1Perp = PerpendicularComp(s1.Velocity, s1Normal);

                    Vector3 s2Para = ParallellComp(s2.Velocity, s2Normal);
                    Vector3 s2Perp = PerpendicularComp(s2.Velocity, s2Normal);

                    float timeOfImpact = Time.deltaTime * (d1 / (d1 - d2));

                    s1.transform.position -= s1.Velocity * (Time.deltaTime - timeOfImpact);
                    s2.transform.position -= s2.Velocity * (Time.deltaTime - timeOfImpact);
                    s1.PrevPos = s1.transform.position;
                    s2.PrevPos = s2.transform.position;

                    Vector3 s1NewPara = (((s1.Mass - s2.Mass) / (s1.Mass + s2.Mass) * s1Para) + (((2 * s2.Mass) / (s1.Mass + s2.Mass)) * s2Para));
                    Vector3 s2NewPara = (((s2.Mass - s1.Mass) / (s1.Mass + s2.Mass) * s2Para) + (((2 * s1.Mass) / (s1.Mass + s2.Mass)) * s1Para));

                    s1.Velocity = s1Perp + CoefficiantOfRestitution * s1NewPara;
                    s2.Velocity = s2Perp + CoefficiantOfRestitution * s2NewPara;

                    s1.transform.position += s1.Velocity * (Time.deltaTime - timeOfImpact);
                    s2.transform.position += s2.Velocity * (Time.deltaTime - timeOfImpact);
                }
            }
        }       
    }

    public Vector3 ParallellComp(Vector3 vec, Vector3 normal)
    {
        return Vector3.Dot(vec, normal) * normal;
    }

    private Vector3 ParraWithMomentum(Vector3 oldPara, Vector3 normal, Sphere s1, Sphere s2)
    {
        return ((s1.Mass - s2.Mass) / (s1.Mass + s2.Mass)) * oldPara +
               ((2 * s2.Mass) / (s1.Mass + s2.Mass)) * ParallellComp(s2.Velocity, normal);
    }

    private Vector3 PerpendicularComp(Vector3 vec, Vector3 normal)
    {
        return vec - ParallellComp(vec, normal);
    }
}
