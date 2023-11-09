using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Save: MonoBehaviour
{
    public GameObject Grid; 
    public List<GameObject> prefabObjects;
    public List<Tilemap> tilemaps;

    void Start()
    {
        Tilemap[] foundTilemaps = Grid.GetComponentsInChildren<Tilemap>();
        tilemaps.AddRange(foundTilemaps);
        GameObject[] objectsWithTag1 = GameObject.FindGameObjectsWithTag("Tree");
        prefabObjects.AddRange(objectsWithTag1);
        GameObject[] objectsWithTag2 = GameObject.FindGameObjectsWithTag("Boar");
        prefabObjects.AddRange(objectsWithTag2);
    }
    public void SaveGame()
    {
        WorldData worldData = new WorldData();
        worldData.savedObjects = new List<SavedObject>();
        worldData.tilemapPositions = new List<Vector3>();
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }
        foreach (GameObject prefab in prefabObjects)
        {
            SavedObject savedObject = new SavedObject();
            savedObject.prefabName = prefab.name; // Имя префаба
            savedObject.position = prefab.transform.position; // Позиция объекта
            worldData.savedObjects.Add(savedObject);
        }
        foreach (Tilemap tilemap in tilemaps)
        {   
            worldData.tilemapPositions.Add((tilemap.transform.position));
        }
        string jsonData = JsonUtility.ToJson(worldData);
        File.WriteAllText(Application.persistentDataPath + "/worldData.json", jsonData);

        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/worldData.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            WorldData worldData = JsonUtility.FromJson<WorldData>(jsonData);
            foreach (SavedObject savedObject in worldData.savedObjects)
            {
                GameObject prefab = Resources.Load<GameObject>(savedObject.prefabName);
                if (prefab != null)
                {
                    GameObject newObject = Instantiate(prefab, savedObject.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Prefab not found: " + savedObject.prefabName);
                }
            }
            Debug.Log("Game Loaded.");
        }
        else
        {
            Debug.Log("No saved game found...");
        }
    }
}
