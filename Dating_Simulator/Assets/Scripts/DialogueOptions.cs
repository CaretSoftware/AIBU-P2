using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueOptions : MonoBehaviour
{
    [SerializeField] private TMP_Text[] dialogueOptions;

    private void Awake()
    {
        GameLoop.choiceText = WriteOutDialogueOptions;
        GameLoop.dialogueText = WriteOutQuestion;       
    }
    
    public void WriteOutQuestion(string question)
    {
        UIManager.Instance.StartWriteOutQuestion(question);
    }

    public void WriteOutDialogueOptions(string[] answersToQuestion)
    {              
        foreach (TMP_Text option in dialogueOptions)
        {
            option.text = "";
        }

        int i = 0;
        foreach (string answer in answersToQuestion)
        {
            dialogueOptions[i].text = answer;
            i++;
        }

        WhichDialogueTextBoxToShow();
    }

    private void WhichDialogueTextBoxToShow()
    {
        for (int i = 0; i < dialogueOptions.Length; i++)
        {
            GameObject dialogueTextBox = dialogueOptions[i].transform.parent.gameObject;
            if (dialogueOptions[i].text == "")
            {
                dialogueTextBox.SetActive(false);
            }
            else
            {
                dialogueTextBox.SetActive(true);
            }
        }
    }
}
