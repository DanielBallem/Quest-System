using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceQuest : MultiQuest
{
    int currentQuestIndex;
    List<string> questIds;
    public SequenceQuest(string title, string description, bool available = false) : base(title, description, available)
    {
        currentQuestIndex = 0;
        questIds = new List<string>();
    }

    public override void addQuest(Quest q)
    {
        if (q == null) return;
        base.addQuest(q);
        //keep track of quest, mark as available if set to quest index.
        questIds.Add(q.getTitle());

        if (questIds[currentQuestIndex] == q.getTitle())
        {
            q.markAsAvailable();
            q.markAsActivated();
        }
        else
            q.markAsUnavailable();

        checkAndUpdateCompletionStatus();
    }

    public override void removeQuest(Quest q)
    {
        if (q == null) return;
        base.removeQuest(q);
        
        //ensuring currentQuestIndex isn't out of bounds. Just for safety
        questIds.Remove(q.getTitle());
        if (currentQuestIndex >= questIds.Count)
            currentQuestIndex = questIds.Count - 1;

        checkAndUpdateCompletionStatus();

    }

    public override void checkAndUpdateCompletionStatus()
    {
        Quest currQuest = getQuest(questIds[currentQuestIndex]);
        if (currQuest == null) return;

        //increment available quest
        if (currQuest.isComplete() && currentQuestIndex < questIds.Count - 1)
        {
            currentQuestIndex++;
            Quest q = getQuest(questIds[currentQuestIndex]);
            q.markAsAvailable();
            q.markAsActivated();

            //check if this quest is finished
            checkAndUpdateCompletionStatus();
        }
        //mark sequence as complete
        else if (currQuest.isComplete() && currentQuestIndex == questIds.Count - 1) { 
            this.completed = true;
        }
        if (this.completed) updateParentCompletionStatus();
    }
}
