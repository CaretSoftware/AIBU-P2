using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestHolder : MonoBehaviour
{
    private static InterestHolder instance;
    public Interest[] interests;

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

	public void SetIntrests()
	{
		interests = GetComponentsInChildren<Interest>();	
	}

	public void Randomize()
	{
		Character.Instance.RandomizeInterests();
	}
}
