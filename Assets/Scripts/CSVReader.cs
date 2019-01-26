using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{
     List<Dialogue> dialogue = new List<Dialogue>();
    // Use this for initialization
    void Start()
    {
        TextAsset testCSV = Resources.Load<TextAsset>("testCSV");

        string[] data = testCSV.text.Split('\n');
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
        }

        foreach (Dialogue q in dialogue)
        {
            Debug.Log(q.name + "," + q.description);
        }
    }
}
