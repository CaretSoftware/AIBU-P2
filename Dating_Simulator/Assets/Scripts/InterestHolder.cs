using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterestHolder : MonoBehaviour
{
    private static InterestHolder instance;
	[SerializeField] private int peopleCount = 4;
	[SerializeField] private int interestsCount = 3;

	public Interest[] personIntrests;
	public Interest[][] interests;
	[SerializeField] private List<Interest> mainInterest = new List<Interest>();
	private Text interestText;
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
		personIntrests = GetComponentsInChildren<Interest>(true);
		for (int i = 0; i < peopleCount; i++)
		{
			interests[i] = new Interest[interestsCount];
		}
	}

	public void SetIntrests()
	{
		RandomizeInterests();
	}
	private void RandomizeInterests()
	{
		interestText = GameObject.Find("InterestText").GetComponent<Text>();
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
				int random = Random.Range(0, personIntrests.Length);
				Interest interest = personIntrests[random];
				while (FindInterest(i, interest) != -1)
				{
					random = Random.Range(0, personIntrests.Length);
					interest = personIntrests[random];
				}
				interests[i][j] = interest;
			}
			Humour = Random.Range(-1, 2);
		}
	}

	public void SwitchPerson()
	{
		GiveIntrest();
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
		for (int i = 0; i < interestsCount; i++)
		{
			if (interests[person][i] == interest) 
			{
				return i;
			}
		}
		return -1;
	}

	private void GiveIntrest()
	{
		for (int i = 0; i < peopleCount; i++)
		{
			GameObject gO = GameObject.Find("Person" + (i + 1));
			if (gO != null && gO.gameObject.activeSelf)
			{
				Character.Instance.TakeIntrests(i);
				interestText.text = "";
				for (int j = 0; j < interestsCount; j++)
				{
					interestText.text += interests[i][j].GetType().Name + " "; 
				}
				break;
			}
		}
	}
}
