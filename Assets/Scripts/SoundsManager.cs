using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Serializable]
    public class PlaySound
    {
        public string name;
        public AudioClip sound;

        public PlaySound()
        {
            name = "";
            sound = null;
        }
    }

    public List<PlaySound> sounds;

    public void playSound(string sound)
    {
        try
        {
            AudioClip clip = sounds.Where(s => (string.Compare(s.name, sound) == 0)).First<PlaySound>().sound;
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        catch (Exception ex)
        {
            Debug.Log("Sound [" + sound + "] does not exist.\n");
        }
    }
}
