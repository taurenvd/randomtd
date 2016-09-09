using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class fire : MonoBehaviour
{
    Transform towerTopPrefab;

    public GameObject nearestEnemy = null;
    GameObject target;
    GameObject arrow_clone;

    public List<GameObject> enemies;

    public float attackSpeed = 1f;
    public float range;

    public int damage = 10;
    public int level;
    public int towerType;

    public string info;

    private bool shoot;

    Vector3 _dir;
 


    void Start()
    {
       towerTopPrefab=gameObject.GetComponentInChildren<Animator>().transform;
       level = 1;
        StartCoroutine(Fire(attackSpeed));
        StartCoroutine(GetNearestEnemy());
        gameObject.name= gameObject.name.Substring(0,gameObject.name.Length-7);
    }

    void Update()
    {
        if (nearestEnemy!=null&&Vector3.Distance(transform.position, nearestEnemy.transform.position) > range)
        {
            nearestEnemy = null;
        }
        info =gameObject.name+" Tower"+"\n"+ "DMG: " + damage + "=>" +(int)( 1.2f * damage) + "  AS: " +attackSpeed + "=>" + Math.Round( (0.9f * attackSpeed),2) + "\nRange: " + range+"=>"+ (int)(range+2*level);
        if (nearestEnemy != null&&nearestEnemy.GetComponent<dest>().hp>0)
        {
           
            towerTopPrefab.LookAt(nearestEnemy.transform);
           towerTopPrefab.transform.eulerAngles = new Vector3(0,towerTopPrefab.transform.eulerAngles.y,0);
       
            if (shoot)
            {
                arrow_clone = (GameObject)Instantiate(gameObject.GetComponentInChildren<bullet>(true).gameObject, towerTopPrefab.position, towerTopPrefab.transform.rotation);
                arrow_clone.SetActive(true);
                arrow_clone.transform.SetParent(gameObject.transform);
                arrow_clone.GetComponent<bullet>().target =nearestEnemy.transform;
                shoot = false;
            }
        }
    }

    IEnumerator Fire(float Delay)
    {

        while (true)
        {
            shoot = true;
            yield return new WaitForSeconds(Delay);
        }
    }

    IEnumerator GetNearestEnemy()
    {
        while (true)
        {
           var enemies=new List<GameObject>(GameObject.FindGameObjectsWithTag("Mobs"));
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Boss"));
             
            
            float min = Mathf.Infinity;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.Distance(transform.position,enemies[i].transform.position)<range&& Vector3.Distance(transform.position, enemies[i].transform.position)<min)
                {     
                    nearestEnemy = enemies[i];
                    min = Vector3.Distance(transform.position, enemies[i].transform.position);
                }
             
            } 
            yield return new WaitForSeconds(0.1f);
           
        }
    }
}