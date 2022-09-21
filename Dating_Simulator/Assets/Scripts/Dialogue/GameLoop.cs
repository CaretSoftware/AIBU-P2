using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
using TMPro;

public class GameLoop : MonoBehaviour {
	public delegate void DialogueText(string text, string speaker);
	public delegate void ChoiceText(string[] text);
	public delegate void SetChoiceIndex(int  index);

	public static DialogueText   dialogueText;
	public static ChoiceText choiceText;
	public static SetChoiceIndex setChoiceIndex;

	private                  Dia             _dialogue;
	private static           int             _choiceIndex = -1;
	private readonly         Random          _random      = new Random(123);
	private                  StringBuilder   _sb = new StringBuilder();

	private void Awake() {
		// dialogueText   += DisplayDialogue;
		// choiceText     += DisplayChoices;
		setChoiceIndex =  SetChoice;
	}

	public void StartLoop(){
		Loop();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1))
			_choiceIndex = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2))
			_choiceIndex = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3))
			_choiceIndex = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4))
			_choiceIndex = 3;
	}

	private async void Loop() {
		_dialogue = DialogueContainer.dia[0];

		while (_dialogue != null) {
			_choiceIndex = -1;

			// Display Dialogue
			DisplayDialogue(_dialogue.text, _dialogue.speaker);
			// Retrieve Dialogue Choices
			DisplayChoices(_dialogue.ChoiceText);

			while (_choiceIndex < 0) { //	await until dialog choice comes back with index
				await Task.Yield();
			}

			// write back rules to ruleList from choice
			if (_dialogue.writeBacks.Length == _dialogue.ChoiceText.Length)
				Query.WriteBack(_dialogue.writeBacks[_choiceIndex]);
			else {
				for (int i = 0; i < _dialogue.writeBacks.Length; i++)
					Query.WriteBack(_dialogue.writeBacks[i]);
			}

			_dialogue = GetNextDialogue();
		}

		Debug.Log("Dialogue was null, GAME OVER");
	}

	

	public Dia GetNextDialogue() {
		List<Dia> dialogues = Query.NewQuery(this).OrderByDescending(x => x.rule.Length).ToList();

		if (dialogues.Count > 0)
			return dialogues[0]; //_random.Next(0, dialogues.Count)];

		return null;
	}

	// DEBUG placeholder for call to Delegates
	private void DisplayDialogue(string text, string speaker) {
		dialogueText(text, speaker);
	}

	// DEBUG placeholder for call to Delegates
	private void DisplayChoices(string[] texts) {
		choiceText(texts);
	}

	public static void SetChoice(int index) {
		_choiceIndex = index;
	}
}