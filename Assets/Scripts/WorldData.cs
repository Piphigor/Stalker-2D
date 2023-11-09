using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedObject
{
    public string prefabName; //  префаб.name
    public Vector3 position; // pos объекта
}

[System.Serializable]
public class WorldData
{
    public List<SavedObject> savedObjects;
    public List<Vector3> tilemapPositions;
}
