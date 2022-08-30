using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        //버튼 클릭 시 창 열리게 하기
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        //버튼 클릭 시 창 닫히게 하기
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
