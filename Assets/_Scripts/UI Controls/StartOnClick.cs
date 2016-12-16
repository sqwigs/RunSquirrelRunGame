using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartOnClick : MonoBehaviour {

    public float delay = 1f;


	public void LoadByIndex (int sceneIndex)
    {
        StartCoroutine(LoadAfterDelay(delay,sceneIndex));
    }

    public IEnumerator LoadAfterDelay(float time, int index)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(index);
        yield return null;
    }
}
