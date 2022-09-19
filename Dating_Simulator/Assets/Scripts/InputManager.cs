using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameLoop.SetChoiceIndex setChoiceIndex;

    private void Awake()
    {
        setChoiceIndex = GameLoop.SetChoice;
    }
    // What should happen when you have clicked a dialogue option
    public void ClickedDialogue(int buttonPressed)
    {
        setChoiceIndex(buttonPressed);
    }

    public void ClickedObject(GameObject clickedObject)
    {
        //clickedObject.GetComponent<SCRIPT>().MethodToRun();   Vad som ska h�nda n�r man klickar p� ett object i scenen (t.ex. Spela en animation)
    }
}
