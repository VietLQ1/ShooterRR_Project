using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundPanel : SingletonMonobehavior<SoundPanel>
{
    [SerializeField]
    private Scrollbar Soundbar;
    public void Volume()
    {
        AudioManager.instance.ChangeVolume(Soundbar.value);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        if(PowerUpPanel.instance != null && !PowerUpPanel.instance.gameObject.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Gameplay"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        
    }
}
