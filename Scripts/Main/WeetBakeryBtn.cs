using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeetBakeryBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bakerypanel;
    
    GameManager gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetTouch(0).phase==TouchPhase.Ended){
            Debug.Log("터치");
            bakerypanel.SetActive(true);
            gameManager.heart=100;
            gameManager.userSave();
        }
    }

}
