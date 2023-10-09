using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPanel : SingletonMonobehavior<PowerUpPanel>
{
    [SerializeField]
    private PlayerMovement PlayerCursor;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        UnlockCursor();
        PlayerCursor.ToggleMovement(false);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
        LockCursor();
        PlayerCursor.ToggleMovement(true);
    }
    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
