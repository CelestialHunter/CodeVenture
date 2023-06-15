using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScript : MonoBehaviour, Interactable
{
    public ByteScript byteObj;
    public TMP_Text valueText;
    public TMP_InputField input_valueText;

    private PlayerScript player;
    private PlayerMovement playerMovement;

    private bool isEditing = false;
    private bool _isInteractable = true;

    public Material wrongMaterial;
    
    public string interactMessage => isInteractable ? "Input Value" : "";

    public bool isInteractable => _isInteractable;

    public void highlight()
    {
        if (!isInteractable) return;
    }

    public void interact()
    {
        if (!isInteractable) return;

        isEditing = true;

        player.switchInteraction();
        playerMovement.switchMovement();

        input_valueText.interactable = true;
        input_valueText.ActivateInputField();
    }

    public void unhighlight()
    {
        if (!isInteractable) return;
    }


    // Start is called before the first frame update
    void Start()
    {       
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        input_valueText.onEndEdit.AddListener(delegate
        {
            isEditing = false;
            player.switchInteraction();
            playerMovement.switchMovement();
            input_valueText.interactable = false;

            if(byteObj.mode == ByteScript.InputMode.Sandbox)
                setBitsFromDisplay();
        });

        setMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (byteObj.mode == ByteScript.InputMode.Sandbox && !isEditing)
            setDisplayFromBits();

        //setMode();        
    }

    void setDisplayFromBits()
    {
        switch (byteObj.type)
        {
            case ByteScript.ByteType.Unsigned_Byte:
            case ByteScript.ByteType.Unsigned_Short:
            case ByteScript.ByteType.Unsigned_Int:
                input_valueText.text = byteObj.getValueUnsigned_int().ToString();
                break;
            case ByteScript.ByteType.SignMagnitude:
                input_valueText.text = byteObj.getValueSignMagnitude().ToString();
                break;
            case ByteScript.ByteType.OneComplement:
                input_valueText.text = byteObj.getValueOneComplement().ToString();
                break;
            case ByteScript.ByteType.Signed_Byte:
            case ByteScript.ByteType.Signed_Short:
            case ByteScript.ByteType.Signed_Int:
                input_valueText.text = byteObj.getValueSigned_int().ToString();
                break;
            case ByteScript.ByteType.Float:
                input_valueText.text = byteObj.getValueFloat().ToString();
                break;
            case ByteScript.ByteType.Double:
                input_valueText.text = byteObj.getValueDouble().ToString();
                break;
                //case ByteScript.ByteType.IBM:
                //    valueText.text = byteObj.getValueIBM().ToString();
                //    break;
        }
        //valueText.text = byteObj.getValueUnsigned().ToString();
    }

    void setBitsFromDisplay()
    {
        switch (byteObj.type)
        {
            case ByteScript.ByteType.Unsigned_Byte:
                {
                    byte value = byte.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
            case ByteScript.ByteType.Unsigned_Short:
                {
                    ushort value = ushort.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
            case ByteScript.ByteType.Unsigned_Int:
                {
                    uint value = uint.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
            case ByteScript.ByteType.SignMagnitude:
                {
                    sbyte value = sbyte.Parse(input_valueText.text);

                    sbyte mag = (sbyte)(value < 0 ? -value : value);
                    sbyte sgn = (sbyte)(value < 0 ? 0b10000000 : 00000000);

                    byteObj.setValue(new byte[] {(byte)(sgn | mag)});                    
                }
                break;
            case ByteScript.ByteType.OneComplement:
                {
                    byte value = byte.Parse(input_valueText.text);
                    byte sgn = (byte)(value & 0x80);
                    byte mag = (byte)(value & 0x7F);

                    if (sgn == 0x80) mag = (byte)((mag+1) & 0x7F);

                    byteObj.setValue(new byte[] { (byte)(sgn | mag) });
                }
                break;
            case ByteScript.ByteType.Signed_Byte:
                {
                    sbyte value = sbyte.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes((sbyte)value));
                    break;
                }
            case ByteScript.ByteType.Signed_Short:
                {
                    short value = short.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
            case ByteScript.ByteType.Signed_Int:
                {
                    int value = int.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
                break;
            case ByteScript.ByteType.Float:
                {
                    float value = float.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
            case ByteScript.ByteType.Double:
                {
                    double value = double.Parse(input_valueText.text);
                    byteObj.setValue(System.BitConverter.GetBytes(value));
                    break;
                }
        }
    }

    void setMode()
    {
        switch (byteObj.mode)
        {
            case ByteScript.InputMode.Sandbox:
                _isInteractable = true;
                GetComponent<Renderer>().material = byteObj.readOnlyMaterial;
                break;
            case ByteScript.InputMode.TestDecimal:
                _isInteractable = true;
                GetComponent<Renderer>().material = byteObj.editableMaterial;
                break;
            case ByteScript.InputMode.TestBinary:
                _isInteractable = false;
                GetComponent<Renderer>().material = byteObj.readOnlyMaterial;
                break;
        }
    }

    public bool checkAgainstBits()
    {
        bool retVal = false;
        switch (byteObj.type)
        {
            case ByteScript.ByteType.Unsigned_Byte:
            case ByteScript.ByteType.Unsigned_Short:
            case ByteScript.ByteType.Unsigned_Int:
                retVal = byteObj.getValueUnsigned_int() == int.Parse(input_valueText.text);
                break;
            case ByteScript.ByteType.SignMagnitude:
                retVal = byteObj.getValueSignMagnitude() == int.Parse(input_valueText.text);
                break;
            case ByteScript.ByteType.OneComplement:
                retVal = byteObj.getValueOneComplement() == int.Parse(input_valueText.text);
                break;
            case ByteScript.ByteType.Signed_Byte:
            case ByteScript.ByteType.Signed_Short:
            case ByteScript.ByteType.Signed_Int:
                retVal = byteObj.getValueSigned_int() == int.Parse(input_valueText.text);
                break;
            case ByteScript.ByteType.Float:
                retVal = float.Equals(byteObj.getValueFloat(), float.Parse(input_valueText.text));
                break;
            case ByteScript.ByteType.Double:
                retVal = double.Equals(byteObj.getValueDouble(), double.Parse(input_valueText.text));
                break;
            //case ByteScript.ByteType.IBM:
            //    return byteObj.getValueIBM() == double.Parse(valueText.text);
        }


        if (!retVal)
            GetComponent<Renderer>().material = wrongMaterial;
        else
            GetComponent<Renderer>().material = byteObj.readOnlyMaterial;

        return retVal;
    }
}
