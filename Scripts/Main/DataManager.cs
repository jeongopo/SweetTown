using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class DataManager : MonoBehaviour
{
    private string userdataPath;
    private string PDdataPath;

    public void Start(){
        init();
    }
    public void init(){
        userdataPath=Application.persistentDataPath+"/UserData.dat";
        PDdataPath=Application.persistentDataPath+"/ProductBuildData.dat";
    }

    public void userSave(UserData userData){
        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(userdataPath); //데이터를 저장하기 위한 파일
    
        UserData user=new UserData();

        user.level=userData.level; //레벨
        user.exprience=userData.exprience; //레벨 경험치
        user.name=userData.name; // 유저가 설정한 이름
        user.cooklev=userData.cooklev; //제빵기술
        user.cookval=userData.cookval; //디저트 제작 횟수
        user.heart=userData.heart; //하트 수치
        user.cloud=userData.cloud; //구름 수치
        user.star=userData.star; //별 수치
        user.ingredients=userData.ingredients; //유저 재료 정보

        bf.Serialize(file,user);
        //Debug.Log("dataPath :: "+dataPath);
        file.Close();
    }

    public UserData userLoad(){
        if(File.Exists(userdataPath)){
            BinaryFormatter bf=new BinaryFormatter();
            FileStream file=File.Open(userdataPath,FileMode.Open);

            UserData user=(UserData)bf.Deserialize(file);
            
            Debug.Log("데이터 로드 "+user.ingredients[1]);
            file.Close();

            return user;
        }else {
            UserData user=new UserData();
            return user;
        }
    }

    public void PDSave(List<ProductionData> productionDatas,int objcount){
        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(PDdataPath); //데이터를 저장하기 위한 파일

        ProductBuildData pdd=(ProductBuildData)bf.Deserialize(file);
        pdd.buildlist=productionDatas;

        bf.Serialize(file,pdd);
        file.Close();
    }

    public ProductBuildData PDLoad(){
    if(File.Exists(PDdataPath)){
            BinaryFormatter bf=new BinaryFormatter();
            FileStream file=File.Open(PDdataPath,FileMode.Open);

            ProductBuildData pdd=(ProductBuildData)bf.Deserialize(file);
            Debug.Log("[PDLoad] 로드 있음 : "+pdd.buildlist.Count);

            file.Close();

            return pdd;
        }else {
            ProductBuildData pdd=new ProductBuildData();
            pdd.buildlist=new List<ProductionData>();
            Debug.Log("[PDLoad] 로드 없음 : "+pdd.buildlist.Count);
            return pdd;
        }
    }

/*
    public void PDSave(ref List<ProductionData> productionDatas,int objcount){

        Debug.Log("[PDSave] 인자 확인 : "+productionDatas[0].producttype+", objcount : "+objcount);

        string savestr="";

        StringBuilder sb = new StringBuilder();

        if(productionDatas.Count>1){
            for(int i=0;i<objcount;i++){
                Debug.Log("[PDSave] producttype 데이터 확인 : "+productionDatas[i].producttype);
                sb.Append(productionDatas[i].productnum.ToString());
                sb.Append("|");
                sb.Append(productionDatas[i].producttype.ToString());
                sb.Append("|");
                sb.Append(productionDatas[i].IsFinish.ToString());
                sb.Append("|");
                sb.Append(productionDatas[i].time.ToString());
                sb.Append("|");
                sb.Append(productionDatas[i].locx.ToString());
                sb.Append("|");
                sb.Append(productionDatas[i].locz.ToString());
                sb.Append("|");

                // savestr+=productionDatas[i].productnum+"|";
                // savestr+=productionDatas[i].producttype+"|";
                // savestr+=productionDatas[i].IsFinish+"|";
                // savestr+=productionDatas[i].time+"|";
                // savestr+=productionDatas[i].locx+"|";
                // savestr+=productionDatas[i].locz+"|";
            }
        }
        Debug.Log("[PDSave] sb 값 : "+ sb.ToString());
        PlayerPrefs.DeleteKey("Production");
        PlayerPrefs.SetString("Production", sb.ToString()); //문자열로 데이터 저장
    }

    public int PDLoad(ref List<ProductionData> productionDatas){
        string checkstr= PlayerPrefs.GetString("Production");
        Debug.Log("[PDLoad] 데이터 확인 : "+checkstr);
        string[] dataArr =checkstr.Split('|'); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
        if(dataArr.Length>1){//배열이 비어있지 않으면 데이터값을 출력
            
            int i;
            for (i = 0; i < dataArr.Length; i=i+7)
            {
                Debug.Log("[PDLoad] 데이터 확인 , "+i+"번째 "+dataArr[i+3]);
                ProductionData tem=new ProductionData();
                tem.productnum=System.Convert.ToInt32(dataArr[i]);
                tem.producttype=dataArr[i+1];
                tem.IsFinish=System.Convert.ToBoolean(dataArr[i+2]);
                tem.time=System.Convert.ToDateTime(dataArr[i+3]);
                tem.locx=System.Convert.ToInt32(dataArr[i+4]);
                tem.locz=System.Convert.ToInt32(dataArr[i+5]);
                productionDatas[i/6+1]=tem;
            }        
            return i-6;
        }else {//데이터 값이 없을 경우, 빈 배열을 리턴
            Debug.Log("[PDLoad] 데이터 없음");
            
            return 0;
        }
        

    }
    */
}
