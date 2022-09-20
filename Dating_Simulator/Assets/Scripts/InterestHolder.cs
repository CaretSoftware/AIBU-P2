using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestHolder : MonoBehaviour
{
    private static InterestHolder instance;
	[SerializeField] private readonly int peopleCount = 4;
	[SerializeField] private int interestsCount = 3;

	public Interest[] personIntrests;
	public Interest[][] interests;
	[SerializeField] private List<Interest> mainInterest;
	private int humour = 0;
	public int Humour { get { return humour; } set { humour = value; } }

	public static InterestHolder Instance
	{
		get
		{
			// "Lazy loading" to prevent Unity load order error
			if (instance == null)
			{
				instance = FindObjectOfType<InterestHolder>();
			}
			return instance;
		}
	}

	public void Awake()
	{
		interests = new Interest[peopleCount][];
		for (int i = 0; i < peopleCount; i++)
		{
			interests[i] = new Interest[interestsCount];
		}
	}

	public void SetIntrests()
	{
		personIntrests = GetComponentsInChildren<Interest>();
		RandomizeInterests();
	}
	private void RandomizeInterests()
	{
		for (int i = 0; i < peopleCount; i++)
		{
			int j = 0;
			if (mainInterest.Count > 0) //Picks one main intrest based on the date
			{
				j++;
				interests[i][0] = mainInterest[Random.Range(0, mainInterest.Count)];
			}

			for (; j < interestsCount; j++) //Randomizes the other intrests
			{
				Interest interest = interests[i][Random.Range(0, interestsCount)];
				while (FindInterest(i, interest) == -1)
				{
					interest = interests[i][Random.Range(0, interestsCount)];
				}
				interests[i][j] = interest;
			}
			Humour = UnityEngine.Random.Range(-1, 2);
		}
	}

	public void AddInterest(Interest interest)
	{
		for (int i = 0; i < mainInterest.Count; i++)
		{
			if (mainInterest[i] == interest)
			{
				return;
			}
		}
		mainInterest.Add(interest);
	}

	private int FindInterest(int person, Interest interest)
	{
		for (int i = 0; i < interests.Length; i++)
		{
			if (interests[person][i] == interest)
			{
				return i;
			}
		}
		return -1;
	}

	public void GiveIntrest()
	{
		for (int i = 0; i < peopleCount; i++)
		{
			if (transform.Find("Person" + (i + 1)))
			{
				Character.Instance.TakeIntrests(i);
			}
		}
	}
}
