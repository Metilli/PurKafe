using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    public GameObject deskObject;
    private int totalDeskCount;
    private string filePath;
    private TableModel tableData;
    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/TableData.json";
        GetTableData();
        SetupDesks();
    }

    void GetTableData()
    {
        if (System.IO.File.Exists(filePath))
        {
            string text = System.IO.File.ReadAllText(filePath);
            tableData = JsonUtility.FromJson<TableModel>(text);
            print("file exist");
        }
        else
        {
            tableData = new TableModel();

            for (int i = 0; i < 10; i++)
            {
                Table table = new Table();
                int j = i + 1;
                table.name = "Masa " + j;
                tableData.tables.Add(table);

                string saveText = JsonUtility.ToJson(tableData);
                System.IO.File.WriteAllText(filePath, saveText);
            }
        }
    }

    private void SetupDesks()
    {
        totalDeskCount = PlayerPrefs.GetInt("DeskCount", 10);

        var itemCountPerRow = 7;
        var buttonWidth = GameObject.Find("MidPanel").GetComponent<RectTransform>().rect.width / 7;
        var buttonHeight = GameObject.Find("MidPanel").GetComponent<RectTransform>().rect.height / 4;
        for (int i = 0; i < tableData.tables.Count; i++)
        {
            GameObject desk = Instantiate(deskObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            desk.transform.SetParent(GameObject.Find("MidPanel").transform, false);
            desk.GetComponentInChildren<Text>().text = tableData.tables[i].name;
            RectTransform rect = desk.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            var mod = i % itemCountPerRow;
            var buttonX = (buttonWidth * mod) + buttonWidth / 2;
            var buttonY = (-(i / itemCountPerRow) * buttonHeight) - (buttonHeight / 2);
            rect.anchoredPosition = new Vector2(buttonX, buttonY);
        }
    }
}

[System.Serializable]
public class TableModel
{
    public List<Table> tables = new List<Table>();
}
[System.Serializable]
public class Table
{
    public string name;
}
