using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BitScript : MonoBehaviour, Interactable
{

    public string interactMessage = "Switch value";
    public bool isInteractable = true;
    public bool value { get; private set; }
    public TMP_Text valueText;

    string Interactable.interactMessage
    {
        get => (isInteractable ? interactMessage : "");
    }
    bool Interactable.isInteractable
    {
        get => isInteractable;
    }

    public void interact()
    {
        if (!isInteractable) return;
        setValue(!value);
    }

    public void highlight()
    {
        if (!isInteractable) return;
        transform.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Highlight");
    }

    public void unhighlight()
    {
        if (!isInteractable) return;
        transform.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        value = false;
        valueText.text = "0";

    }

    public int getIntValue()
    {
        return value ? 1 : 0;
    }
    
    public void setValue(bool val)
    {
        value = val;
        valueText.text = value ? "1" : "0";
    }

    public void setValue(int val)
    {
        value = val == 1;
        valueText.text = value ? "1" : "0";
    }
}
