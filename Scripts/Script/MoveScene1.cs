using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickLoadScene()
    {

        //2.Ÿ�� == "Domino Scene"���� �̵��ϰ� �ʹ�.
        string sceneName = "Intro Scene";
        //1.Ÿ�� ���� �̵��ϰ� �ʹ�.
        SceneManager.LoadSceneAsync(sceneName);
    }
}
