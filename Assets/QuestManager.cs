using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //public MultiQuest game = new MultiQuest("Game", "Full game", true);
    //public Quest mainQuest;
    //public List<ObjectiveQuest> activeObjectives = new List<ObjectiveQuest>();
    //public Dictionary<string, List<Objective>> listeningObjectives = new Dictionary<string, List<Objective>>();
    //// create quests
    //void Start()
    //{
    //    SequenceQuest mainQuest = new SequenceQuest("Main quest", "Do this to beat the game", true);
    //    //MultiQuest sideQuests = new MultiQuest("sidequests", "Part of the game", true);
    //    this.mainQuest = mainQuest;
    //    game.addQuest(mainQuest);
    //    //game.addQuest(sideQuests);

    //    ObjectiveQuest clickQuest = createClickQuest();

    //    mainQuest.addQuest(clickQuest);
    //}

    //private ObjectiveQuest createClickQuest()
    //{
    //    ObjectiveQuest clickQuest = new ObjectiveQuest("Click on some objects", "I need you to click", true);
    //    Objective clickOnBox = new Objective("click_box", 1, 0);
    //    Objective clickOnSphere = new Objective("click_sphere", 1, 0);
    //    addObjectives(clickQuest, clickOnBox, clickOnSphere);
    //    return clickQuest;
    //}

    //public void markEvent(string eventId, int amount = 1) {
    //    bool objectivesAreListening = listeningObjectives.TryGetValue(eventId, out List<Objective> objectives);

    //    if (objectivesAreListening) {
    //        foreach (Objective objective in objectives) {
    //            objective.incrementCurrentCount(amount);
    //        }
    //    }

    //    Debug.Log(mainQuest.isComplete());
    //}

    //void addObjective(Objective o) {
    //    bool success = listeningObjectives.TryGetValue(o.getId(), out List<Objective> list);

    //    if (success)
    //    {
    //        list.Add(o);
    //    }
    //    else {
    //        List<Objective> newList = new List<Objective>();
    //        newList.Add(o);
    //        listeningObjectives.Add(o.getId(), newList);
    //    }
    //}

    //void addObjectives(ObjectiveQuest oq, params Objective[] objectives) {
    //    foreach (Objective objective in objectives) {
    //        oq.addObjective(objective);
    //        addObjective(objective);
    //    }  
    //}
}
