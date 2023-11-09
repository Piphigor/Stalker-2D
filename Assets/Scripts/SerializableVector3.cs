using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializableVector3 
{
    public float x; public float y; public float z;
    public SerializableVector3(Vector3 vector)
    { this.x = vector.x; this.y=vector.y; this.z = vector.z;}
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
