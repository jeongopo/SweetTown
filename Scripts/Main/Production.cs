using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Production : MonoBehaviour
{

    public GameManager gameManager;
    public AudioManager audioManager;
    public GameObject popup;
    public float time=10; //생산시간
    bool IsFinish=false; //재료가 만들어졌는지
    bool IsRun=false; //가동하고 있는지
    DateTime startTime; //시작시간

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {   
        if(IsFinish==false&&IsRun==false){  //만들어진것도 없고 가동도 안하고 있는 상태
        IsRun=true;
        startTime=DateTime.Now;
        }else {
                if(IsRun==true){
                    Debug.Log("가동중");
                    pop();
                }
                if(IsFinish==true){
                    if(Input.GetMouseButtonDown(0)){
                        productGet();
                }
            }
        }
    }

    public void init(){
        popup.SetActive(false);
    }

    public void productGet(){
        gameManager.heart++;
        audioManager.audioPlay("audioGet");
        popup.SetActive(false);
        IsFinish=false;
        IsRun=false;
        gameManager.userSave();
        startTime=DateTime.Now;
    }

    public void pop(){ //팝업창을 만들기
        TimeSpan gabTime=DateTime.Now-startTime; 
        Debug.Log("시간차이 : "+gabTime.Seconds);
        if(gabTime.Days>=0 && gabTime.Hours>=0 && gabTime.Minutes>=0 && gabTime.Seconds>=time){ //생산 시간이 지났을 때
            Debug.Log("생산완료");
            IsFinish=true;
            popup.SetActive(true);
        }
    }

}

