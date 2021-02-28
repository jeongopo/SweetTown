using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGM : MonoBehaviour
{
    public InputField input;
    public SetUserData setuserData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void submitName(){
        if(input.text.Length>=1){
            Debug.Log("[submitName] 값 전달");
            setuserData.setusername=input.text;
            DontDestroyOnLoad(setuserData);
            SceneManager.LoadScene("MainScene");
        }
    }
}
