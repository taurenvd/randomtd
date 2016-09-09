using UnityEngine;
using System.Collections;

public class Haste : MonoBehaviour {

   public GameObject part;

   public float cooldown;
   public float duration;
   public float speedMult=5;

	void Start ()
    {
        StartCoroutine(CorName(part, speedMult));
    }
    IEnumerator CorName(GameObject part,float speedMul)
    {
        while (GetComponent<dest>().death==false)
        {
            yield return new WaitForSeconds(3f);

            GetComponent<NavMeshAgent>().speed *= speedMul;
            gameObject.GetComponent<dest>().effects+= "Haste ";
            if (gameObject.GetComponent<Animator>()!=null)
            {
             
               gameObject.GetComponent<Animator>().SetBool("Run", true);
                
            }
            part.SetActive(true);
           
                yield return new WaitForSeconds(6f);
            GetComponent<NavMeshAgent>().speed /= speedMul;
            part.SetActive(false);
            if (gameObject.GetComponent<Animator>() != null)
            {
                gameObject.GetComponent<Animator>().SetBool("Run", false);
                gameObject.GetComponent<dest>().effects = gameObject.GetComponent<dest>().effects.Replace("Haste", "");
            }

        }
    }
}
