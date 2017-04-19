using UnityEngine;
using System.Collections;

public class MouseHover : MonoBehaviour
{
    void Start()
    {
        //renderer.material.color = Color.black;
        GetComponent<Renderer>().material.color = Color.black;
    }

    void OnMouseEnter()
    {
        //renderer.material.color = Color.red;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        //renderer.material.color = Color.black;
        GetComponent<Renderer>().material.color = Color.black;
    }
}
