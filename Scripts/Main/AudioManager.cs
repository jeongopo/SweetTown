using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audioGet;
    public AudioClip audioIn;
    public AudioClip audioOut;

    public AudioSource audioSource;

    void Start()
    {
        this.audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void audioPlay(string action){
        switch(action) {
            case "audioGet" : 
                audioSource.clip=audioGet;
                break;
        
            case "audioIn" : 
                audioSource.clip=audioIn;
                break;

            case "audioOut" : 
                audioSource.clip=audioOut;
                break;

            default :
                audioSource.clip=audioGet;
                break;
        }

        audioSource.Play();
    }
    
}
