using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bakingpanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
        
            if (hit.collider != null)
                {
                    if(hit.collider.name==name){
                        Debug.Log(name+"클릭");
                        bakingpanel.SetActive(true);
                    }
                }
            }
        
    }


}
