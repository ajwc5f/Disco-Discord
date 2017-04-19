using UnityEngine;
using System.Collections;

public class RainbowColorChanger : MonoBehaviour {
    public Color modifiedColor = new Color(1.0f, 0f, 0f, 1f);
    public int colorTransitionDelay = 20;

	// Use this for initialization
	void Start () {
        //modifiedColor = new Color(1.0f, 0f, 0f);
	}
	
	void FixedUpdate ()
    {//Time.timeScale *
        //yellow -> green
        if (modifiedColor.r > 0 && modifiedColor.g >= 1 && modifiedColor.b <= 0)
            modifiedColor.r -= 1.0f / colorTransitionDelay;

        //red -> yellow
        if (modifiedColor.r >= 1 && modifiedColor.g <= 1 && modifiedColor.b <= 0)
            modifiedColor.g += 1.0f / colorTransitionDelay;

        //teal -> blue
        if (modifiedColor.r <= 0 && modifiedColor.g > 0 && modifiedColor.b >= 1)
            modifiedColor.g -= 1.0f / colorTransitionDelay;

        //green -> teal
        if (modifiedColor.r <= 0 && modifiedColor.g >= 1 && modifiedColor.b < 1)
            modifiedColor.b += 1.0f / colorTransitionDelay;

        //purple -> red
        if (modifiedColor.r >= 1 && modifiedColor.g <= 0 && modifiedColor.b > 0)
            modifiedColor.b -= 1.0f / colorTransitionDelay;

        //blue -> purple
        if (modifiedColor.r <= 1 && modifiedColor.g <= 0 && modifiedColor.b >= 1)
            modifiedColor.r += 1.0f / colorTransitionDelay;

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

        GetComponent<SpriteRenderer>().color = modifiedColor;
    }
}
