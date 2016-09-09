using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {
    public ColorBlock z = new ColorBlock();

    void Start ()
    {
        z.colorMultiplier = 1;
        StartCoroutine(ColorChange(gameObject.GetComponent<Button>(), Color.yellow, Color.red, 5f));
        
    }
	

	void Update ()
    {
	    
	}
    IEnumerator ColorChange(Button random, Color startColor, Color finishColor, float duration)
    {
        bool end = false;
        float progress = 0;
        float increment = 0.5f / duration;
        while (true)
        {


            while (z.normalColor != finishColor && !end)
            {
                z.normalColor = Color.Lerp(startColor, finishColor, progress);
                z.highlightedColor = z.normalColor;
                random.colors = z;
                progress += increment;
                yield return new WaitForSeconds(0.10f);

            }
            end = true;

            while (z.normalColor != startColor && end)
            {

                z.normalColor = Color.Lerp(finishColor, startColor, 1 - progress);
                z.highlightedColor = z.normalColor;
                random.colors = z;
                progress -= increment;
                yield return new WaitForSeconds(0.10f);
            }






            end = false;
        }

    }
}
