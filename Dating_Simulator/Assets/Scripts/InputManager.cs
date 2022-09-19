using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private DialogueOptions dialogueOptions;
    // What should happen when you have clicked a dialogue option
    public void ClickedDialogue(GameObject clickedDialogue /* F�r att spara ner och kanske anv�nda till n�got senare */)
    {
        dialogueOptions = clickedDialogue.transform.parent.GetComponent<DialogueOptions>();
        dialogueOptions.ShowNextDialogue();

        // Kanske borde ha denna metod n�gon annan stans men f�r ligga kvar just nu
        dialogueOptions.SaveDownAnswer(clickedDialogue);
    }

    public void ClickedObject(GameObject clickedObject)
    {
        //clickedObject.GetComponent<SCRIPT>().MethodToRun();   Vad som ska h�nda n�r man klickar p� ett object i scenen (t.ex. Spela en animation)
    }
}
