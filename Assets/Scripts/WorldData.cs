using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedObject
{
    public string prefabName; //  ������.name
    public Vector3 position; // pos �������
}

[System.Serializable]
public class WorldData
{
    public List<SavedObject> savedObjects;
    public List<Vector3> tilemapPositions;
}
