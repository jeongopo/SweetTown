using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelbar : MonoBehaviour
{
    public GameManager gameManager;

    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect=GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float tem=(gameManager.exprience/gameManager.maxEx)*180f;
        int temposition=90-(gameManager.exprience/gameManager.maxEx)*90;
        Debug.Log("[Levelbar] 경험치 가로 길이 : "+tem+", 가로 위치 : "+temposition);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tem); //가로길이
        rect.anchoredPosition=new Vector2(temposition,185); //위치
    }
}
