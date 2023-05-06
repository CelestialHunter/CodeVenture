using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IDEScript : MonoBehaviour
{

    public TMP_InputField codeText;
    LaptopScript currentLaptop;

    public TMP_Text resultText;
    public GameObject resultPanel;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void setEnv(LaptopScript laptop)
    {
        currentLaptop = laptop;
        laptop.loadCode();
        //codeText.SetText(laptop.getCode());
        codeText.text = laptop.getCode();
    }

    public void saveCode()
    {
        currentLaptop.setCode(codeText.text);
        currentLaptop.saveCode();
    }

    public void compileCode()
    {
        saveCode();
        currentLaptop.compileCode();
    }
    
    public void runCode()
    {
        resultText.text = currentLaptop.runCode();
        resultPanel.SetActive(!string.IsNullOrEmpty(resultText.text));
    }
}
