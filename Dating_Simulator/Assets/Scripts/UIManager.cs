using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textElement;
    private string textInputed;
    private string textToOutput;
    private int textLength;

    // Start is called before the first frame update
    void Start()
    {      
        textInputed = "This is just a test to see how it looks to write out";
        textLength = textInputed.Length;
        StartCoroutine("WriteDialogue");
    }

    void DisplayDialogue()
    {
        textElement.text = textToOutput;
    }

    IEnumerator WriteDialogue()
    {
        for (int i = 0; i < textLength; i++)
        {
            textToOutput += textInputed[i].ToString();
            DisplayDialogue();
            yield return new WaitForSeconds(0.05f);
        }      
    }
}
