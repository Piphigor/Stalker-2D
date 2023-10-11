using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    private GameData gameData; 
    public static DataPersistence Instance { get; private set; };
    private void Awake() {
    if (Instance == null){
            debug.LogError("Найдено более одного Data Persistence Manager на этой сцене");
        }
        Instance = this;
    };
    public void NewGame(){
        this.gameData = new GameData();
    }
    public void LoadGame(){
        //TODO - функция загрузки игры из файла. Может пригодиться дескриптор.
        if (this.gameData == null)
        {
            Debug.Log("Данные не найдены. Инициализация данных по умолчанию.");
            NewGame();
        }
        //TODO
    }
    public void SaveGame(){
        //TODO 

        //TODO
    }
    public void OnApplicationQuit(){
        SaveGame();
    }
    private void OnApplicationStart(){
        LoadGame();
    }
}
