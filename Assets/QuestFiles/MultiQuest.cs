using System.Collections.Generic;

public class MultiQuest : Quest
{
    protected Dictionary<string, Quest> subquests;

    public MultiQuest(string title, string description, bool available = false) : base(title, description, available)
    {
        subquests = new Dictionary<string, Quest>();
    }

    public virtual void addQuest(Quest q)
    {
        if (q == null) return;
        q.setParent(this);

        //not a current quest yet
        if (getQuest(q) == null) {
            subquests.Add(q.getTitle(), q);
        } 
        else { 
            subquests[q.getTitle()] = q;
        }
    }

    public virtual void removeQuest(Quest q)
    {
        if (q == null) return;
        removeQuest(q.getTitle());
    }

    public void removeQuest(string q) {
        if (q == null) return;
        subquests.Remove(q);
        checkAndUpdateCompletionStatus();
    }
    

    public Quest getQuest(Quest q) => (q == null) ? null : getQuest(q.getTitle());
    public Quest getQuest(string q)
    {
        Quest quest;
        return (subquests.TryGetValue(q, out quest)) ? quest : null;
    }

    public override void checkAndUpdateCompletionStatus() {
        bool oneIsIncomplete = false;
        foreach (KeyValuePair<string, Quest> kvp in subquests) {
            if (!kvp.Value.isComplete()) {
                oneIsIncomplete = true;
                break;
            }
        }
        this.completed = !oneIsIncomplete;
        if (this.completed) updateParentCompletionStatus();
    }

    public List<Quest> getSubquests() {
        List<Quest> subqs = new List<Quest>();
        foreach (KeyValuePair<string, Quest> kvp in subquests) {
            subqs.Add(kvp.Value);
        }
        return subqs;
    }
}