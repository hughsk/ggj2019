using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{
    public int ID;

    public float delay = 0.1f;
    private string currentText;
    public string fullText;

    Dictionary<string, Dialogue> dialogue = new Dictionary<string, Dialogue>();

    void OnEnable()
    {
        TextAsset displayText = Resources.Load<TextAsset>("itemdialogue");

        string[] data = displayText.text.Split('\n');
        for (int i = 2; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');

            Dialogue d = new Dialogue();

            if (row.Length > 1 && row[1] != "") {
                int.TryParse(row[0], out d.id);
                d.name = row[1];
                d.dialogue = row[2];
                dialogue.Add(d.name, d);
            }
        }
    }

    public string GetKey (string key) {
      return dialogue[key].dialogue;
    }

}
