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

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    private void Awake()
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
        //audioManager.PlaySound(dialogueSound);
    }

    public void StartWriteOutQuestion(string question)
    {
        StartCoroutine(WriteDialogue(question));
    }

    public IEnumerator WriteDialogue(string question)
    {
        textToOutput = "";
        questionTextBox.text = "";
        foreach (char letter in question.ToCharArray())
        {
            textToOutput += letter;
            DisplayDialogue();
            yield return new WaitForSeconds(0.05f);
        }                        
    }

}
