using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProjectorScript : MonoBehaviour, Interactable
{
    public bool _isInteractable = true;
    public bool _isVideoPlaying = false;

    public GameObject projectorScreen;
    public GameObject projectorObject;

    public VideoPlayer videoPlayer;

    public string interactMessage => (_isVideoPlaying ? "Press E to pause video" : "Press E to play video");

    public bool isInteractable => _isInteractable;

    public void highlight()
    {
        if (!isInteractable) return;
        projectorObject.transform.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Highlight");
    }

    public void interact()
    {
        if (!isInteractable) return;

        if (_isVideoPlaying)
        {
            videoPlayer.Pause();
            _isVideoPlaying = false;
        }
        else
        {
            videoPlayer.Play();
            _isVideoPlaying = true;
        }
    }

    public void unhighlight()
    {
        if (!isInteractable) return;
        projectorObject.transform.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
    }

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += (videoPlayer) => { _isVideoPlaying = false; };
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
