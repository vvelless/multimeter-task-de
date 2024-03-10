using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    public GameObject objectToHighlight;
    
    /// <summary>
    /// Change object color when mouse is above it.
    /// </summary>
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = new Color(0.99f, 1f, 0.34f);
    }

    /// <summary>
    /// Change object color back when mouse isn't above it.
    /// </summary>
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.clear;
    }
}