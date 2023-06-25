using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{

    public List<DoorScript> doors;
    
    void Start()
    {
        doors = new List<DoorScript>();
        foreach (Transform child in transform)
        {
            doors.Add(child.GetComponentInChildren<DoorScript>());
        }
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
