using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo{
    
    [System.Serializable]
    public class GameData{
        public List<ProductionData> productlist=new List<ProductionData>();
    }

    [System.Serializable]
    public class UserData //MonoVehaviour의 상속을 받지 않음
    { //유저에 대한 정보를 저장하고 있음
        public int level=1; //레벨
        public int exprience=1; //레벨 경험치
        public string name="익명"; // 유저가 설정한 이름
        public int cooklev=1; //제빵기술
        public int cookval=0; //디저트 제작 횟수
        public int heart=0; //하트 수치
        public int cloud=0; //구름 수치
        public int star=0; //별 수치
    }

    [System.Serializable]
    public class ProductionData{
        public int productnum=0; //오브젝트 넘버
        public bool isRun=false; //현재 가동중인지
        public bool IsFinish=false; //가동이 끝났는지
        public Time time; //종료 시간
        public int locx=0; //오브젝트의 x위치
        public int locy=0; //오브젝트의 y위치
    }
}
