using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

    public string levelName;

    //--------------------------------------
 void Start() { 

#if UNITY_WEBGL
        Debug.Log("Webgl");
        Camera.main.orthographicSize = 185;

#endif

#if UNITY_STANDALONE_WIN
     Debug.Log("Win");
     Camera.main.orthographicSize = 120;

#endif
    }
    public  void load(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void merge(string levelName)
    {
        SceneManager.MergeScenes(SceneManager.GetActiveScene(),SceneManager.GetSceneByName(levelName));
    }

}
