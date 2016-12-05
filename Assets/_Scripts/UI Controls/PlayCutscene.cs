using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))] // makes script REQUIRE audio rather than having audio component in interface

public class PlayCutscene : MonoBehaviour {

    public MovieTexture movie;
    public int transitionSceneNum;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();
    }
	
    /// <summary>
    /// Check if movie is playing and then transition to next scene. 
    /// </summary>
   void Update ()
    {
        if (!movie.isPlaying || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)
                                || Input.GetKeyDown(KeyCode.Joystick1Button7)
                                || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            SceneManager.LoadScene(transitionSceneNum);
        }
    }
}
