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
        TextAsset displayText = Resources.Load<TextAsset>("draftItemDialogue");

        string[] data = displayText.text.Split('\n');
        for (int i = 2; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');

            Dialogue d = new Dialogue();

            if (row.Length > 1 && row[1] != "") {
                d.name = row[0];
                d.dialogue = new List<string>();
                for (int j = 1; j < row.Length; j++) {
                  var text = row[j];
                  if (text.Length > 0) {
                    d.dialogue.Add(text);
                  }
                }
                if (d.dialogue.Count > 0) {
                  dialogue.Add(d.name, d);
                }
            }
        }
    }

    public string GetKey (string key) {
      var entries = dialogue[key].dialogue;
      return entries[
        Random.Range(0, entries.Count)
      ];
    }

}
