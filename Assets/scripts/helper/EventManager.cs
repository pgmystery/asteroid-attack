using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class EventManagerEvent<T> : UnityEvent<object[]> {}

public class EventManager : MonoBehaviour {
    // public static EnemyDestroyEvent enemyDestroyEvent;

//     private static TESTTEST testtest;
//     private static EnemyDestroyEvent enemyDestroyEvent;

    // private Dictionary <string, UnityEvent> eventDictionary;
    private Dictionary <string, dynamic> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof (EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init(); 
                }
            }

            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            // eventDictionary = new Dictionary<string, UnityEvent>();
            eventDictionary = new Dictionary<string, dynamic>();
        }

        // eventDictionary.Add("EnemyDestroy", enemyDestroyEvent);
        // eventDictionary.Add("EnemyDestroyEvent", testtest);
    }

    public static void Subscribe(string eventName, UnityAction<object[]> listener) {
        dynamic thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent) && thisEvent) {
            Debug.Log("event found!");
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new EventManagerEvent<object[]>();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void Unsubscribe(string eventName, UnityAction<object[]> listener)
    {
        if (eventManager == null) return;
        dynamic thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
            if (thisEvent.GetPersistentEventCount() == 0) instance.eventDictionary.Remove(eventName);
        }
    }

    // public static void TriggerEvent (string eventName)
    // {
    //     UnityEvent thisEvent = null;
    //     if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
    //     {
    //         thisEvent.Invoke ();
    //     }
    // }

    public static EventManagerEvent<object[]> Trigger(string eventName, object[] parameters) {
        dynamic thisEvent = null;
        // if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        // {
        //     thisEvent.Invoke ();
        //     return thisEvent;
        // }
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(parameters);
            return thisEvent;
        }
        return null;
    }
}
