using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interest : MonoBehaviour
{
	public virtual void AddInterest()
	{
		InterestHolder.Instance.AddInterest(this);
		//print(this.name);
	}
}
