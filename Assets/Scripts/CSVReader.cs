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

    List<Dialogue> dialogue = new List<Dialogue>();

    void Start()
    {
        TextAsset displayText = Resources.Load<TextAsset>("itemdialogue");

        string[] data = displayText.text.Split('\n');
        //Debug.Log(data.Length);
        for (int i = 2; i < data.Length; i++)
        {
            string[] row = data[i].Split(',');

            Dialogue d = new Dialogue();
            int.TryParse(row[0], out d.id);

            if (row[1] != "")
            {
                d.name = row[1];
                d.dialogue = row[2];
            }
            dialogue.Add(d);
            fullText = d.dialogue;
            //Debug.Log(data[2][2]);            
        }

        //foreach (Dialogue d in dialogue)
        //{
        //    currentText = data[2][2].ToString();
        //}

        //StartCoroutine("ShowText");
    }

    void Update()
    {

    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
