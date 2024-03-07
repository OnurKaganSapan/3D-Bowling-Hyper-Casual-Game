using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public Animator LayoutAnimator;
    public GameObject Settings_Open;
    public GameObject Settings_Close;
    public GameObject Sounds_On;
    public GameObject Sounds_Off;
    public GameObject Vibration_On;
    public GameObject Vibration_Off;
    public GameObject Iap;
    public GameObject Information;
    public GameObject IntroHand;
    public GameObject TapToMoveText;
    private bool isVibrationEnabled = true;


    public void settings_open( )
    {
        Settings_Open.SetActive( false );
        Settings_Close.SetActive( true );
        LayoutAnimator.SetTrigger("Slide_in");

    }

    public void settings_close()
    {
        Settings_Close.SetActive(false);
        Settings_Open.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_out");

    }


    public void LayoutSettingsOpen() {

        LayoutAnimator.SetTrigger("Slide_in");
    }

    public void LayoutSettingsClose()
    {

        LayoutAnimator.SetTrigger("Slide_out");
    }


    public void settings_Open()
    {
        Settings_Open.SetActive(false);
        Settings_Close.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_in");


    }
    public void settings_Close()
    {
        Settings_Open.SetActive(true);
        Settings_Close.SetActive(false);
        Settings_Close.SetActive(false);


    }

    public void sound_On()
    {
        Sounds_On.SetActive(false);
        Sounds_Off.SetActive(true);
      
        AudioListener.volume = 0f;
        PlayerPrefs.SetInt("SoundEnabled", 0); // Ses kapalý
    }

    public void sound_Off()
    {
        Sounds_On.SetActive(true);
        Sounds_Off.SetActive(false);
        AudioListener.volume = 1f;
        PlayerPrefs.SetInt("SoundEnabled", 1); // Ses açýk
    }

    void Start()
    {
        // Uygulama baþlatýldýðýnda ses ayarýný yükleyin
        int soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1); // Varsayýlan olarak ses açýk
        AudioListener.volume = soundEnabled == 1 ? 1f : 0f;
        Sounds_On.SetActive(soundEnabled == 1);
        Sounds_Off.SetActive(soundEnabled == 0);
    }
    public void vibration_On()
    {
        Vibration_On.SetActive(false);
        Vibration_Off.SetActive(true);
        isVibrationEnabled = true;
    }

    public void vibration_Off()
    {
        Vibration_On.SetActive(true);
        Vibration_Off.SetActive(false);
        isVibrationEnabled = false;
    }
    public void FirstTouch()


    {
        Settings_Open.SetActive(false);;
        Settings_Close.SetActive(false);;
        Sounds_Off.SetActive(false);;
        Vibration_On.SetActive(false);;
        Vibration_Off.SetActive(false);;
        Iap.SetActive(false);;
        Information.SetActive(false);;
        IntroHand.SetActive(false);
        TapToMoveText.SetActive(false);

    }





}
