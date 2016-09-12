using UnityEngine;
using System.Collections;

public class dest : MonoBehaviour
{
    Animator animator;

    public NavMeshAgent agent;

    public GameObject Finish;

    public int hp = 10;
    public int destroyedCreepsCount=0;
  
    public string effects;
    public string info;

    public  bool death = false;
//--------------------------------------------------------------
    void Start()
    {
        if (agent != null)
        {
            agent.destination = Finish.transform.position;
        }
        if (gameObject.GetComponent<Animator>()!=null)
        {
            animator=gameObject.GetComponent<Animator>();
            animator.SetBool("Walking", true);
            if (gameObject.tag=="Boss")
            {
                GameObject.FindObjectOfType<Spot>().bossConstHp = hp;
            }
            
        }
        effects = "Active effects:\n";
        gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);
    }

    void Update()
    {
        if (hp <= 0)
        {

            if (gameObject.GetComponent<Animator>() != null && !death)
            {
                StartCoroutine(DeathClip(gameObject, 2));
                death = true;
                Reward();
                GameObject.FindObjectOfType<UI>().deadCreeps++;
            }      

        }

        info =gameObject.name+"\n" +"HP= " + hp.ToString() + "\nMS= " + agent.speed;    

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
            if (gameObject.tag == "Boss")
            {
                GameObject.FindObjectOfType<UI>().lives -= 10;
              

            }
            else GameObject.FindObjectOfType<UI>().lives--;
            GameObject.FindObjectOfType<UI>().deadCreeps++;
        }
   

    }

    void Reward()
    {
        if (gameObject.tag == "Boss")
        {
            GameObject.FindObjectOfType<UI>().gold += 50 * (int)GameObject.FindObjectOfType<UI>().waveCount;
            FindObjectOfType<UI>().score += 5;
        }
        else
        {
            GameObject.FindObjectOfType<UI>().gold += 5 * (int)GameObject.FindObjectOfType<UI>().waveCount;
            FindObjectOfType<UI>().score += 1;
        }

    }

    IEnumerator DeathClip(GameObject target,float aniTime)
    {
        target.tag = "Untagged";
        agent.Stop();
        target.GetComponent<NavMeshAgent>().speed = 0;
        target.GetComponent<Animator>().Play("Death");
       yield return new WaitForSeconds(aniTime);
        Destroy(target);

    }
}
