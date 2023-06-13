using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveScript : MonoBehaviour
{
    public BoolCondition condition;
    public ObjectiveSystem objectiveSystem;
    public string objectiveName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(condition != null)
        {
            if(condition.Invoke())
                objectiveSystem.setObjective(objectiveName);            
        }
    }    
}

[Serializable]
public class BoolCondition : SerializableCallback<bool> { }
