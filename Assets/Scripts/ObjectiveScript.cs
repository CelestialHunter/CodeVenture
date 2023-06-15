using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveScript : MonoBehaviour
{
    public BoolCondition[] condition;
    public ObjectiveSystem objectiveSystem;
    public string objectiveName;

    private bool isCompleted = false;
    private bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone) return;
        checkCompleted();
        if (isCompleted)
        {
            objectiveSystem.setObjective(objectiveName);
            isDone = true;
        }
    }   
    
    void checkCompleted()
    {
        isCompleted = true;
        foreach (BoolCondition cond in condition)
        {
            isCompleted = isCompleted && cond.Invoke();
        }
    }
}

[Serializable]
public class BoolCondition : SerializableCallback<bool> { }
