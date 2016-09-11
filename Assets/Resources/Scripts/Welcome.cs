using UnityEngine;
using System.Collections;

public class Welcome : MonoBehaviour {
    public Animator ogre;

	void Start ()
    {
        FindObjectOfType<Settings>().Load();
        // ogre.Play("attack_2", 0, 0.15f);
        ogre.SetBool("Attack",true);
	}

}
