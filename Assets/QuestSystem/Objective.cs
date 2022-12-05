using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveRequirement
{
    LESS_THAN,
    GREATER_THAN,
    EQUAL_TO,
    GREATER_THAN_OR_EQUAL,
    LESS_THAN_OR_EQUAL
}

public class Objective : CompletableObject
{
    Event _event;
    ObjectiveRequirement _requirement;
    int _target;
    int _current;
    bool _complete = false;

    public bool IsComplete { get { return _complete; } set { _complete = value; } }

    public Objective(Event eventObject, int target, int current, ObjectiveRequirement requirement)
    {
        _event = eventObject;
        _target = target;
        _current = current;
        _requirement = requirement;
        updateCompletionStatus();
    }

    public Objective(Event eventObject, int target) : this(eventObject, target, 0, ObjectiveRequirement.GREATER_THAN_OR_EQUAL) { }

    public string EventString => _event.ToString();

    public void updateEvent(Event triggeredEvent, int amount = 1)
    {
        if (_event.Equals(triggeredEvent))
        {
            _current += amount;

            updateCompletionStatus();
        }
    }

    public void updateCompletionStatus()
    {
        switch (_requirement)
        {
            case ObjectiveRequirement.LESS_THAN:
                IsComplete = _current < _target;
                break;
            case ObjectiveRequirement.LESS_THAN_OR_EQUAL:
                IsComplete = _current <= _target;
                break;
            case ObjectiveRequirement.GREATER_THAN:
                IsComplete = _current > _target;
                break;
            case ObjectiveRequirement.GREATER_THAN_OR_EQUAL:
                IsComplete = _current >= _target;
                break;
            case ObjectiveRequirement.EQUAL_TO:
                IsComplete = _current == _target;
                break;
            default:
                IsComplete = false;
                break;
        }
    }
}
