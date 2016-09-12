using UnityEngine;
using System.Collections;
using System;

public class Upgrade : MonoBehaviour {

	//TODO: make 'main att' for accurate upgrade;

	public void LevelUp ()
    {
     
       GetComponent<fire>().level++;
       GetComponent<fire>().range += 2 * GetComponent<fire>().level;
       GetComponent<fire>().damage+= (int)(0.2 * GetComponent<fire>().damage);
       GetComponent<fire>().attackSpeed =(float)Math.Round((0.9f * GetComponent<fire>().attackSpeed),2);
        
    }
    public void LevelUp(int level)
    {
        for (int i = 0; i < level; i++)
        {

        
        GetComponent<fire>().level++;
        GetComponent<fire>().range += 2 * GetComponent<fire>().level;
        GetComponent<fire>().damage += (int)(0.2 * GetComponent<fire>().damage);
        GetComponent<fire>().attackSpeed = (float)Math.Round((0.9f * GetComponent<fire>().attackSpeed), 2);
        }
    }

}
