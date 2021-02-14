using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    private string dataPath;

    public void Start(){
        init();
    }
    public void init(){
        dataPath=Application.persistentDataPath+"/UserData.dat";
    }

    public void userSave(UserData userData){
        BinaryFormatter bf=new BinaryFormatter();
        FileStream file=File.Create(dataPath); //데이터를 저장하기 위한 파일
    
        UserData user=new UserData();

        user.level=userData.level; //레벨
        user.exprience=userData.exprience; //레벨 경험치
        user.name=userData.name; // 유저가 설정한 이름
        user.cooklev=userData.cooklev; //제빵기술
        user.cookval=userData.cookval; //디저트 제작 횟수
        user.heart=userData.heart; //하트 수치
        user.cloud=userData.cloud; //구름 수치
        user.star=userData.star; //별 수치

        bf.Serialize(file,user);
        //Debug.Log("dataPath :: "+dataPath);
        file.Close();
    }

    public UserData userLoad(){
        if(File.Exists(dataPath)){
            BinaryFormatter bf=new BinaryFormatter();
            FileStream file=File.Open(dataPath,FileMode.Open);

            UserData user=(UserData)bf.Deserialize(file);
            
            Debug.Log("데이터 로드 "+user.heart);
            file.Close();

            return user;
        }
        else {
            UserData user=new UserData();
            return user;
        }
    }
}
