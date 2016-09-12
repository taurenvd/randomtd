using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;
using System;

public class SaveLoad : MonoBehaviour
{

    public Button save;
    public Button load;



    void Start()
    {
        save.onClick.AddListener(Save);
        load.onClick.AddListener(Load);

     
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            FindObjectOfType<SaveLoad>().Save();
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
          
            FindObjectOfType<SaveLoad>().Load();
        }
    }

    public  void Save()
    {
        var file = File.Create(Application.dataPath + "/savedGames.rsave");
       
        var xs = new XmlSerializer(typeof(SerClass));
        var sC = ScriptableObject.CreateInstance<SerClass>();

        sC.gold = FindObjectOfType<UI>().gold;
        sC.lives = FindObjectOfType<UI>().lives;
        sC.wave = FindObjectOfType<UI>().waveCount;
        sC.randomTimer = FindObjectOfType<UI>().randomTimer; 
        sC.mCam=Camera.main.transform.position;
        sC.towersPos = FindObjectOfType<UI>().towerPosL;
        sC.towerName = FindObjectOfType<UI>().towerTypesL;
        sC.platforms = FindObjectOfType<UI>().platformNameL;
        sC.parent = FindObjectOfType<UI>().parent;
        sC.score = FindObjectOfType<UI>().score;
        sC.playerName=FindObjectOfType<UI>().playerName;
        foreach (var item in FindObjectsOfType<fire>())
        {
           
            Debug.Log(item.level);
            sC.levels.Add( item.level);
        }

        xs.Serialize(file,sC);

        Destroy(sC);
        file.Close();
  
    }
    public  void Load()
    {
        if (File.Exists(Application.dataPath + "/savedGames.rsave"))
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Buildings"))
            {
                Destroy(item);
            }
            
            var sc2 = ScriptableObject.CreateInstance<SerClass>();
            var file = File.Open(Application.dataPath + "/savedGames.rsave", FileMode.Open);
            var xml = new XmlSerializer(typeof(SerClass));
            sc2=(SerClass)xml.Deserialize(file);
           FindObjectOfType<UI>().gold = sc2.gold;
            FindObjectOfType<UI>().lives = sc2.lives;
            FindObjectOfType<UI>().waveCount = sc2.wave;
            FindObjectOfType<UI>().randomTimer = sc2.randomTimer;
            Camera.main.transform.position = (Vector3)sc2.mCam;
            FindObjectOfType<UI>().score = sc2.score;
            FindObjectOfType<UI>().towerPosL=(List<Vector3>)sc2.towersPos;
            FindObjectOfType<UI>().towerTypesL = (List<string>)sc2.towerName;
            FindObjectOfType<UI>().playerName = sc2.playerName;
            for (int i = 0; i < sc2.towerName.Count; i++)
            {
               
                var buf = (GameObject)Instantiate(Resources.Load("Prefabs/TowerTypes/" + sc2.towerName[i]), (Vector3)sc2.towersPos[i], new Quaternion(0,0,0,0) );
                buf.GetComponent<Upgrade>().LevelUp(sc2.levels[i]);
                buf.transform.SetParent(GameObject.Find(sc2.parent[i]).transform);
                FindObjectOfType<UI>().platformsL.Remove(GameObject.Find(sc2.parent[i]));
                //platform remove from 

            }
            DestroyObject(sc2);
        
            file.Close();
            FindObjectOfType<SceneLoader>().merge("TowerDefence");
        }
        Time.timeScale = 1f;
        
    }
}

[System.Serializable]
public class SerClass: UnityEngine.ScriptableObject
{
    public int gold;
    public int lives;
    public int wave;
    public int score;

    public UnityEngine.Vector3 mCam;

    public float randomTimer;

    public List<Vector3> towersPos;
    public List<string> towerName;
    public List<string> parent;
    public List<int> levels=new List<int>();
    public List<string> platforms;
    public string playerName;

}

