using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour {

    public string levelName;

    public Button start;

    //--------------------------------------
    void Start()
    {
        start.onClick.AddListener(load);

#if UNITY_WEBGL
        Debug.Log("Webgl");
        Camera.main.orthographicSize = 185;

#endif

#if UNITY_STANDALONE_WIN
     Debug.Log("Win");
     Camera.main.orthographicSize = 120;

#endif
    }
    void Update ()
    {
	
	}

    public  void load()
    {
        SceneManager.LoadScene(levelName);
    }

}
