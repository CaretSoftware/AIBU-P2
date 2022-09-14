using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // What should happen when you have clicked a dialogue option
    public void ClickedDialogue(GameObject clickedDialogue)
    {
        clickedDialogue.GetComponent<DialogueOption>().ShowNextDialogue();
    }

    public void ClickedObject(GameObject clickedObject)
    {
        //clickedObject.GetComponent<SCRIPT>().MethodToRun();   Vad som ska hända när man klickar på ett object i scenen (t.ex. Spela en animation)
    }
}
