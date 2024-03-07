using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource buttonSource;
    public AudioSource completedSource;
    public AudioSource beginSource;

    public AudioClip buttonclip;
    public AudioClip completedclip;
    public AudioClip beginclip;


    public void ButtonSound()
    {
        
        buttonSource.PlayOneShot(buttonclip);


    }
    public void CompletedSound()
    {

        completedSource.PlayOneShot(completedclip);


    }
    public void BeginSound()
    {

        beginSource.PlayOneShot(beginclip);


    }
}
