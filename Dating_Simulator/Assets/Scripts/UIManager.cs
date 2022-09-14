using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textElement;
    [SerializeField] private AudioClip dialogueSound;
    private AudioManager audioManager;
    private string textInputed;
    private string textToOutput;
    

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;
        textInputed = "This is just a test to see how it looks to write out";
        StartCoroutine(WriteDialogue());
    }

    void DisplayDialogue()
    {
        textElement.text = textToOutput;
        audioManager.PlaySound(dialogueSound);
    }

    IEnumerator WriteDialogue()
    {
        textElement.text = "";
        foreach (char letter in textInputed.ToCharArray())
        {
            textToOutput += letter;
            DisplayDialogue();
            yield return new WaitForSeconds(0.05f);
        }                        
    }
}
