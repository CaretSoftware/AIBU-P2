using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("Character stats")]
	private static Character instance;
   [SerializeField] private Interest[] interests;
	private InterestHolder interestHolder;

	private int mood = 0;
	private int interested = 0;
	private int drunkness = 0;

	public int Mood { get { return mood; } set { mood = value; } }
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
		interestHolder = InterestHolder.Instance; 
	}

	private void Start()
	{
		/*
		RandomizeInterests();
		ChangeMood("Angry");
		*/
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

			case "Embarrassed":
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

	public void TakeIntrests(int person)
	{
		interests = interestHolder.interests[person];
		print(person);
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
