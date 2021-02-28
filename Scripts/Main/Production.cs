using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DataInfo;

public class Production : MonoBehaviour
{

    public ProductionData productionData;
    public GameManager gameManager;
    public AudioManager audioManager;
    public GameObject moveBtns;
    public GameObject popup;
    public float time; //생산시간
    public int p_Ingre; //생산 재료 번호
    public int p_ex; //생산시 획득하는 경험치

    bool IsFinish=false; //재료가 만들어졌는지
    public bool IsMoving=false; //현재 이동중인지
    bool IsCollision=false;

    DateTime startTime; //시작시간
    Vector3 newposition; //이동하는 경우, 이동값

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if((gameManager.IsMove==true)&&gameManager.IsNew==productionData.productnum){ //새로 구매한 구조물인 경우
            Debug.Log("["+name+"] 구매된 건물");
            popup.SetActive(false);
            moveBtns.SetActive(true);
            IsMoving=true;
            if(Input.GetMouseButtonDown(0)){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if(IsMoving==true){
                        if(hit.collider != null){
                            //Debug.Log("["+name+"] Move collider : "+hit.collider.name+", producttype : "+productionData.producttype);
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
            popup.SetActive(false);
            if(Input.GetMouseButtonDown(0)){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);

                    if(IsMoving==true){
                        if (hit.collider != null){
                            //Debug.Log("["+name+"] Move collider : "+hit.collider.name+", producttype : "+productionData.producttype);
                            if(hit.collider.name=="MoveBtn"){
                                movePosition();
                            }else if(hit.collider.name=="MoveCancelBtn"){
                                Debug.Log("["+name +" Update] 이동 취소 ("+productionData.locx+", "+productionData.locz+")");
                                moveCancel(); //원위치로 이동
                            }else{
                            Debug.Log("["+name +" Update] 마우스 클릭 인식 ("+hit.point.x+", "+hit.point.z+")");
                            newposition=new Vector3((int)hit.point.x,0,(int)hit.point.z); //정수 형태의 숫자로 저장
                            this.transform.position=newposition;
                            }
                        }                           
                    }else{
                        if (hit.collider != null)
                        {  Debug.Log("["+name+"] hit collider : "+hit.collider.name+", producttype : "+productionData.producttype);
                            if(hit.collider.name==productionData.producttype){
                                if(gameManager.IsOneMove==false) {//다른 오브젝트가 움직이고있지 않을 때
                                        gameManager.IsOneMove=true; //중복 배치를 막기 위해, 자신의 배치하고 있다는 것을 알림
                                        moveBtns.SetActive(true);
                                        IsMoving=true;
                                    }
                            }
                        }
                    }
            }
        }else if(gameManager.IsInUI==false){ //배치 상태가 아닐 때, UI 입장중도 아님
            if(IsFinish==false) {pop();} //재료를 획득한 다음, 자동 pop
            else {
                if (Input.GetMouseButtonDown(0)){
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);
        
                    if (hit.collider != null)
                    {
                        Debug.Log("["+name+"] hit collider : "+hit.collider.name+", producttype : "+productionData.producttype);
                        if(hit.collider.name==productionData.producttype){
                            Debug.Log(name+"생산 완료");
                            productGet();
                        }
                    }
                }
            }
        }

        
    }

    public void initdata(ProductionData pd){
            productionData=pd;
            string[] namesplit=name.Split('@');
            productionData.producttype=namesplit[0];
            productionData.productnum=Int32.Parse(namesplit[1]);
            Debug.Log("["+name+"] init : "+productionData.productnum);
    }
    public void init(){
        initdata(productionData);
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager=GameObject.Find("AudioManager").GetComponent<AudioManager>();
        popup.SetActive(false);
        IsMoving=false;
        moveBtns.SetActive(false);
        IsCollision=false;
    }

    public void productGet(){
        audioManager.audioPlay("audioGet");
        popup.SetActive(false);
        IsFinish=false;
        gameManager.getIngredients(p_Ingre);
        gameManager.exprience+=p_ex;
        startTime=DateTime.Now;    
    }

    public void pop(){ //팝업창을 만들기
        TimeSpan gabTime=DateTime.Now-startTime; 
        //Debug.Log("시간차이 : "+gabTime.Seconds);
        if(gabTime.Days>=0 && gabTime.Hours>=0 && gabTime.Minutes>=0 && gabTime.Seconds>=time){ //생산 시간이 지났을 때
            Debug.Log("생산완료");
            IsFinish=true;
            popup.SetActive(true);
            //gameManager.SaveProduction(productionData.productnum,ref productionData);
        }
    }

    public void movePosition(){ //배치 완료 버튼을 누르면 실행되는 함수
            IsMoving=false;
            productionData.locx=(int)newposition.x;
            productionData.locz=(int)newposition.z;
            gameManager.IsOneMove=false; //중복 배치를 막기 위해, 자신의 배치하고 있다는 것을 알림
            moveBtns.SetActive(false);
            //gameManager.SaveProduction(productionData.productnum, ref productionData);
    }

    public void moveCancel(){ //배치 취소 버튼을 누르면 실행되는 함수
        IsMoving=false;
        Vector3 tem=new Vector3(productionData.locx,0,productionData.locz);
        this.transform.position=tem;
        gameManager.IsOneMove=false;
        moveBtns.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision){
        if(collision.collider.tag=="production"){
            IsCollision=true;
        }
    }
    public void OnCollisionEnd(Collision collision){
        if(collision.collider.tag=="production"){
            IsCollision=false;
        }
    }

    public void startMove(){
        gameManager.IsOneMove=true; //중복 배치를 막기 위해, 자신의 배치하고 있다는 것을 알림
        moveBtns.SetActive(true);
        IsMoving=true;
        Debug.Log("["+name+"] 배치 시작");
    }


}





