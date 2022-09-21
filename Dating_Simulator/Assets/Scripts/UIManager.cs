using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioClip dialogueSound;
    [SerializeField] private TMP_Text questionTextBox;
    private AudioManager audioManager;
    private string textToOutput;
    private string qustionToWriteOut;
    private string speakerName;

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioManager = AudioManager.Instance;
    }

    void DisplayDialogue()
    {
        questionTextBox.text = speakerName + ": " + textToOutput;        
        audioManager.PlaySound(dialogueSound);
    }

    public void StartWriteOutQuestion(string question, string speaker)
    {
        qustionToWriteOut = question;
        speakerName = speaker;
        StopAllCoroutines();
        StartCoroutine(WriteDialogue());
    }

    private IEnumerator WriteDialogue()
    {
        textToOutput = "";
        questionTextBox.text = "";

        foreach (char letter in qustionToWriteOut.ToCharArray())
        {
            textToOutput += letter;
            DisplayDialogue();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void SkipDialogueWriteOut()
    {
        StopAllCoroutines();
        textToOutput = qustionToWriteOut;
        DisplayDialogue();
    }
}
