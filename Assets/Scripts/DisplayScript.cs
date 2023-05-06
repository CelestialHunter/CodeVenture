using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public ByteScript byteObj;
    public TMP_Text valueText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        valueText.text = byteObj.getValueUnsigned().ToString();
    }
}
