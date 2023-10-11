using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{
    private GameData gameData; 
    public static DataPersistence Instance { get; private set; };
    private void Awake() {
    if (Instance == null){
            debug.LogError("������� ����� ������ Data Persistence Manager �� ���� �����");
        }
        Instance = this;
    };
    public void NewGame(){
        this.gameData = new GameData();
    }
    public void LoadGame(){
        //TODO - ������� �������� ���� �� �����. ����� ����������� ����������.
        if (this.gameData == null)
        {
            Debug.Log("������ �� �������. ������������� ������ �� ���������.");
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
