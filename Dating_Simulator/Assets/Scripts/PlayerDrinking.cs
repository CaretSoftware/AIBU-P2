using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrinking : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip drinkAnimation;


    public void Drink()
    {
        animator.Play(drinkAnimation.ToString());
    }
}
