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
        //��ư Ŭ�� �� â ������ �ϱ�
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        //��ư Ŭ�� �� â ������ �ϱ�
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
