using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    //В классе нужно описать переменные, которые будем сохранять. 
    void OnGUI(){
        if (GUI.Button(new Rect(750, 0, 125, 50), "Save Your Game")) // Кнопка для сохранения
            SaveGame();
        if (GUI.Button(new Rect(750, 100, 125, 50), "Load Your Game")) // Кнопка для загрузки
            LoadGame();
        if (GUI.Button(new Rect(750, 200, 125, 50), "Reset Save Data")) // Кнопка для ресета
            ResetData();
    }
    void SaveGame(){
        PlayerPrefs.SetInt("SavedInteger", intToSave);
        PlayerPrefs.SetFloat("SavedFloat", floatToSave);
        PlayerPrefs.SetString("SavedString", stringToSave);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
    void LoadGame(){
        if (PlayerPrefs.HasKey("SavedInteger"))
        {
            intToSave = PlayerPrefs.GetInt("SavedInteger");
            floatToSave = PlayerPrefs.GetFloat("SavedFloat");
            stringToSave = PlayerPrefs.GetString("SavedString");
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    void ResetData()
    {
        PlayerPrefs.DeleteAll();
        intToSave = 0;
        floatToSave = 0.0f;
        stringToSave = "";
        Debug.Log("Data reset complete");
    }
    
}
