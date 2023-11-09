using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Save save; // Поле для сохранения ссылки на SaveLoadManager

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Save tmpsave = save.GetComponent<Save>();
            tmpsave.SaveGame(); // Вызываем метод сохранения из SaveLoadManager
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Save tmpsave = save.GetComponent<Save>();
            tmpsave.LoadGame(); // Вызываем метод загрузки из SaveLoadManager
        }
    }
}
