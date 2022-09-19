using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueOptions : MonoBehaviour
{
    [SerializeField] private TMP_Text[] dialogueOptions;
    [SerializeField] private TMP_Text questionText;

    private int questionToShow = 1;

    private string[] answersQ1 = { "10", "20", "30", "40" };
    private string[] answersQ2 = { "Yes", "No" };
    private string[] answersQ3 = { "Yes" };



    private void Start()
    {
        //ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {        
/*        switch (questionToShow)
        {
            case 1:
                WriteTextToTheDialogueOptions("How old are you?", answersQ1);
                questionToShow++;
                break;
            case 2:
                WriteTextToTheDialogueOptions("Do you like the food?", answersQ2);
                questionToShow++;
                break;
            case 3:
                WriteTextToTheDialogueOptions("Do you like video games?", answersQ3);
                break;

        }*/
    }

    //Denna metod borde nog flyttas eller tas bort beroende på hur din dialog fungerar Patrik
    public void SaveDownAnswer(GameObject clickedAnswer)
    {

    }

    public void WriteTextToTheDialogueOptions(string question, string[] answersToQuestion)
    {
        UIManager.Instance.StartWriteOutQuestion(question, questionText);
        
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
