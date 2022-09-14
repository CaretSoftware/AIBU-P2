using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("Character stats")]
	private static Character instance;
	[SerializeField] private List<string> subjects = new List<string>();

	[SerializeField] private Interest[] interests;
	private int LoveInterest { get { return LoveInterest; } set => LoveInterest = value; }
	private int Drunkness { get { return Drunkness; } set => Drunkness = value; }

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
		interests = new Interest[subjects.Count];
		for (int i = 0; i < subjects.Count; i++)
		{
			Interest interest = new Interest
			{
				subject = subjects[i],
				value = 0
			};
			interests[i] = interest;
		}
	}

	public bool CheckLikness(string subject, int value)
	{
		if (interests[FindInterest(subject)].value >= value)
		{
			return true;
		}
		return false;
	}

	public void ChangeLikeness(string subject, int changedValue)
	{
		int interestIndex = FindInterest(subject);
		interests[interestIndex].value += changedValue;

		if (changedValue > 10)
		{
			interests[interestIndex].value = 10;
		}

		if (changedValue < 0)
		{
			interests[interestIndex].value = 0;
		}
	}

	public void ChangeLoveinterest(char calculation)
	{
		if (calculation.Equals('+') || calculation.Equals('-'))
		{
			switch (calculation)
			{
				case '+':
					LoveInterest++;
					break;

				case '-':
					LoveInterest--;
					break;
			}
		}
		// if its over high enough at the end of the game you get another date...
	}
	public void Drink()
	{
		Drunkness++;

		if (Drunkness >= 8)
		{
			//Falling over, saying something weird or something else?
		}
	}

	private int FindInterest(string subject)
	{
		for (int i = 0; i < interests.Length; i++)
		{
			Interest interest = interests[i];
			if (interest.subject == subject)
			{
				return i;
			}
		}
		return -1;
	}

}

[Serializable] public struct Interest
{
	public string subject;
	public int value;
}
