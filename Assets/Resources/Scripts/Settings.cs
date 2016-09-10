using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
public class Settings : MonoBehaviour
{
    public SettingsSC k;
    public void Save()
    {
        Debug.Log("here");
        k = new SettingsSC();
        Debug.Log(Application.persistentDataPath);
        
        var xml = new XmlSerializer(typeof(SettingsSC));
        if (File.Exists(Application.persistentDataPath + "/settings.ini"))
        {
            FileStream f = new FileStream(Application.persistentDataPath + "/settings.ini", FileMode.Open);
            k =(SettingsSC) xml.Deserialize(f);
          
             FindObjectOfType<AudioSource>().volume = k.volume;

            FindObjectOfType<AudioSource>().mute = k.mute;
            f.Close();
        }

        else {
            var f = new FileStream(Application.persistentDataPath + "/settings.ini",FileMode.Create);
         
            xml.Serialize(f, k);

            f.Close();

        }
      




     
       
    }
}





[System.Serializable]
public class SettingsSC : ScriptableObject
{
  public  float volume;
  public  bool mute;
  

}
