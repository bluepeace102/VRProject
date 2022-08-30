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

        //2.타깃 == "Domino Scene"으로 이동하고 싶다.
        string sceneName = "Intro Scene";
        //1.타깃 씬을 이동하고 싶다.
        SceneManager.LoadSceneAsync(sceneName);
    }
}
