using UnityEngine;
using System.Collections;

public class Poison : MonoBehaviour
{


    void Start()
    {
      


    }
    void Update()
    {

    }
    public IEnumerator PoisonEffect(GameObject target, float duration)
    {
        if (target.GetComponent<dest>().effects.Contains("Poison"))
        {
            Destroy(gameObject);
            yield break;
        }

        target.GetComponent<dest>().effects += "Poison ";
        target.GetComponent<NavMeshAgent>().speed *= 0.5f;
        yield return new WaitForSeconds(duration);

        target.GetComponent<dest>().effects = target.GetComponent<dest>().effects.Replace("Poison ", "");
        target.GetComponent<NavMeshAgent>().speed *= 2f;

        Destroy(gameObject);

    }
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(PoisonEffect(GetComponent<bullet>().target.gameObject, 2));
    }
}
