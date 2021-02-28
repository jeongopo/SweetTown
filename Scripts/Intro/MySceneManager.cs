using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using DataInfo;
using System.IO;


public class MySceneManager : MonoBehaviour
{

    private string userdataPath;
    string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        userdataPath=userdataPath=Application.persistentDataPath+"/UserData.dat";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(File.Exists(userdataPath)){
            movetoMainScene();
            }else movetoTutorial();
        }
    }

    public void movetoMainScene(){
        Debug.Log("클릭 확인");
        SceneManager.LoadScene("MainScene");
    }

    void movetoTutorial(){
        SceneManager.LoadScene("Tutorial");
    }


}
