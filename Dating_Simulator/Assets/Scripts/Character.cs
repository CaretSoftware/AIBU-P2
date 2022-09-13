using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("Character stats")]
	private static Character instance;
	[SerializeField] private Dictionary<string, int> likeness = new Dictionary<string, int>();
	private readonly List<string> subjects = new List<string>();

	private int loveIntrest = 0;

	public static Character Instance
	{
		get 
		{
			// "Lazy loading" to prevent Unity load order error
			if (instance == null)
			{
				instance = FindObjectOfType<Character>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		foreach (string s in subjects)
		{
			likeness.Add(s, 0);
		}
	}

	private bool CheckLikness(string subject, int value)
	{
		if (likeness.TryGetValue(subject, out int likenesV) == true && likenesV >= value)
		{
			return true;
		}
		return false;
	}

	private void ChangeLikeness(string subject, int changedValue)
	{
		likeness[subject] += changedValue;

		if (changedValue > 10)
		{
			likeness[subject] = 10;
		}

		if (changedValue < 0)
		{
			likeness[subject] = 0;
		}
	}

	private void ChangeLoveinterest()
	{
		loveIntrest++;
		// if its over high enough at the end of the game you get another date...
	}

}
