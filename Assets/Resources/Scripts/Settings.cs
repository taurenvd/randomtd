using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;
    public InputField playerName;
    public void Save()
    {
        var k = ScriptableObject.CreateInstance<SettingsSC>();
        k.volume = slider.value;
        k.mute = toggle.isOn;
        k.playerName = playerName.text;

        Debug.Log(playerName.text);
        var f = File.Create(Application.dataPath+"/settings.ini");
        Debug.Log(Application.dataPath);
         var xml = new XmlSerializer(typeof(SettingsSC));
        xml.Serialize(f, k);
        f.Close();
    }
    public void Load()
    {
        var k = ScriptableObject.CreateInstance<SettingsSC>();

        var xml = new XmlSerializer(typeof(SettingsSC));
        if (File.Exists(Application.dataPath + "/settings.ini"))
        {

            FileStream f = new FileStream(Application.dataPath + "/settings.ini", FileMode.Open);
            k = (SettingsSC)xml.Deserialize(f);
            if (gameObject.GetComponent<UI>()!=null)
            {
                gameObject.GetComponent<UI>().playerName = k.playerName;
                Debug.Log(k.playerName);
            }        
            playerName.text =k.playerName;
            slider.value = k.volume;
            toggle.isOn = k.mute;
            f.Close();
        }
        else
        {
            playerName.text = "";
            slider.value = 0.5f;
            toggle.isOn = false;
        }
    }
    public void SetActiveRec(GameObject go) { go.SetActive(!go.activeSelf); }
    void Start()
    {
  
        Load();
    }
}
[System.Serializable]
public class SettingsSC : ScriptableObject
{
  public  float volume;
  public  bool mute;
  public string playerName;
 
}
