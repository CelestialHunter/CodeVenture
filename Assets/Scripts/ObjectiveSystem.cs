using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveSystem : MonoBehaviour
{

    [Serializable]
    public class Objective
    {
        [SerializeField]
        public string objectiveName;
        [SerializeField]
        public string[] doorNames;
        [SerializeField]
        public bool isCompleted;

        
        public Objective()
        {
            objectiveName = "";
            doorNames = new string[0];
            isCompleted = false;
        }

        public Objective(string objectiveName, string[] doorNames, bool isCompleted)
        {
            this.objectiveName = objectiveName;
            this.doorNames = doorNames;
            this.isCompleted = isCompleted;
        }
        
        public void CompleteObjective()
        {
            this.isCompleted = true;
        }
    }

    [SerializeField]
    public List<Objective> objectives;

    public DoorSystem doorSystem;
    public UIScript ui;

    private void Start()
    {
        GameObject go;
        if((go = GameObject.Find("_loadGame"))!=null)
        {
            LoadObjectives();
            Destroy(go);           
        }
    }

    public void SaveObjectives()
    {
        // save objectives list to XML
        Debug.Log("Saving game...");

        // create new XML file
        System.Xml.Serialization.XmlSerializer writer =
            new System.Xml.Serialization.XmlSerializer(typeof(List<Objective>));

        System.IO.FileStream file = System.IO.File.Create(Application.persistentDataPath + "/objectives.xml");

        writer.Serialize(file, objectives);

        file.Close();

        Debug.Log("Game saved.");
    }

    public void LoadObjectives()
    {
        // load objectives list from XML
        Debug.Log("Loading game...");

        // check if objectives.xml exists

        System.IO.FileStream file = null;
        try
        {
            file = System.IO.File.Open(Application.persistentDataPath + "/objectives.xml", System.IO.FileMode.Open);
        }
        catch (System.IO.FileNotFoundException)
        {
            Debug.Log("No save file found.");
            return;
        }

        System.Xml.Serialization.XmlSerializer reader =
            new System.Xml.Serialization.XmlSerializer(typeof(List<Objective>));

        objectives = (List<Objective>)reader.Deserialize(file);

        file.Close();

        // unlock and open completed doors
        foreach (Objective o in objectives)
            if (o.isCompleted)
                foreach (string doorName in o.doorNames)
                {
                    doorSystem.unlockDoor(doorName);
                    doorSystem.openDoor(doorName);
                }
    }

    public void setObjective(string objectiveName)
    {
        Objective o = objectives.Where(x => x.objectiveName == objectiveName).FirstOrDefault();
        o.CompleteObjective();

        
        foreach (string doorName in o.doorNames)
        {
            doorSystem.unlockDoor(doorName);
        }
        StartCoroutine(ui.showUnlockedDoorsNotif(o.doorNames));
        GameObject.Find("SoundManager").GetComponent<SoundsManager>().playSound("doorUnlock");
    }

    
}
