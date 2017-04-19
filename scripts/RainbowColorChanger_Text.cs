using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RainbowColorChanger_Text : MonoBehaviour {
    public Color modifiedColor = new Color(1.0f, 0f, 0f);
    public int colorTransitionDelay = 10;
    public Text title;

	// Use this for initialization
	void Start () {
        //modifiedColor = new Color(1.0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        //yellow -> green
        if (modifiedColor.r > 0 && modifiedColor.g >= 1 && modifiedColor.b <= 0)
            modifiedColor.r -= Time.timeScale * 1.0f / colorTransitionDelay;

        //red -> yellow
        if (modifiedColor.r >= 1 && modifiedColor.g <= 1 && modifiedColor.b <= 0)
            modifiedColor.g += Time.timeScale * 1.0f / colorTransitionDelay;

        //teal -> blue
        if (modifiedColor.r <= 0 && modifiedColor.g > 0 && modifiedColor.b >= 1)
            modifiedColor.g -= Time.timeScale * 1.0f / colorTransitionDelay;

        //green -> teal
        if (modifiedColor.r <= 0 && modifiedColor.g >= 1 && modifiedColor.b < 1)
            modifiedColor.b += Time.timeScale * 1.0f / colorTransitionDelay;

        //purple -> red
        if (modifiedColor.r >= 1 && modifiedColor.g <= 0 && modifiedColor.b > 0)
            modifiedColor.b -= Time.timeScale * 1.0f / colorTransitionDelay;

        //blue -> purple
        if (modifiedColor.r <= 1 && modifiedColor.g <= 0 && modifiedColor.b >= 1)
            modifiedColor.r += Time.timeScale * 1.0f / colorTransitionDelay;

        //make sure all values stay between 0 and 1
        if (modifiedColor.r > 1)
            modifiedColor.r = 1;
        else if (modifiedColor.r < 0)
            modifiedColor.r = 0;
        if (modifiedColor.g > 1)
            modifiedColor.g = 1;
        else if (modifiedColor.g < 0)
            modifiedColor.g = 0;
        if (modifiedColor.b > 1)
            modifiedColor.b = 1;
        else if (modifiedColor.b < 0)
            modifiedColor.b = 0; 

        title.color = new Color(modifiedColor.r, modifiedColor.g, modifiedColor.b);
    }
}
