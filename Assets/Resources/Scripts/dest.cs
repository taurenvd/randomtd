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
            GameObject.FindObjectOfType<Spot>().bossConstHp = hp;
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
            }
            if (gameObject.tag=="Mobs")
            {
                Reward();
            }
            GameObject.FindObjectOfType<UI>().deadCreeps++;
        }
        
           info =gameObject.name+"\n" +"HP= " + hp.ToString() + "\nMS= " + agent.speed;
      

    }
    void OnTriggerEnter(Collider other)
    {


   
        if (other.gameObject.name == "Destination")
        {

            Destroy(gameObject);
            if (gameObject.name == "Boss(Clone)")
            {
                GameObject.FindObjectOfType<UI>().lives -= 10;
               

            }
            else GameObject.FindObjectOfType<UI>().lives--;
            GameObject.FindObjectOfType<UI>().deadCreeps++;
        }
    }

    void Reward()
    {
            GameObject.FindObjectOfType<UI>().gold += 5 * (int)GameObject.FindObjectOfType<UI>().waveCount;
            Destroy(gameObject); 
    }

    IEnumerator DeathClip(GameObject target,float aniTime)
    {
        target.tag = "Untagged";
        agent.Stop();
        GameObject.FindObjectOfType<UI>().gold += 50 * (int)GameObject.FindObjectOfType<UI>().waveCount;
        target.GetComponent<NavMeshAgent>().speed = 0;
        target.GetComponent<Animator>().Play("Death");
       yield return new WaitForSeconds(aniTime);
      Destroy(target);

    }
}
