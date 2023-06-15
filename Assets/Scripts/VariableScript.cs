using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VariableScript : MonoBehaviour, Interactable
{

    public GameObject peerObj;
    
    public string VariableName;
    public byte value;

    bool isPeering = false;

    public string[] interactMessages = { "Press E to Peer", "Press E to Unpeer" };
    string Interactable.interactMessage => interactMessages[isPeering ? 1 : 0];

    public bool isInteractable => true;
    
    public void highlight()
    {
        //todo
    }

    IEnumerator loadVariableScene()
    {
        AsyncOperation loadSceneOp = SceneManager.LoadSceneAsync("VariableScene", LoadSceneMode.Additive);
        while (!loadSceneOp.isDone)
        {
            yield return null;
        }

        RenderTexture renderTexture = new RenderTexture(256, 256, 24);


        GameObject.Find("VariableCamera").GetComponent<Camera>().targetTexture = renderTexture;
        peerObj.GetComponent<MeshRenderer>().material.mainTexture = renderTexture;
        GameObject.Find("VariableByte").GetComponent<ByteScript>().setValue_int(value);
    }
    
    public void interact()
    {
        if (!isPeering)
        {
            StartCoroutine(loadVariableScene());
            //GameObject.Find("VariableCamera").GetComponent<Camera>().gameObject.SetActive(false);
            peerObj.SetActive(true);
            isPeering = true;
        }
        else
        {
            // unload scene VariableScene
            // disable peerObj
            peerObj.SetActive(false);
            SceneManager.UnloadSceneAsync("VariableScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            isPeering = false;
        }
    }
    

    public void unhighlight()
    {
        //todo
    }

    // Start is called before the first frame update
    void Start()
    {
        peerObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
