using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ByteScript : MonoBehaviour
{
    public BitScript[] bits;

    bool[] value = new bool[8];

    void Start()
    {
        foreach (BitScript bit in bits)
        {
            bit.setValue(0);
        }

        value = Enumerable.Repeat(false, 8).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i++)
            value[i] = bits[i].value;
    }

    public int getValueUnsigned()
    {
        int val = 0;
        for (int i = 0; i < 8; i++)
        {
            val += value[i] ? (int)Mathf.Pow(2, i) : 0;
        }
        return val;
    }

    public int getValueSigned()
    {
        int val = 0;
        for (int i = 0; i < 8; i++)
        {
            val += value[i] ? (int)Mathf.Pow(2, i) : 0;
        }
        if (value[7])
        {
            val -= (int)Mathf.Pow(2, 8);
        }
        return val;
    }
}
