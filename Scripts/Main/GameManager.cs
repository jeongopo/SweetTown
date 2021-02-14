using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera maincam;
    public GameObject BakeryPanel;
    public GameObject BasicPanel;
    public Text HeartText;
    public Text CloudText;
    public Text StarText;
    public Text NameText;
    public Text LevText;
    public Text CookLevText;


    public int heart=0;
    public int cloud=0;
    public int star=0;
    public int level=1;
    public int cookval=0;
    public int cooklev=1;
    public int exprience=0;

    public string username="";
    Animator animator;
    public DataManager dataManager;

    void Start()
    {
        animator=maincam.GetComponent<Animator>();
        setCamera();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        setValue();
        userUpdate();
        userSave();
    }

    void Destroy(){
        userSave();
    }

    void init(){
        BakeryPanel.SetActive(false);
        BasicPanel.SetActive(true);
        setValue();
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Intro"));
    }

    void userUpdate(){ //매 프레임마다 데이터 값을 표시하기
        HeartText.text=heart.ToString();
        CloudText.text=cloud.ToString();
        StarText.text=star.ToString();
        LevText.text="Lv. "+level.ToString();

        switch(cooklev){
            case 1:CookLevText.text="열정제빵사";
                break;
            case 2: CookLevText.text="천재제빵사";
                break;
            case 3: CookLevText.text="신의제빵사";
                break;
        }
        NameText.text=username;
    }

    void setValue(){ //씬에 들어올 때 저장된 데이터를 불러오기
        UserData user=dataManager.userLoad();
        username=user.name;
        level=user.level;
        cooklev=user.cooklev;
        heart=user.heart;
        cloud=user.cloud;
        star=user.star;
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

        dataManager.userSave(user);
    }

}
