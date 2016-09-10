using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spot : MonoBehaviour {

    public GameObject enemy;
    public List<GameObject> enemiesL;
    public GameObject ogrePref;
    GameObject _bossClone;
    public GameObject hpbar;

    public int nCreeps=1;
    public int BossDiv = 3;

    public Transform green;

    public float bossConstHp=0;
    public float currentHp;
    public float spawnTime = 10f;
    public float currentTime;

    public  bool boss;
    //--------------------------------------------------
    void Start ()
    {
        currentTime = spawnTime;
        hpbar.SetActive(false);
        Instantiate(enemy, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
         boss = true;
    }
	
	void Update ()
    {
        //spawner;
        if (currentTime <= 0&& (GameObject.FindObjectOfType<UI>().creepsOnWave>nCreeps))
        {
            if ((GameObject.FindObjectOfType<UI>().waveCount%BossDiv)==0&&boss==true)
            {
                _bossClone=(GameObject)Instantiate(ogrePref, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
                FindObjectOfType<Audio>().audio.volume = 1f;
                FindObjectOfType<Audio>().PlaySound(FindObjectOfType<Audio>().boss);



                boss = false;
             
                
            }
            else Instantiate(enemy, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
            currentTime= spawnTime;
            nCreeps++;        }
        else currentTime -= Time.deltaTime;
        //boss hpbar;
        if (_bossClone != null&& _bossClone.GetComponent<dest>().hp>0)
        {
            hpbar.SetActive(true);
            currentHp = (bossConstHp - _bossClone.gameObject.GetComponent<dest>().hp) / bossConstHp;
            if ((1-currentHp)>0&& (1 - currentHp) <1)
            {
                green.localScale = new Vector3(1 - currentHp, green.localScale.y, green.localScale.z);
            }
       
        }
        else
        {
            green.localScale = new Vector3(1, green.localScale.y, green.localScale.z);
            hpbar.SetActive(false);
            if (!FindObjectOfType<Audio>().audio.isPlaying&&!FindObjectOfType<UI>().wait)
            {
                FindObjectOfType<Audio>().audio.Stop();
                FindObjectOfType<Audio>().PlaySound(FindObjectOfType<Audio>().main);
                Debug.Log(FindObjectOfType<Audio>().audio.clip);
            }


           boss = true;
        }

    }
    
}
