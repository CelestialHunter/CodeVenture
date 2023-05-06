using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BinaryBitwiseScript : MonoBehaviour, Interactable
{
    public BitScript input1;
    public BitScript input2;
    public BitScript output;
    public TMP_Text operationDisplay;

    delegate bool BinaryBitwiseOperation(bool v1, bool v2);

    BinaryBitwiseOperation[] operations = {
        (bool v1, bool v2) => v1 && v2, // AND
        (bool v1, bool v2) => v1 || v2, // OR
        (bool v1, bool v2) => v1 ^ v2 // XOR
    };
    public enum BinaryOperation
    {
        AND,
        OR,
        XOR,
    };

    public static string[] operationNames = {
        "AND",
        "OR",
        "XOR"
    };

    public BinaryOperation selectedOperation;

    public string interactMessage => "Change operation";

    public bool isInteractable => true;

    void Start()
    {
        selectedOperation = BinaryOperation.AND;
        operationDisplay.text = operationNames[(int)selectedOperation];
    }

    // Update is called once per frame
    void Update()
    {
        if (input1 == null || input2 == null || output == null) return;

        output.setValue(operations[((int)selectedOperation)](input1.value, input2.value));
    }

    public void interact()
    {
        //switch (selectedOperation)
        //{
        //    case BinaryOperation.AND:
        //        selectedOperation = BinaryOperation.OR;
        //        break;
        //    case BinaryOperation.OR:
        //        selectedOperation = BinaryOperation.XOR;
        //        break;
        //    case BinaryOperation.XOR:
        //        selectedOperation = BinaryOperation.AND;
        //        break;
        //}        

        selectedOperation = (BinaryOperation)(((int)selectedOperation + 1) % 3);
        operationDisplay.text = operationNames[(int)selectedOperation];
        
    }

    public void highlight()
    {
         // todo
    }

    public void unhighlight()
    {
        // todo
    }
}
