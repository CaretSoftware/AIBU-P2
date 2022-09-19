using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDate : MonoBehaviour
{
    [SerializeField] private GameObject[] girls;
    private int girlToShow = 0;

    public void NextGirl()
    {
        girls[girlToShow].SetActive(false);
        girlToShow++;

        if (girlToShow > girls.Length - 1)
        {
            girlToShow = 0;
        }
        girls[girlToShow].SetActive(true);
    }

    public void PreviousGirl()
    {
        girls[girlToShow].SetActive(false);
        girlToShow--;

        if (girlToShow < 0)
        {
            girlToShow = girls.Length - 1;
        }
        girls[girlToShow].SetActive(true);
    }
}
