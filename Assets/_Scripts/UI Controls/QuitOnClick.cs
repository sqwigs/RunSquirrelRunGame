using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {

    /// <summary>
    /// Quits the current Game Application
    /// </summary>
	public void QuitGame ()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
    }
}
