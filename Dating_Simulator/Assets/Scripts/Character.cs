using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Header("Character stats")]
	private static Character instance;
   [SerializeField] private  Interest[]     interests;
	private                  InterestHolder interestHolder;
	[SerializeField] private Animator       animator;
	[SerializeField] private AnimationClip  drinkAnimation;

	[SerializeField] private GameObject[] neutralFaces;
	[SerializeField] private GameObject[] flirtyFaces;
	[SerializeField] private GameObject[] angryFaces;
	[SerializeField] private GameObject[] boredFaces;
	[SerializeField] private GameObject[] happyFaces;
	[SerializeField] private GameObject[] embarrassedFaces;

	private                  int          mood       = 0;
	private                  int          interested = 0;

	public int Mood { get { return mood; } set { mood = value; } }
	public int Interested { get { return interested; } set { interested = value; } }

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
		Query.faceSetter += ChangeMood;
		
		interestHolder   = InterestHolder.Instance; 
	}

	public void ChangeInterest(int changedValue)
	{
		interested += changedValue;
		// How intrested/bored the date thinks of you.
	}

	public void ChangeMood(int changedValue)
	{
		mood += changedValue;
		// if its over high enough at the end of the game you get another date...
	}

	public void ChangeMood(string change)
	{
		//SetInactive();
		// Transform t = transform.Find(change);
		// t.gameObject.SetActive(true);
		Debug.Log($"Change {change.Equals("bored")} {change}");
		
		// if (change.Equals("neutral") ||
		// 	change.Equals("flirty")  ||
		// 	change.Equals("bored")   ||
		// 	change.Equals("angry")   ||
		// 	change.Equals("happy") ||
		// 	change.Equals("embarrassed")) {
		// 	
		if( change.Equals(DialogueContainer.neutral) ||
			change.Equals(DialogueContainer.flirty) ||
			change.Equals(DialogueContainer.angry) ||
			change.Equals(DialogueContainer.bored) ||
			change.Equals(DialogueContainer.happy) ||
			change.Equals(DialogueContainer.embarrassed)) {
			
			for (int i = 0; i < neutralFaces.Length; i++)
				neutralFaces[i].SetActive(change.Equals(DialogueContainer.neutral));
			for (int i = 0; i < flirtyFaces.Length; i++)
				flirtyFaces[i].SetActive(change.Equals(DialogueContainer.flirty));
			for (int i = 0; i < boredFaces.Length; i++)
				boredFaces[i].SetActive(change.Equals(DialogueContainer.bored));
			for (int i = 0; i < angryFaces.Length; i++)
				angryFaces[i].SetActive(change.Equals(DialogueContainer.angry));
			for (int i = 0; i < embarrassedFaces.Length; i++)
				embarrassedFaces[i].SetActive(change.Equals(DialogueContainer.embarrassed));
			for (int i = 0; i < happyFaces.Length; i++)
				happyFaces[i].SetActive(change.Equals(DialogueContainer.happy));
		}
		
		switch (change)
		{
			case "happy":
				mood++;
			break;

			case "angry":
				mood--;
				break;

			case "bored":
				interested--;
				break;

			case "flirty":
				interested++;
				if (animator != null)
					animator.Play(drinkAnimation.ToString());
				break;
		}
	}

	public void TakeIntrests(int person)
	{
		interests = interestHolder.interests[person];
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
