using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MySceneManager : MonoBehaviour
{

    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            moveScene();
        }
    }

    public void moveScene(){
        Debug.Log("클릭 확인");
        SceneManager.LoadScene(SceneName);
    }


}
