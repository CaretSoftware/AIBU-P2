using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Query {
	private static string happy         = "happy";
	private static string flirty        = "flirty";
	private static string embarrassed   = "embarrassed";
	private static string angry         = "angry";
	private static string bored         = "bored";
	private static string interest      = "interest";
	private static string likesJokes    = "likesJokes";
	private static Dictionary<string, int> _query = new Dictionary<string, int>() {
		{ happy, 2 },
		{ flirty, 0 },
		{ embarrassed, 0 },
		{ angry, 0 },
		{ bored, 2 },
		{ interest, 0 },
		{ likesJokes, 1 },
	};
	private Random   _rand = new Random(123);
	// private static string compliment    = "compliment";
	// private static string joke          = "joke";
	// private static string selfInterest = "selfInterest";

	public static List<Dia> NewQuery(GameLoop gl) {
		
		List<Dia> dialogs = new List<Dia>();
		bool      match   = true;

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
		Debug.Log($"write back: {writeBack.Item1}.[{_query[writeBack.Item1]}] CompType.{writeBack.Item3}");
	}
}