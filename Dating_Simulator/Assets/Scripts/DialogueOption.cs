using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueOption : MonoBehaviour
{
    [SerializeField] private GameObject nextDialogue;

    public void ShowNextDialogue()
    {        
        nextDialogue.SetActive(true);
    }
}
