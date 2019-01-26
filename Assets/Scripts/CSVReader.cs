using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{
    List<Dialogue> dialogue = new List<Dialogue>();

    void Start()
    {
        TextAsset displayText = Resources.Load<TextAsset>("testCSV");

        string[] data = displayText.text.Split('\n');
        //Debug.Log(data.Length);
        for (int i = 2; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(',');

            Dialogue q = new Dialogue();
            int.TryParse(row[0], out q.id);

            if (row[1] != "")
            {
                q.name = row[1];
                q.description = row[2];
            }
            dialogue.Add(q);
            string cat = row[2].ToString();
            Debug.Log(cat);
        }
    }
}
