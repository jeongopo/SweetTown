using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntroGM : MonoBehaviour
{

    public Image logo;
    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator=logo.GetComponent<Animator>();
        audioSource=this.GetComponent<AudioSource>();
        animator.SetBool("IsStart",false);

        Debug.Log("애니메이션");
        animator.SetBool("IsStart",true);
        Debug.Log("소리");
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
