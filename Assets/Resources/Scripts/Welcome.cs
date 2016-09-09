using UnityEngine;
using System.Collections;

public class Welcome : MonoBehaviour {
    public Animator ogre;

	void Start () {
       // ogre.Play("attack_2", 0, 0.15f);
        ogre.SetBool("Attack",true);
	}


    void Update()
    {
        if (ogre.playbackTime ==0.2f)
        {
            ogre.Play("attack_2", 0, 0.15f);
        }
    } 
}
