using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System;

public class Decoration : MonoBehaviour
{
    
    public DecoData decoData;
    public GameObject moveBtns;
    public GameManager gameManager;
    public AudioManager audioManager;

    public bool IsMoving;
    Vector3 newposition;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if((gameManager.IsMove==true)&&gameManager.IsNew==decoData.deconum){ //새로 구매한 구조물인 경우
            Debug.Log("["+name+"] 구매된 건물");
            moveBtns.SetActive(true);
            IsMoving=true;
            if(Input.GetMouseButtonDown(0)){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if(IsMoving==true){
                        if(hit.collider != null){
                            //Debug.Log("["+name+"] Move collider : "+hit.collider.name+", decotype : "+decoData.decotype);
                            if(hit.collider.name=="MoveBtn"){
                                movePosition();
                                gameManager.IsNew=0;
                                gameManager.IsOneMove=false;
                                gameManager.MoveEnd();
                            }else{
                            Debug.Log("["+name +" Shopping] 마우스 클릭 인식 ("+hit.point.x+", "+hit.point.z+")");
                            newposition=new Vector3((int)hit.point.x,0,(int)hit.point.z); //정수 형태의 숫자로 저장
                            this.transform.position=newposition;
                            }
                        }                           
                    }
            }
        }else if(gameManager.IsMove==true){ //배치 상태일 때
            if(Input.GetMouseButtonDown(0)){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);

                    if(IsMoving==true){
                        if (hit.collider != null){
                            //Debug.Log("["+name+"] Move collider : "+hit.collider.name+", decotype : "+decoData.decotype);
                            if(hit.collider.name=="MoveBtn"){
                                movePosition();
                            }else if(hit.collider.name=="MoveCancelBtn"){
                                Debug.Log("["+name +" Update] 이동 취소 ("+decoData.locx+", "+decoData.locz+")");
                                moveCancel(); //원위치로 이동
                            }else{
                            Debug.Log("["+name +" Update] 마우스 클릭 인식 ("+hit.point.x+", "+hit.point.z+")");
                            newposition=new Vector3((int)hit.point.x,0,(int)hit.point.z); //정수 형태의 숫자로 저장
                            this.transform.position=newposition;
                            }
                        }                           
                    }else{
                        if (hit.collider != null)
                        {  Debug.Log("["+name+"] hit collider : "+hit.collider.name+", decotype : "+decoData.decotype);
                            if(hit.collider.name==decoData.decotype){
                                if(gameManager.IsOneMove==false) {//다른 오브젝트가 움직이고있지 않을 때
                                        gameManager.IsOneMove=true; //중복 배치를 막기 위해, 자신의 배치하고 있다는 것을 알림
                                        moveBtns.SetActive(true);
                                        IsMoving=true;
                                    }
                            }
                        }
                    }
            }
        }
    }

    public void initdata(DecoData dd){
            decoData=dd;
            string[] namesplit=name.Split('@');
            decoData.decotype=namesplit[0];
            decoData.deconum=Int32.Parse(namesplit[1]); //Int32: using System
            Debug.Log("["+name+"] init : "+decoData.deconum);
    }


    public void init(){
        initdata(decoData);
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager=GameObject.Find("AudioManager").GetComponent<AudioManager>();
        IsMoving=false;
        moveBtns.SetActive(false);
    }

    public void movePosition(){ //배치 완료 버튼을 누르면 실행되는 함수
            IsMoving=false;
            decoData.locx=(int)newposition.x;
            decoData.locz=(int)newposition.z;
            gameManager.IsOneMove=false; //중복 배치를 막기 위해, 자신의 배치하고 있다는 것을 알림
            moveBtns.SetActive(false);
            //gameManager.SaveProduction(decoData.productnum, ref decoData);
    }

    public void moveCancel(){ //배치 취소 버튼을 누르면 실행되는 함수
        IsMoving=false;
        Vector3 tem=new Vector3(decoData.locx,0,decoData.locz);
        this.transform.position=tem;
        gameManager.IsOneMove=false;
        moveBtns.SetActive(false);
    }
}
