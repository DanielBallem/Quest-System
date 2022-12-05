using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event { 
    EventType eventType;
    EventSubject subject;

    public Event(EventType eventType, EventSubject subject) { 
    this.eventType = eventType;
    this.subject = subject;
    }

    public override string ToString() => eventType.ToString() + subject.ToString();

    public override bool Equals(object obj)
    {

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return this.ToString().Equals(obj.ToString());
    }
}

public enum EventType
{
    GoToLocation,
    LoneAction,
    Eliminate,
    Interact,
    TalkToPerson,
}

/**
 * Postfix of a given event. Modify as your game requires more event subjects.
 */
public enum EventSubject { 
    PersonA,
    PersonB,
    Location1,
    Location2,
    Jump,
    Walk,
    Enemy1,
    Enemy2
}
