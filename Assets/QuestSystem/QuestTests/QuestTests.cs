using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class QuestTests
{
    static int NUMBER_OF_PARENTS = 10;

    // A Test behaves as an ordinary method
    [Test]
    public void ParentQuestWithNoSubquestsIsVacuouslyComplete()
    {
        ParentQuest quest = new ParentQuest("asdf", "desc");
        quest.updateCompletionStatus();
        Assert.That(quest.IsComplete);
    }

    [Test]
    public void ObjectiveQuestWithNoObjectiveIsVacuouslyComplete()
    {
        ObjectiveQuest q = new ObjectiveQuest("lkj", "asdf");
        q.updateCompletionStatus();
        Assert.That(q.IsComplete);
    }

    [Test]
    public void ParentQuestWithIncompleteSubquestsIsNotComplete1()
    {
        ParentQuest quest = new ParentQuest("asdf", "desc");
        ObjectiveQuest q2 = new ObjectiveQuest("lkj", "asdf");
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10);
        q2.AddObjective(o);
        quest.AddChildQuest(q2);
        Assert.That(!quest.IsComplete);
    }

    [Test]
    public void ParentQuestWithIncompleteSubquestsIsNotComplete2()
    {
        ParentQuest quest = new ParentQuest("asdf", "desc");
        ObjectiveQuest q2 = new ObjectiveQuest("lkj", "asdf");
        ParentQuest q3 = new ParentQuest("asdf", "desc"); //vacuously complete
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10);
        q2.AddObjective(o);
        quest.AddChildQuest(q2);
        quest.AddChildQuest(q3);
        Assert.That(!quest.IsComplete);
    }

    [Test]
    public void ParentQuestWithCompleteSubquestsIsAlsoComplete()
    {
        ParentQuest quest = new ParentQuest("asdf", "desc");
        ObjectiveQuest q2 = new ObjectiveQuest("lkj", "asdf");
        ParentQuest q3 = new ParentQuest("asdf", "desc"); //vacuously complete
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10);
        q2.AddObjective(o);
        quest.AddChildQuest(q2);
        quest.AddChildQuest(q3);

        q2.UpdateObjective(new Event(EventType.Interact, EventSubject.PersonB), 11);
        Assert.That(quest.IsComplete);
    }

    [Test]
    public void ObjectiveQuestIsCompleteWhenObjectiveIsComplete()
    {
        ObjectiveQuest q = new ObjectiveQuest("lkj", "asdf");
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10);
        q.AddObjective(o);

        Assert.That(!q.IsComplete);
        //event is a new Event with the same values. Still needs to work.
        q.UpdateObjective(new Event(EventType.Interact, EventSubject.PersonB), 10);
        Assert.That(o.IsComplete);
        Assert.That(q.IsComplete);

    }

    [Test]
    public void ObjectiveQuestIsCompleteWhenObjectiveIsCompletePart2()
    {
        ObjectiveQuest q = new ObjectiveQuest("lkj", "asdf");
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10, 0, ObjectiveRequirement.LESS_THAN);
        q.AddObjective(o);

        Assert.That(o.IsComplete);
        Assert.That(q.IsComplete);

    }

    [Test]
    public void OrderedQuestIsCompleteWhenChildrenAreComplete()
    {
        ObjectiveQuest q = new ObjectiveQuest("lkj", "asdf");
        Objective o = new Objective(new Event(EventType.Interact, EventSubject.PersonB), 10, 0, ObjectiveRequirement.LESS_THAN);
        q.AddObjective(o);

        Assert.That(o.IsComplete);
        Assert.That(q.IsComplete);

    }



    //private static string randomString() {
    //    return UnityEngine.Random.Range(0, 9999).ToString();
    //}

    //[DatapointSource]
    //public List<ParentQuest> parentQuests = generateParentQuests(NUMBER_OF_PARENTS);

    //private static List<ParentQuest> generateParentQuests(int amount)
    //{
    //    List<ParentQuest> parentQuests = new List<ParentQuest>();

    //    for (int i = 0; i < amount; i++)
    //        parentQuests.Add(generateParentQuestNoChildren());

    //    return parentQuests;
    //}

    //private static ParentQuest generateParentQuestNoChildren() { 
    //    return new ParentQuest(randomString(), randomString());
    //}

    //private static ObjectiveQuest generateObjectiveQuestNoObjectives()
    //{
    //    return new ObjectiveQuest(randomString(), randomString());
    //}

    //private static ObjectiveQuest generateObjectiveQuestWithObjectives()
    //{
    //    ObjectiveQuest q = new ObjectiveQuest(randomString(), randomString());
    //    Objective o = new Objective(generateRandomEvent(), UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 5), ObjectiveRequirement.GREATER_THAN_OR_EQUAL);
    //    return q;
    //}

    //private static Event generateRandomEvent() {
    //    return new Event((EventType)UnityEngine.Random.Range(0, 5), (EventSubject)UnityEngine.Random.Range(0, 5));
    //}

    //private static List<ObjectiveQuest> generateObjectiveQuests()
    //{
    //    List<ObjectiveQuest> objectiveQuests = new List<ObjectiveQuest>();

    //    for (int i = 0; i < NUMBER_OF_PARENTS; i++)
    //        objectiveQuests.Add(new ObjectiveQuest(randomString(), randomString()));

    //    return objectiveQuests;
    //}

    //[TestCaseSource(nameof(generateParentQuests))]
    //public void ParentClassesWithNoChildrenAreVacuouslyComplete(ParentQuest p)
    //{
    //    p.updateCompletionStatus();
    //    Assert.That(p.IsComplete);
    //}
}
