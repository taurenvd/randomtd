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
        DontDestroyOnLoad(this);
        var k = ScriptableObject.CreateInstance<SettingsSC>();
        k.volume = slider.value;
        k.mute = toggle.isOn;
        k.playerName = playerName.text;
        var f = File.Create(Application.persistentDataPath + "/settings.ini");
        var xml = new XmlSerializer(typeof(SettingsSC));
        xml.Serialize(f, k);
        f.Close();
    }
    public void Load()
    {



        var k = ScriptableObject.CreateInstance<SettingsSC>();

        var xml = new XmlSerializer(typeof(SettingsSC));
        if (File.Exists(Application.persistentDataPath + "/settings.ini"))
        {

            FileStream f = new FileStream(Application.persistentDataPath + "/settings.ini", FileMode.Open);
            k = (SettingsSC)xml.Deserialize(f);
            playerName.text = k.playerName;
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
}
[System.Serializable]
public class SettingsSC : ScriptableObject
{
  public  float volume;
  public  bool mute;
  public string playerName;
}
