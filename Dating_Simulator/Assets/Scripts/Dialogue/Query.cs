using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Query {
	public static   PlayerDrinking playerDrinking;
	public delegate void           FaceSetter(string face);
	public static   FaceSetter     faceSetter;
	public delegate void           SetChoiceIndex(int index);
	private static  string         happy       = "happy";
	private static  string         flirty      = "flirty";
	private static  string         embarrassed = "embarrassed";
	private static  string         angry       = "angry";
	private static  string         bored       = "bored";
	private static  string         interest    = "interest";
	private static  string         likesJokes  = "likesJokes";
	private static readonly string[] moods = {
		DialogueContainer.neutral,
		DialogueContainer.flirty,
		DialogueContainer.angry,
		DialogueContainer.bored,
		DialogueContainer.happy,
		DialogueContainer.embarrassed,
	};

	private static Dictionary<string, int> _query = new Dictionary<string, int>() {
		{ happy, 0 },
		{ flirty, 0 },
		{ embarrassed, 0 },
		{ angry, 0 },
		{ bored, 0 },
		{ interest, 0 },
		{ likesJokes, 1 },
	};

	public static List<Dia> NewQuery(GameLoop gl) {

		List<Dia> dialogs = new List<Dia>();
		bool      match   = true;
		int       winning = 0;
		for (int i = 0; i < DialogueContainer.dia.Length; i++) {
			(string key, Criterion crit)[] criteria = DialogueContainer.dia[i].rule.Criteria;
			for (int crit = 0; crit < criteria.Length; crit++) {
				match = true;
				string key = criteria[crit].key;
				if (!_query.ContainsKey(key) ||
					!criteria[crit].crit.Compare(_query[key])) {
					match = false;
					break;
				}
			}

			if (match)
				dialogs.Add(DialogueContainer.dia[i]);

		}

		// string debug = "";
		// for (int i = 0; i < dialogs.Count; i++) {
		// 	debug += "{";
		// 	debug += dialogs[i].text;
		// 	debug += "}\n";
		// }
		// Debug.Log($"query result: {dialogs.Count}\n{debug}");
		// if (_query.ContainsKey("highestMood"))
			// Debug.Log($"{moods[_query["highestMood"]]} index:{_query["highestMood"]}");
		return dialogs;
	}

	public enum CompType {
		Bool,
		Increment,
		Set,
	}

	public static void WriteBack((string, int, CompType) writeBack) {
		if (!_query.ContainsKey(writeBack.Item1))
			_query.Add(writeBack.Item1, writeBack.Item2);
		else
			switch (writeBack.Item3) {
				case CompType.Bool:
					_query[writeBack.Item1] = Math.Min(1, Math.Max(0, writeBack.Item2));
					break;
				case CompType.Increment:
					_query[writeBack.Item1] += writeBack.Item2;
					break;
				case CompType.Set:
					_query[writeBack.Item1] = writeBack.Item2;
					break;
			}


		if (writeBack.Item1.Equals(DialogueContainer.cost)) {
			DialogueContainer.totalCost    += writeBack.Item2;
			DialogueContainer.dia[80].text =  $"The total bill is at {DialogueContainer.totalCost}";
			DialogueContainer.dia[86].text =  $"The total bill is at {DialogueContainer.totalCost}";
			// Debug.Log(DialogueContainer.dia[80].text);
		}

		if (writeBack.Item1.Equals(DialogueContainer.neutral)    ||
			writeBack.Item1.Equals(DialogueContainer.flirty)     ||
			writeBack.Item1.Equals(DialogueContainer.angry)      ||
			writeBack.Item1.Equals(DialogueContainer.bored)      ||
			writeBack.Item1.Equals(DialogueContainer.happy) ||
			writeBack.Item1.Equals(DialogueContainer.embarrassed)) {

			SetHighestMood();
		}

		if (writeBack.Item1.Equals(DialogueContainer.drinkWine) && playerDrinking != null) {
			// Debug.Log("DRINKING!!!!!!!!!!");
			playerDrinking.Drink();
		}
		
		// Debug.Log($"write back: {writeBack.Item1}.[{_query[writeBack.Item1]}] CompType.{writeBack.Item3}");
	}

	private static void SetHighestMood() {
		int highestMoodIndex = -1;
		int highestMood      = -1;
		for (int i = 0; i < moods.Length; i++)
			if (_query.ContainsKey(moods[i]) &&
				_query[moods[i]] > highestMood) {
				highestMood      = _query[moods[i]];
				highestMoodIndex = i;
			}

		if (_query.ContainsKey("highestMood"))
			_query["highestMood"] = highestMoodIndex;
		else
			_query.Add("highestMood", highestMoodIndex);
		
		faceSetter?.Invoke(moods[highestMoodIndex]);
		
		//Debug.Log($"HIGHEST MOOD: {moods[highestMoodIndex]} {highestMoodIndex}");
	}

	public static void SetCommonInterest(string commonInterest) {
		DialogueContainer._commonInterest = commonInterest;
	}
	
	public static void SetRandomInterest(string randomInterest) {
		DialogueContainer._commonInterest = randomInterest;
	}
}