using UnityEngine;
using System.Collections.Generic;

public class bullet : MonoBehaviour
{
    public Transform target;


    float step;
    public float speed = 100;
 
    public int radius;

    void Start()
    {
    }
    void Update()
    {
        if (target == null)
        {

            Destroy(gameObject);
            return;
        }
        step = speed * Time.deltaTime;
      

        gameObject.transform.LookAt(target.transform);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, step);




    }
    void Hit()
    {
     

        if (radius == 0)
        {
            target.GetComponent<dest>().hp-=gameObject.GetComponentInParent<fire>().damage;
        }
        else
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider c in cols)
            {
                dest e = c.GetComponent<dest>();
                if (e != null)
                {
                    e.GetComponent<dest>().hp -= gameObject.GetComponentInParent<fire>().damage;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Mobs"|| other.gameObject.tag == "Boss")
        {
            Hit();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
         // Destroy(gameObject);
        }
    }
}



