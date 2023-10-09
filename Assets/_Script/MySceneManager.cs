using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonobehavior<MySceneManager>
{
    //[SerializeField] private AudioClip _buttonClip;
    [SerializeField] private AudioClip _BGMClip;
    [SerializeField] GameObject SoundPanel;
    private void Start()
    {
        AudioManager.instance.PlayBGM(_BGMClip);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ChangeSceneBGM(AudioClip clip)
    {
        _BGMClip = clip;
        AudioManager.instance.PlayBGM(clip);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SoundPanel.gameObject.activeInHierarchy == false)
        {
            SoundPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
