using UnityEngine;
using System.Collections;

public class CursorControl : MonoBehaviour {

    public float timeCursorOnScreen;
    private Timer time;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // locks cursor to middle of screen

        time = new Timer(timeCursorOnScreen);
    }


    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // locks cursor to middle of screen
        }
        else if (time.TimeLeft < 0)
        {
            Cursor.visible = false;
            time.ResetTimer();
        }

        time.UpdateTimer();
    }
}
