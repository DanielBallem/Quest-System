using System.Collections.Generic;

public class ObjectiveQuest : Quest
{

    protected Dictionary<string, Objective> items;

    public ObjectiveQuest(string title, string description, bool available = false) : base(title, description, available)
    {
        items = new Dictionary<string, Objective>();
    }

    public void addItem(Objective item)
    {
        if (items == null) return;
        if (items.TryGetValue(item.getId(), out Objective val))
        {
            val = item;
        }
        else
        {
            items.Add(item.getId(), item);
            item.parent = this;
        }
        checkAndUpdateCompletionStatus();

    }

    public override void checkAndUpdateCompletionStatus()
    {
        List<Objective> list = getItems();

        bool questIsComplete = true;
        foreach (Objective qi in list)
        {
            if (!qi.isComplete())
            {
                questIsComplete = false;
                break;
            }
        }
        this.completed = questIsComplete;
        if (this.completed) updateParentCompletionStatus();
    }

    public void decrimentCountForObjective(Objective item, int count = 1)
    {
        if (item == null) return;
        decrimentCountForObjective(item.getId(), count);
    }
    public void decrimentCountForObjective(string itemId, int count = 1)
    {
        items.TryGetValue(itemId, out Objective item);
        if (item == null) return;
        item.decrimentCurrentCount(count);
        //checkAndUpdateCompletionStatus();


    }

    //useful for displaying items. No need for keys
    public List<Objective> getItems()
    {
        List<Objective> itemList = new List<Objective>();
        foreach (KeyValuePair<string, Objective> entry in items)
        {
            itemList.Add(entry.Value);
        }
        return itemList;
    }

    public void incrementCountForObjective(Objective item, int count = 1)
    {
        if (item == null) return;
        incrementCountForObjective(item.getId(), count);
    }
    public void incrementCountForObjective(string itemId, int count = 1)
    {
        items.TryGetValue(itemId, out Objective item);
        if (item == null) return;

        item.incrementCurrentCount(count);
        //checkAndUpdateCompletionStatus();
    }
}