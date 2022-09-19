using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interest : MonoBehaviour
{
	public virtual void AddInterest()
	{
		Character.Instance.AddInterest(this);
		//print(this.name);
	}
}
