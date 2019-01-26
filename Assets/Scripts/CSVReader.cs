using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSVReader : MonoBehaviour
{

    public float delay = 0.1f;
    private string currentText;
    public string fullText;

    List<Dialogue> dialogue = new List<Dialogue>();

    void Start()
    {
        TextAsset displayText = Resources.Load<TextAsset>("testCSV");

        string[] data = displayText.text.Split('\n');
        //Debug.Log(data.Length);
        for (int i = 2; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(',');

            Dialogue d = new Dialogue();
            int.TryParse(row[0], out d.id);

            if (row[1] != "")
            {
                d.name = row[1];
                d.description = row[2];
            }
            dialogue.Add(d);
            fullText = d.description;
            Debug.Log(d.description);            
        }

        StartCoroutine("ShowText");

        foreach (Dialogue d in dialogue)
        {
            currentText = d.description;
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i); Debug.Log("1");
            this.GetComponent<Text>().text = currentText; Debug.Log("2");
            yield return new WaitForSeconds(delay); Debug.Log("3");
        }
    }
}
