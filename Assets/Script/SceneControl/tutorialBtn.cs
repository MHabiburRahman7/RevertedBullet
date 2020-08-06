using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialBtn : MonoBehaviour
{
    public GameObject tutorialImg;
    private bool isShowing = true;
    // Start is called before the first frame update
    void Start()
    {
        tutorialImg.gameObject.SetActive(false);
        isShowing = false;
    }


    public void ShowTutorial()
    {
        if (isShowing)
        {
            tutorialImg.gameObject.SetActive(false);
            isShowing = false;
        }
        else
        {
            tutorialImg.gameObject.SetActive(true);
            isShowing = true;
        }
    }

}
