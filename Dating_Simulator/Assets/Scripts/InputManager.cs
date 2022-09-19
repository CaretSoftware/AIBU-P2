using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private DialogueOptions dialogueOptions;
    // What should happen when you have clicked a dialogue option
    public void ClickedDialogue(GameObject clickedDialogue /* För att spara ner och kanske använda till något senare */)
    {
        dialogueOptions = clickedDialogue.transform.parent.GetComponent<DialogueOptions>();
        dialogueOptions.ShowNextDialogue();

        // Kanske borde ha denna metod någon annan stans men får ligga kvar just nu
        dialogueOptions.SaveDownAnswer(clickedDialogue);
    }

    public void ClickedObject(GameObject clickedObject)
    {
        //clickedObject.GetComponent<SCRIPT>().MethodToRun();   Vad som ska hända när man klickar på ett object i scenen (t.ex. Spela en animation)
    }
}
