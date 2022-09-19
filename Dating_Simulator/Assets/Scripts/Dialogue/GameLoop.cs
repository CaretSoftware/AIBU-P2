using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;
using TMPro;

public class GameLoop : MonoBehaviour {
	public delegate void DialogueText(string text, string[] choises);
	public delegate void ChoiceText(string[] text);
	public delegate void SetChoiceIndex(int  index);

	public DialogueText   dialogueText;
	public ChoiceText     choiceText;
	public SetChoiceIndex setChoiceIndex;

	private                  Dia             _dialogue;
	private static           int             _choiceIndex = -1;
	private readonly         Random          _random      = new Random(123);
	[SerializeField] private TextMeshProUGUI dialogue;
	[SerializeField] private TextMeshProUGUI choice;
	[SerializeField] private DialogueOptions dialogueOptions;
	private                  StringBuilder   _sb = new StringBuilder();

	private void Awake() {
		dialogueText   += DisplayDialogue;
		choiceText     += DisplayChoices;
		setChoiceIndex =  SetChoice;
	}

	private void Start() {
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
			DisplayDialogue(_dialogue.text, _dialogue.ChoiceText);
			// Retrieve Dialogue Choices
			//DisplayChoices(_dialogue.ChoiceText);

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

	

	private Dia GetNextDialogue() {
		List<Dia> dialogues = Query.NewQuery(this).OrderByDescending(x => x.rule.Length).ToList();

		if (dialogues.Count > 0)
			return dialogues[0]; //_random.Next(0, dialogues.Count)];

		return null;
	}

	// DEBUG placeholder for call to Delegates
	private void DisplayDialogue(string text, string[] choises) {
		if (dialogue == null) return;
		dialogueOptions.WriteTextToTheDialogueOptions(text, choises);
		//dialogue.text = text;
	}

	// DEBUG placeholder for call to Delegates
	private void DisplayChoices(string[] texts) {
		if (choice == null) return;
		
		_sb.Clear();
		for (int i = 0; i < texts.Length; i++) {
			_sb.Append(texts[i]);
			if (i != texts.Length - 1)
				_sb.Append("\n");
		}
		
		choice.text = _sb.ToString();
	}

	private static void SetChoice(int index) {
		_choiceIndex = index;
	}
}