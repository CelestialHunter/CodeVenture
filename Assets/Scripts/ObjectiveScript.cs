using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveScript : MonoBehaviour
{
    public BoolCondition condition;

    public DoorScript[] targetDoors;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(condition != null)
        {
            foreach (DoorScript door in targetDoors)
            {
                door.isLocked = !condition.Invoke();
            }
        }
    }    
}

[Serializable]
public class BoolCondition : SerializableCallback<bool> { }
