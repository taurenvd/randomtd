using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {
    

    void Start()
    {
      
    }

    public IEnumerator StunEffect(GameObject target, float duration, float chances)
    {
        var rand = (int)Random.Range(0, chances);
        if (target.GetComponent<dest>().effects.Contains("Stun") || rand != 0)
        {
            Destroy(gameObject);
            yield break;
        }
        target.GetComponent<dest>().effects += "Stun ";

        if (target.tag == "Boss")
        {

            target.GetComponent<Animator>().SetBool("Walking", false);
            target.GetComponent<Animator>().SetBool("Stun", true);
        }
        target.GetComponent<NavMeshAgent>().Stop();
        yield return new WaitForSeconds(duration);
        if (target.tag == "Boss")
        {
            target.GetComponent<Animator>().SetBool("Walking", true);
            target.GetComponent<Animator>().SetBool("Stun", false);
        }
        target.GetComponent<dest>().effects = target.GetComponent<dest>().effects.Replace("Stun ", "");
        target.GetComponent<NavMeshAgent>().Resume();
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StunEffect(GetComponent<bullet>().target.gameObject, 1, 5));
    }
}
