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
    private bool stopCorutine = false;

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
        questionTextBox.text = textToOutput;        
        audioManager.PlaySound(dialogueSound);
    }

    public void StartWriteOutQuestion(string question)
    {
        stopCorutine = false;
        qustionToWriteOut = question;
        StartCoroutine(WriteDialogue());
    }

    public IEnumerator WriteDialogue()
    {
        textToOutput = "";
        questionTextBox.text = "";
        foreach (char letter in qustionToWriteOut.ToCharArray())
        {
            if (stopCorutine) continue;

            textToOutput += letter;
            DisplayDialogue();
            yield return new WaitForSeconds(0.05f);
        }                        
    }

    public void SkipDialogueWriteOut()
    {
        StopCoroutine(WriteDialogue());
        stopCorutine = true;
        textToOutput = qustionToWriteOut;
        DisplayDialogue();
    }
}
