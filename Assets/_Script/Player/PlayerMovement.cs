using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isFollowingCursor;
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        isFollowingCursor = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isFollowingCursor && Time.timeScale > 0f)
        {
            FollowCursor();
        }
    }
    void FollowCursor()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
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
    public void ToggleMovement(bool value)
    {
        isFollowingCursor = value;
    }
}
