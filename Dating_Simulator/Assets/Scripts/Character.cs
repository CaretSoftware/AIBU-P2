using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("Character stats")]
	private static Character instance;
	[SerializeField] private int interestsCount;
	[SerializeField] private Interest[] mainInterest;
   [SerializeField] private Interest[] interests;
	private InterestHolder interestHolder;

	public int Mood { get { return Mood; } set => Mood = value; }
	public int Humour { get { return Humour; } set => Humour = value; }
	public int Interested { get { return Interested; } set => Interested = value; }
	public int Drunkness { get { return Drunkness; } set => Drunkness = value; }

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
		interests = new Interest[interestsCount];
		interestHolder = InterestHolder.Instance;
	}

	private void Start()
	{
		RandomizeInterests();
	}

	private void RandomizeInterests()
	{
		int i = 0;
		if (mainInterest.Length > 0)
		{
			i++;
			interests[0] = mainInterest[UnityEngine.Random.Range(0, mainInterest.Length)];
		}

		for (; i < interestsCount; i++)
		{
			Interest interest = interestHolder.interests[UnityEngine.Random.Range(0, InterestHolder.Instance.interests.Length)];
			while (FindInterest(interest) != -1)
			{
				interest = interestHolder.interests[UnityEngine.Random.Range(0, InterestHolder.Instance.interests.Length)];
			}
			interests[i] = interest;
		}

		Humour = UnityEngine.Random.Range(-1, 2);
	}


	public void ChangeLoveinterest(int changedValue)
	{
		Interested += changedValue;
		// How intrested/bored the date thinks of you.
	}

	public void ChangeMood(int changedValue)
	{
		Mood += changedValue;
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

	private int FindInterest(Interest interest)
	{
		for (int i = 0; i < interests.Length; i++)
		{
			if (interests[i] == interest)
			{
				return i;
			}
		}
		return -1;
	}

}
