using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private AudioClip dialogueSound;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;      
    }

    void DisplayDialogue(TMP_Text questionTextBox)
    {
        questionTextBox.text = textToOutput;        
        audioManager.PlaySound(dialogueSound);
    }

    public void StartWriteOutQuestion(string question, TMP_Text questionTextBox)
    {
        StartCoroutine(WriteDialogue(question, questionTextBox));
    }

    public IEnumerator WriteDialogue(string question, TMP_Text questionTextBox)
    {
        textToOutput = "";
        questionTextBox.text = "";
        foreach (char letter in question.ToCharArray())
        {
            textToOutput += letter;
            DisplayDialogue(questionTextBox);
            yield return new WaitForSeconds(0.05f);
        }                        
    }

}
