using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CompletableObject {
    bool IsComplete { get; }

    public void updateCompletionStatus();

    public static bool AllAreComplete(IEnumerable<CompletableObject> objects)
    {
        if (objects == null) return true; //vacuously true

        bool allAreComplete = true;
        foreach (CompletableObject completable in objects)
        {
            if (!completable.IsComplete)
            {
                allAreComplete = false;
            }
        }
        return allAreComplete; //vacuously true if empty IEnumerable of objects
    }
}


public abstract class Quest : CompletableObject {
    public Quest parent;
    
    bool _available = false;
    bool _initiated = false;
    bool _complete = false;
    public bool IsComplete { get { return _complete; } }

    /**
     * <summary>
     * Whether or not this quest is available to initiate.
     * 
     * A subquest can only be made available iff their parent quest is also available.
     * If the quest does not have a parent, you can set it as you please.
     * </summary>
     */
    public bool IsAvailable { get { return _available; } 
        set {
            if (parent != null)
                //quests can only be available if their parent is available
                if (parent.IsAvailable)
                    _available = value;
                else
                    _available = false;
            else
                _available = value;
        } 
    }
    /**
     * <summary>
     * Whether or not the player has this quest initiated.
     * 
     * If the player hasn't started the quest, it is not initiated.
     * </summary>
     */
    public bool IsInitiated
    {
        get { return _initiated; }
        set
        {
            if (IsAvailable)
                _initiated = value;
        }
    }

    string _name;
    public string Name { get { return _name; } set { _name = value; } }
    string _description;
    public Quest(string name, string description, bool available = false, bool initiated = false) { 
        _name = name;
        _description = description;
        _available = available;
        _initiated = initiated;
        updateCompletionStatus();
    }

    public void updateCompletionStatus() {}

    public virtual void updateQuestCompletionStatus(IEnumerable<CompletableObject> requirements) {

        if (CompletableObject.AllAreComplete(requirements))
        {
            _complete = true;

            if (parent == null) return;

            if (_complete)
                parent.updateCompletionStatus();
        }
    }
}

public class ParentQuest : Quest
{
    HashSet<Quest> _subquests = new HashSet<Quest>();

    public ParentQuest(string name, string description, bool available = false, bool initiated = false) : base(name, description, available, initiated) { }

    public void AddChildQuest(Quest q) {
        q.parent = this;
        _subquests.Add(q);
        updateCompletionStatus();
    }

    new public void updateCompletionStatus()
    {
        base.updateQuestCompletionStatus(_subquests);
    }
}

public class ParentOrderedQuest : Quest
{
    List<Quest> _subquests;
    int _questIndex;

    public ParentOrderedQuest(string name, string description, bool available = false, bool initiated = false) : base(name, description, available, initiated) { }

    public void AddChildQuest(Quest q)
    {
        q.parent = this;
        _subquests.Add(q);

        //first quest is always available
        if ( _subquests.Count == 1)
            q.IsAvailable = true;
        else
            q.IsAvailable = false;
    }

    new public void updateCompletionStatus()
    {
        Quest currentQuest = _subquests[_questIndex];

        if (currentQuest.IsComplete) {
            _questIndex++;
        }
        if (_questIndex < _subquests.Count)
        {
            _subquests[_questIndex].IsAvailable = true;
        }

        base.updateQuestCompletionStatus(_subquests);


    }
}

public class ObjectiveQuest : Quest {
    Dictionary<string, Objective> _objectives = new Dictionary<string, Objective>();
    HashSet<string> _listenedToEvents = new HashSet<string>();

    public ObjectiveQuest(string name, string description, bool available = false, bool initiated = false) : base(name, description, available, initiated) { }

    public void AddObjective(Objective newObjective) { 
        _objectives.Add(newObjective.EventString, newObjective);

        _listenedToEvents.Add(newObjective.EventString);
        updateCompletionStatus();
    }

    public HashSet<string> GetListenedToEvents() { 
        return new HashSet<string>(_listenedToEvents);
    }

    public void UpdateObjective(Event triggeredEvent, int amount = 1) {
        Objective listeningObjective;
        bool success = _objectives.TryGetValue(triggeredEvent.ToString(), out listeningObjective);

        if (success) {
            listeningObjective.updateEvent(triggeredEvent, amount);
        }
        updateCompletionStatus();
    }

    new public void updateCompletionStatus()
    {
        base.updateQuestCompletionStatus(_objectives.Values);
    }
}