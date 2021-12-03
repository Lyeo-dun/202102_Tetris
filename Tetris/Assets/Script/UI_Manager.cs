using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager S;
    public AudioSource audioSource;
    public Slider Volume;
    public GameObject Option_Window;
    public GameObject Game_Over_Window;
    public AudioSource Game_Over_BGM;

    public void Awake()
    {
        S = this;
        if (Game_Over_BGM)
        {
            Game_Over_BGM.Stop();
        }
        if (Game_Over_Window != null)
        {
            Game_Over_Window.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "StartScene" && !PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", audioSource.volume);
        }
        Volume.value = PlayerPrefs.GetFloat("Volume");
        PlayerPrefs.SetFloat("Volume", Volume.value);
        Option_Window.SetActive(false);
    }

    public void Click_Start()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Click_Help()
    {
        SceneManager.LoadScene("HelpScene");
    }
    public void Click_Option()
    {
        Option_Window.SetActive(true);
    }
    public void Click_Op_Close()
    {
        Option_Window.SetActive(false);
    }
    public void Click_ReStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Click_Main()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Show_GameOver()
    {
        Game_Over_Window.SetActive(true);
        audioSource.Stop();
        Game_Over_BGM.volume = PlayerPrefs.GetFloat("Volume");
        Game_Over_BGM.Play();
    }
    public void Update()
    {
        audioSource.volume = Volume.value;
        PlayerPrefs.SetFloat("Volume", audioSource.volume);
    }
}
