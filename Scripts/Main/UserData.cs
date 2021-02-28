using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DataInfo{
    
    [System.Serializable]
    public class GameData{
        public List<ProductionData> productlist=new List<ProductionData>();
    }

    [System.Serializable]
    public class UserData //MonoVehaviour의 상속을 받지 않음
    { //유저에 대한 정보를 저장하고 있음
        public int level=3; //레벨
        public int exprience=1; //레벨 경험치
        public string name="익명"; // 유저가 설정한 이름
        public int cooklev=1; //제빵기술
        public int cookval=0; //디저트 제작 횟수
        public int heart=100000; //하트 수치
        public int cloud=0; //구름 수치
        public int star=100000; //별 수치
        public int[] ingredients=new int[10];
    }

    [System.Serializable]
    public class ProductionData{
        public int productnum; //오브젝트 넘버
        public string producttype; //구조물 타입
        public bool IsFinish; //가동이 끝났는지
        public DateTime time; //종료 시간
        public int locx; //오브젝트의 x위치
        public int locz; //오브젝트의 z위치

        // public ProductionData(int productnum, string producttype, bool IsFinish, DateTime time, int locx,int locz){
        //     this.productnum=productnum;
        //     this.producttype=producttype;
        //     this.IsFinish=IsFinish;
        //     this.time=time;
        //     this.locx=locx;
        //     this.locz=locz;
        // }

        public void setValue(int productnum, string producttype, bool IsFinish, DateTime time, int locx,int locz){
            this.productnum=productnum;
            this.producttype=producttype;
            this.IsFinish=IsFinish;
            this.time=time;
            this.locx=locx;
            this.locz=locz;
        }
    }

    [System.Serializable]
    public class DecoData{
        public int deconum; //오브젝트 넘버
        public string decotype; //구조물 타입
        public int locx; //오브젝트의 x위치
        public int locz; //오브젝트의 z위치
    }


    [System.Serializable]
    public class ProductBuildData{
        public List<ProductionData> buildlist;
    }
    
}

