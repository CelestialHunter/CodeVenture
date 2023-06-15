using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ByteScript : MonoBehaviour
{
    public enum ByteType
    {
        Unsigned_Byte,
        SignMagnitude,
        OneComplement,
        Signed_Byte, // C2
        Unsigned_Short,
        Signed_Short,
        Unsigned_Int,
        Signed_Int,
        Float,
        Double //,
        //IBM
    };

    static public int[] byteLengths = { 8, 8, 8, 8, 16, 16, 32, 32, 32, 64 }; //, 32 };

    public enum InputMode
    {
        Sandbox,
        TestBinary,
        TestDecimal
    };


    public BitScript[] bits;
    bool[] value;


    private ByteType _type;
    public ByteType type = ByteType.Unsigned_Byte;
    private InputMode _mode;
    public InputMode mode = InputMode.Sandbox;
    public Material editableMaterial;
    public Material readOnlyMaterial;

    public int bitNo = byteLengths[0];

    void Start()
    {
        bitNo = byteLengths[(int)type];
        if (bits.Length != bitNo)
        {
            Debug.LogError("ByteScript: Number of bits does not match type");
            return;
        }

        foreach (BitScript bit in bits)
        {
            bit.setValue(0);
        }

        value = Enumerable.Repeat(false, bitNo).ToArray();

        _type = type;
        _mode = mode;

        setMode();
    }

    // Update is called once per frame
    void Update()
    {
        //if (mode == InputMode.Sandbox)
        for (int i = 0; i < bitNo; i++)
            value[i] = bits[i].value;

        if(_type != type)
        {
            bitNo = byteLengths[(int)type];
            if (bits.Length != bitNo)
            {
                Debug.LogError("ByteScript: Number of bits does not match type");
                type = _type;
                bitNo = byteLengths[(int)type];
            }

            _type = type;
        }
        
        if(_mode != mode)
        {
            setMode();
            _mode = mode;
        }
    }

    public int getValueUnsigned_int()
    {
        int val = 0;
        for (int i = 0; i < bitNo; i++)
        {
            val |= value[i] ? (1<<i) : 0;
        }
        return val;
    }

    public int getValueSignMagnitude()
    {
        int val = 0;
        for (int i = 0; i < bitNo-1; i++)
        {
            val |= value[i] ? (1 << i) : 0;
        }

        if (value[bitNo - 1]) val = -val;
        
        return val;
    }

    public int getValueOneComplement()
    {
        int val = 0;
        for (int i = 0; i < bitNo - 1; i++)
        {
            if(!value[bitNo - 1])
                val |= value[i] ? (1 << i) : 0;
            else
                val |= value[i] ? 0 : (1 << i);
        }

        if (value[bitNo - 1]) val = -val;

        return val;
    }

    public int getValueSigned_int()
    {
        int val = 0;
        for (int i = 0; i < bitNo - 1; i++)
        {
            if (!value[bitNo - 1])
                val |= value[i] ? (1 << i) : 0;
            else
                val |= value[i] ? 0 : (1 << i);
        }

        

        if (value[bitNo - 1]) val = -(val+1);

        
        return val;
    }

    public float getValueFloat()
    {
        int val = 0;
        for (int i = 0; i < bitNo; i++)
        {
            val |= value[i] ? (1 << i) : 0;
        }

        return System.BitConverter.ToSingle(System.BitConverter.GetBytes(val), 0);
    }

    public double getValueDouble()
    {
        ulong val = 0;
        for (int i = 0; i < bitNo; i++)
        {
            val |= value[i] ? ((ulong)1 << i) : 0;
        }

        return System.BitConverter.ToDouble(System.BitConverter.GetBytes(val), 0);
    }

    //public double getValueIBM()
    //{
    //    ulong val = 0;
    //    for (int i = 0; i < byteNo; i++)
    //    {
    //        val |= value[i] ? ((ulong)1 << i) : 0;
    //    }

    //    return System.BitConverter.ToDouble(System.BitConverter.GetBytes(val), 0);
    //}


    public void setValue(byte[] bytes)
    {
        for (int i = 0; i < bitNo; i++)
        {
            value[i] = (bytes[i / 8] & (1 << (i % 8))) != 0;
            bits[i].setValue(value[i]);
        }
    }

    public void setValue_int(int val)
    {
        for (int i = 0; i < bitNo; i++)
        {
            bits[i].setValue((val & (1 << i)) != 0);
        }
    }

    public void setValue_float(float val)
    {
        int intVal = System.BitConverter.ToInt32(System.BitConverter.GetBytes(val), 0);
        for (int i = 0; i < bitNo; i++)
        {
            bits[i].setValue((intVal & (1 << i)) != 0);
        }
    }

    public void setValue_double(double val)
    {
        ulong intVal = System.BitConverter.ToUInt64(System.BitConverter.GetBytes(val), 0);
        for (int i = 0; i < bitNo; i++)
        {
            bits[i].setValue((intVal & ((ulong)1 << i)) != 0);
        }
    }

    public bool checkAgainst_int(int val)
    {
        return getValueUnsigned_int() == val;
    }

    public bool checkAgainstSigned_int(int val)
    {
        return getValueSigned_int() == val;
    }

    public bool checkAgainstFloat(float val)
    {
        return getValueFloat() == val;
    }

    public bool checkAgainstDouble(double val)
    {
        return getValueDouble() == val;
    }   

    private void disableBits()
    {
        for (int i = 0; i < bitNo; i++)
        {
            bits[i].isInteractable = false;
        }
    }

    private void enableBits()
    {
        for (int i = 0; i < bitNo; i++)
        {
            bits[i].isInteractable = true;
        }
    }

    private void setMode()
    {
        switch (mode)
        {
            case InputMode.TestBinary:
                if (type == ByteType.Float || type == ByteType.Double) break;
                for (int i = 0; i < bitNo; i++)
                {
                    bits[i].gameObject.GetComponent<MeshRenderer>().material = editableMaterial;
                }
                break;
            case InputMode.TestDecimal:
                disableBits();
                for (int i = 0; i < bitNo; i++)
                {
                    bits[i].gameObject.GetComponent<MeshRenderer>().material = readOnlyMaterial;
                }
                break;
            case InputMode.Sandbox:
                for (int i = 0; i < bitNo; i++)
                {
                    bits[i].gameObject.GetComponent<MeshRenderer>().material = readOnlyMaterial;
                }
                break;
        }
    }
}
