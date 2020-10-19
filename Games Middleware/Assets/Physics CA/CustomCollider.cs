using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomCollider: MonoBehaviour
{
    public abstract bool IsCollidingWith(Plane s);
    public abstract bool IsCollidingWith(Sphere s);
}
