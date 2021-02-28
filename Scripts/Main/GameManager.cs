using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;
using System;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera maincam;
    public GameObject BakeryPanel;
    public GameObject BasicPanel;
    public GameObject MakePanel;
    public GameObject ShopPanel;
    public Text HeartText;
    public Text CloudText;
    public Text StarText;
    public Text NameText;
    public Text LevText;
    public Text ExText;
    public GameObject MoveBtn;
    public GameObject MoveEndBtn;
    public Text UiHeartText;
    public Text UiCloudText;
    public Text UiStarText;
    public Image levelgaze;

    public int heart=0;
    public int cloud=0;
    public int star=0;
    public int level=1;
    public int cookval=0;
    public int cooklev=1;
    public int exprience=0;
    public int maxEx=0;
    public int[] ingredients=new int[11]; //재료 정보

    public string username="";
    Animator animator;
    public DataManager dataManager;
    List<ProductionData> productionDatas=new List<ProductionData>();
    List<DecoData> DecorationDatas=new List<DecoData>();

    string[] productionnames;
    int objcount=0;
    public bool IsMove=false; //배치상태 아님
    public bool IsOneMove=false; //누군가 이동중인지. 중복 건물 이동을 막기 위함
    public int IsNew=0; //새로 상점에서 구매했다면 제대로 배치하도록 해야함
    public bool IsInUI=false;

    public GameObject GoldFactory;
    public GameObject SandFactory;
    float m_fOldToucDis = 0;       // 이전 터치 거리
    float m_fFieldOfView = 45;


    void Start()
    {
        animator=maincam.GetComponent<Animator>();
        setCamera();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        UserTouch();

        //setValue();
        if(IsInUI==true){
            uiUpdate();    //RigthTop 정보만 업데이트
        }else {
            userUpdate(); //전체 유저정보 업데이트
        }
        //userSave();
    }

    void Destroy(){
        userSave();
    }

    void init(){

        BakeryPanel.SetActive(false);
        BasicPanel.SetActive(true);
        MakePanel.SetActive(false);
        ShopPanel.SetActive(false);
        MoveEndBtn.SetActive(false);
        setValue();
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Intro"));
        setMap();
        IsMove=false;

    }

    void uiUpdate(){ //UI에 들어가 있을 때 업데이트할 내용
        Debug.Log("UI 입장");
        UiHeartText.text=heart.ToString();
        UiCloudText.text=cloud.ToString();
        UiStarText.text=star.ToString();
    }
    void userUpdate(){ //매 프레임마다 데이터 값을 표시하기
        HeartText.text=heart.ToString();
        CloudText.text=cloud.ToString();
        StarText.text=star.ToString();

        if(exprience>=maxEx){ //레벨업
            level++;
            maxEx=maxExCal(level);
            exprience=0;
        }

        LevText.text="Lv. "+level.ToString();
        ExText.text=exprience.ToString()+" / "+maxEx.ToString();
        

        NameText.text=username;
    }

    void setValue(){ //씬에 들어올 때 저장된 데이터를 불러오기
        UserData user=dataManager.userLoad();
        username=user.name;
        level=user.level;
        cooklev=user.cooklev;
        heart=9999;
        cloud=user.cloud;
        star=99999;
        ingredients=user.ingredients;
        maxEx=maxExCal(level);

        if(GameObject.Find("SetUserData")!=null){
            username=GameObject.Find("SetUserData").GetComponent<SetUserData>().setusername;
            Debug.Log("[SetValue] 튜토리얼 종료 : "+username);
        }
    }

    int maxExCal(int lev){
        switch(lev){
            case 1: return 3500;
            case 2: return 7000;
            case 3: return 500;
            case 4: return 30000;
            case 5: return 50000;
            case 6: return 80000;
            default: return -1;
        }
    }
    void setCamera(){ //카메라 값 초기설정
        animator.SetBool("IsZoom",false);
        animator.SetBool("IsZoom",true);
        maincam.orthographicSize=45;
        //animator.SetBool("IsZoom",false);
    }

    public void userSave(){
        UserData user=new UserData();

        user.level=level; //레벨
        user.exprience=exprience; //레벨 경험치
        user.name=username; // 유저가 설정한 이름
        user.cooklev=cooklev; //제빵기술
        user.cookval=cookval; //디저트 제작 횟수
        user.heart=heart; //하트 수치
        user.cloud=cloud; //구름 수치
        user.star=star; //별 수치
        user.ingredients=ingredients;

        //Debug.Log("[setValue] 불러온 데이터 : "+ user.ingredients[1]);

        dataManager.userSave(user);
    }

    public void UserTouch(){
        int nTouch = Input.touchCount;
        float m_fToucDis = 0f;
        float fDis = 0f;
 
        // 터치가 두개이고, 두 터치중 하나라도 이동한다면 카메라의 fieldOfView를 조정합니다.
        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
        {
            Debug.Log("[UserTouch] 터치 인식");
            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;
 
            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;
 
            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
            m_fFieldOfView -= fDis;
 
            //최대 최소 확대 거리
            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 10,100);
 
            // 확대 / 축소가 갑자기 되지않도록 보간합니다.
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);
 
            m_fOldToucDis = m_fToucDis;
        }
    }

    public void setMap(){ //초기 맵 설정

        productionDatas=dataManager.PDLoad().buildlist;
        objcount=productionDatas.Count;
        Debug.Log("[setMap] 초기 맵 설정, objcount 값 : "+objcount);

        if(objcount!=0){
            for(int i=0;i<objcount;i++){ //저장된 정보들로 맵을 구성하기
                Debug.Log("[SetMap] 맵 정보 로드 "+productionDatas[i].producttype);
                GameObject tem=Instantiate(GameObject.Find(productionDatas[i].producttype),new Vector3(productionDatas[i].locx,0,productionDatas[i].locz),Quaternion.identity);  
                tem.name=productionDatas[i].producttype+i;
                tem.GetComponent<Production>().initdata(productionDatas[i]); // 건물 내용 전달하기
                Debug.Log("[setMaptem] product 인자 값 확인 : "+tem.GetComponent<Production>().productionData.locx);
            }
        }
    }

    public void saveMap(){
        dataManager.PDSave(productionDatas,objcount);
    }

    public int SetNumber(){ //구조물의 번호 부여
        objcount++;
        Debug.Log("[SetNumber] 넘버 출력 : "+objcount);
        return objcount; //다음 인덱스를 전달
    }

    public void newDecoration(string prefabname){ //새로 생산공장을 만드는 함수
            bool check=false;
            int minusstar=0;
            int plusheart=0;

            switch(prefabname){
                case "MacaronTree": check=IsPurable(1000,0,1);
                                minusstar=1000;
                                plusheart=30;
                                break;
                case "Pudding" : check=IsPurable(2000,0,1);
                                minusstar=2000;
                                plusheart=60;
                                break;
                case "JellyTree" :check=IsPurable(4000,0,2);
                                minusstar=4000;
                                plusheart=150;
                                break;
            }

            if(check){
                this.star-=minusstar;
                this.heart+=plusheart;
                GameObject tem=Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+prefabname+".prefab", typeof(GameObject)),new Vector3(50,0,50),Quaternion.identity);  //GoldFactory를 Vector(0,0,0) 위치에 회전각을 0,0,0으로 하여 복제
                int temnum=SetNumber();
                tem.name=prefabname+"@"+temnum;

                DecoData decoration=new DecoData();
                decoration.decotype=prefabname;
                decoration.deconum=temnum;

                DecorationDatas.Add(decoration);
                //tem.GetComponent<Decoration>().init();
                //tem.GetComponent<Decoration>().initdata(DecorationDatas[temnum-1]); // 건물 내용 전달하기
                Debug.Log("[newProduction] product 인자 값 확인 : "+tem.GetComponent<Decoration>().decoData.decotype);
                ShopPanel.SetActive(false);
                BasicPanel.SetActive(true);
                IsInUI=false;

                IsNew=temnum;
                IsOneMove=true;

                MoveStart();
            }else Debug.Log("[newProduction] 구매 불가능");
    }

    public void newProduction(string prefabname){ //새로 생산공장을 만드는 함수
            bool check=false;
            int minusstar=0;

            switch(prefabname){
                case "GoldFactory": check=IsPurable(1000,100,1);
                                minusstar=1000;
                                break;
                case "SandFactory" : check=IsPurable(5000,300,1);
                                minusstar=5000;
                                break;
                case "HellFruit" :check=IsPurable(15000,500,2);
                                minusstar=15000;
                                break;
            }

            if(check){
                this.star-=minusstar;
                GameObject tem=Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+prefabname+".prefab", typeof(GameObject)),new Vector3(50,0,50),Quaternion.identity);  //GoldFactory를 Vector(0,0,0) 위치에 회전각을 0,0,0으로 하여 복제
                int temnum=SetNumber();
                tem.name=prefabname+"@"+temnum;

                ProductionData production=new ProductionData();
                production.producttype=prefabname;
                production.productnum=temnum;

                productionDatas.Add(production);
                tem.GetComponent<Production>().init();
                //tem.GetComponent<Production>().initdata(productionDatas[temnum-1]); // 건물 내용 전달하기
                Debug.Log("[newProduction] product 인자 값 확인 : "+tem.GetComponent<Production>().productionData.producttype);
                ShopPanel.SetActive(false);
                BasicPanel.SetActive(true);
                IsInUI=false;

                IsNew=temnum;
                IsOneMove=true;

                MoveStart();
            }else Debug.Log("[newProduction] 구매 불가능");
    }

    bool IsPurable(int star, int heart,int level){ //구매 가능여부 판단
        if(this.star<star) return false;
        else if(this.heart<heart) return false;
        else if(this.level<level) return false;
        else return true;
    }

    public void MoveStart(){ //배치시작임을 알려주는 함수 (외부에서 접근)
        MoveBtn.SetActive(false);
        MoveEndBtn.SetActive(true);
        IsMove=true;
    }
    public void MoveEnd(){ //배치 끝임을 알려주는 함수
        MoveBtn.SetActive(true);
        MoveEndBtn.SetActive(false);
        IsMove=false;
    }

    public void SaveProduction(int objnum,ref ProductionData pd){ //배치가 끝난 후, 해당 정보를 저장함
        productionDatas[objnum]=pd;
        // productionDatas[objnum].productnum=pd.productnum;
        // productionDatas[objnum].producttype=pd.producttype;
        // productionDatas[objnum].IsFinish=pd.IsFinish;
        // productionDatas[objnum].time=pd.time;
        // productionDatas[objnum].locx=pd.locx;
        // productionDatas[objnum].locz=pd.locz;

        Debug.Log("[SaveProduction] 인자값 : "+objnum);
        Debug.Log("[SaveProduction] 값 확인 : "+ productionDatas[objnum].locx);

        dataManager.PDSave(productionDatas,objcount);

    }

    public void getIngredients(int num){
        // Debug.Log("[GetIngredients] num 확인 : "+num);
        Debug.Log("[GetIngredients] ingredients 확인 : "+ingredients[num]);
        ingredients[num]++;
        userSave();
    }

    public void uiIn(bool val){
        IsInUI=val;
    }



}
