using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioClip doorUnlockSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string sound)
    {
        if (string.Compare(sound, "doorUnlock") == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(doorUnlockSound);
        }
    }
}
