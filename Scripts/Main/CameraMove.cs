using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Camera maincam;
    float speed=0.1f;
    float distance=50f;
    Vector3 start;
    Vector3 end;

    // void OnMouseDrag()
    // {
    //     Vector3 mousePosition = new Vector3(Input.mousePosition.x, 30f, Input.mousePosition.z);
    //     Vector3 worldPosition=Camera.main.ScreenToWorldPoint(mousePosition);
    //     worldPosition.y=80f;
    //     worldPosition.x-=distance;
    //     if(worldPosition.x>250) worldPosition.x=250;
    //     else if(worldPosition.x<0) worldPosition.x=0;
        
    //     worldPosition.z-=distance;
    //     if(worldPosition.z>210) worldPosition.z=210;
    //     else if(worldPosition.z<0) worldPosition.z=0;

    //     Debug.Log("드래그 ("+worldPosition.x+", "+ worldPosition.z);

    //     maincam.transform.position =Vector3.MoveTowards(maincam.transform.position,mousePosition,1f); 
    // }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("클릭  ("+Input.mousePosition.x+", "+ Input.mousePosition.y+")");
            start=new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z);
        }
        if(Input.GetMouseButtonUp(0)){
            Debug.Log("클릭 끝  ("+Input.mousePosition.x+", "+ Input.mousePosition.y+")");
            end=new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z);

            if(start!=end){//터치가 아님
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, 30f, Input.mousePosition.z);
            Vector3 worldPosition=Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.y=80f;
            worldPosition.x-=distance;
            if(worldPosition.x>250) worldPosition.x=250;
            else if(worldPosition.x<0) worldPosition.x=0;
            
            worldPosition.z-=distance;
            if(worldPosition.z>210) worldPosition.z=210;
            else if(worldPosition.z<0) worldPosition.z=0;

            Debug.Log("드래그 ("+worldPosition.x+", "+ worldPosition.z);

            maincam.transform.position =Vector3.Lerp(maincam.transform.position,mousePosition, 0.2f); 
            }
        }
    }

/*
    public Transform Camera;
    private Vector2 moverotation;
    private Vector2 FirstPosition;

    public float speed;
    private bool isTouch=false;

    Quaternion pitch;
    Quaternion yaw;

    // Start is called before the first frame update
    void Start()
    {
        Camera=GameObject.Find("Main Camera").transform;
        speed=5f*Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouch){
           
            pitch=Quaternion.Euler(-moverotation.y,0,0);
            yaw=Quaternion.Euler(0,moverotation,0);

            Camera.localRotation=yaw+Camera.localRotation;
            Camera.localRotation=Camera.localRotation*pitch;
            
        }
    }

    public void OnDrag(PointerEventData eventData){

        Vector2 val=eventData.position-FirstPosition;
        val=val.normalized;

        moverotation=new Vector2(val.x*100*Time.deltaTime,val.y*100*Time.deltaTime);
    } 

    public void OnPointerDown(PointerEventData eventData){
        FirstPosition=eventData.position;
        isTouch=true;
    }
    public void OnPointerUp(PointerEventData eventData){
        FirstPosition=Vector2.zero;
        moverotation=Vector2.zero;
        isTouch=false;
    }
    */



}
