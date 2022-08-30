using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingBtn : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    float currTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime;

                if (myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeIn)
        {
            if (myUIGroup.alpha >= 0)
            {
                myUIGroup.alpha += Time.deltaTime;

                if (myUIGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
        currTime += Time.deltaTime;

        if (currTime > 10)
        {
            ShowUI();
        }
    }
    public void ShowUI()
    {
        fadeIn = true;
    }
    public void HideUI()
    {
        fadeOut = true;
    }
    public void GameQuit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
