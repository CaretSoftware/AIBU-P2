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

	private int mood = 0;
	private int humour = 0;
	private int interested = 0;
	private int drunkness = 0;

	public int Mood { get { return mood; } set { mood = value; } }
	public int Humour { get { return humour; } set { humour = value; } }
	public int Interested { get { return interested; } set { interested = value; } }
	public int Drunkness { get { return drunkness; } set { drunkness = value; } }

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
		ChangeMood("Angry");
	}

	public void AddInterest(Interest interest)
	{
		for (int i = 0; i < mainInterest.Length; i++)
		{
			if (mainInterest[i] == null)
			{
				mainInterest[i] = interest;
			}
			break;
		}
	}

	private void RandomizeInterests()
	{
		int i = 0;
		if (mainInterest.Length > 0) //Picks one main intrest based on the date
		{
			i++;
			interests[0] = mainInterest[UnityEngine.Random.Range(0, mainInterest.Length)];
		}

		for (; i < interestsCount; i++) //Randomizes the other intrests
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
		interested += changedValue;
		// How intrested/bored the date thinks of you.
	}

	public void ChangeMood(int changedValue)
	{
		mood += changedValue;
		// if its over high enough at the end of the game you get another date...
	}

	public void Drink()
	{
		drunkness++;

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

	public void ChangeMood(string change)
	{
		SetInactive();
		Transform t = transform.Find(change);
		t.gameObject.SetActive(true);
		switch (change)
		{
			case "Happy":
				mood++;
			break;

			case "Angry":
				mood--;
				break;

			case "Embarresed":
				// not sure if it should be a variable
				break;

			case "Bored":
				interested--;
				break;

			case "Flirty":
				interested++;
				break;
		}
	}

	private void SetInactive()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gO = transform.GetChild(i).gameObject;
			if (gO.activeSelf && gO.transform.name != "Body")
			{
				gO.SetActive(false);
				break;
			}
		}
	}
}
