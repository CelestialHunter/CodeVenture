using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public List<DoorScript> doors;
    
    // Start is called before the first frame update
    void Start()
    {
        // populate doors list with all children
        doors = new List<DoorScript>();
        foreach (Transform child in transform)
        {
            doors.Add(child.GetComponentInChildren<DoorScript>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlockDoor(string doorName)
    {
        DoorScript door = doors.Find(x => x.roomName == doorName);
        door.unlock();
    }

    public void openDoor(string doorName)
    {
        DoorScript door = doors.Find(x => x.roomName == doorName);
        door.open();
    }
}
