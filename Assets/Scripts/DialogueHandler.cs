using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public float delay = 0.1f;
    private string currentText = "";
    public string fullText;
    //private CSVReader[] displayText;


    private void Start()
    {
        SetText();
    }
    void SetText()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for(int i=0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
