using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Save save; // ���� ��� ���������� ������ �� SaveLoadManager

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Save tmpsave = save.GetComponent<Save>();
            tmpsave.SaveGame(); // �������� ����� ���������� �� SaveLoadManager
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Save tmpsave = save.GetComponent<Save>();
            tmpsave.LoadGame(); // �������� ����� �������� �� SaveLoadManager
        }
    }
}
