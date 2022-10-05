using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ObjectiveQuestTests
{
    [Test]
    public void Initialization()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");

        Assert.True(fq.getTitle().Equals("Title 1"));
        Assert.True(fq.getDescription().Equals("Desc 1"));
        Assert.False(fq.isComplete());
        Assert.False(fq.isAvailable());
    }

    [Test]
    public void AddItem_Success()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("sword", 1, 0);
        fq.addItem(qi);

        Objective result = fq.getItems()[0];
        Assert.IsNotNull(result);
    }

    [Test]
    public void AddItem_Duplicate()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("sword", 1, 0);
        fq.addItem(qi);
        fq.addItem(qi);

        int questSize = fq.getItems().Count;
        Assert.True(questSize == 1);
    }

    [Test]
    public void AddItem_DuplicateItemId()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 0);
        Objective qi2 = new Objective("special_sword", 1, 1);
        fq.addItem(qi);
        fq.addItem(qi2);

        Assert.True(fq.getItems().Count == 1);
    }

    [Test]
    public void IsComplete_UponInitialization()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 1);
        fq.addItem(qi);

        Assert.True(fq.isComplete());

    }

    [Test]
    public void IsIncomplete_UponInitialization()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 0);
        fq.addItem(qi);

        Assert.False(fq.isComplete());
    }

    [Test]
    public void IsComplete_UponUpdatingObjectiveValue()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 0);
        fq.addItem(qi);
        fq.incrementCountForObjective(qi);

        Assert.True(fq.isComplete());
    }

    [Test]
    public void IsIncomplete_UponUpdatingObjectiveValue()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 1);
        fq.addItem(qi);
        fq.decrimentCountForObjective(qi);

        Assert.False(fq.isComplete());
    }

    [Test]
    public void IsIncomplete_UponUpdatingOneObjectiveValue()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 0);
        Objective qi2 = new Objective("special_shield", 1, 0);
        fq.addItem(qi);
        fq.addItem(qi2);
        fq.incrementCountForObjective(qi);

        Assert.False(fq.isComplete());
        Assert.True(qi.isComplete());
    }

    [Test]
    public void getItems_EmptyList()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        List<Objective> empty = fq.getItems();

        Assert.That(empty.Count, Is.EqualTo(0));
    }

    [Test]
    public void getItems_NonEmptyList()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("special_sword", 1, 0);
        Objective qi2 = new Objective("special_shield", 1, 0);
        fq.addItem(qi);
        fq.addItem(qi2);

        List<Objective> list = fq.getItems();

        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(qi, Is.EqualTo(list[0]));
        Assert.That(qi2, Is.EqualTo(list[1]));
    }

    [Test]
    public void updatingCounts()
    {
        ObjectiveQuest fq = new ObjectiveQuest("Title 1", "Desc 1");
        Objective qi = new Objective("flower_1", 1, 0);
        Objective qi2 = new Objective("flower_2", 1, 0);
        fq.addItem(qi);
        fq.addItem(qi2);

        fq.incrementCountForObjective("flower_0");
        Assert.That(qi.isComplete, Is.False);
        Assert.That(qi2.isComplete, Is.False);

        fq.incrementCountForObjective("flower_2");
        Assert.That(qi2.isComplete, Is.True);

        fq.decrimentCountForObjective("flower_2");
        Assert.That(qi2.isComplete, Is.False);



    }
}

public class MultiQuestTests {

    [Test]
    public void Initialization()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1", true);

        Assert.True(mq.getTitle().Equals("Title 1"));
        Assert.True(mq.getDescription().Equals("Desc 1"));
        Assert.That(mq.isComplete(), Is.False);
        Assert.That(mq.isAvailable(), Is.True);
    }

    [Test]
    public void addQuest()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest innerq = new ObjectiveQuest("inner 1", "desc 1");

        mq.addQuest(innerq);
        List<Quest> subqs = mq.getSubquests();
        Assert.That(subqs[0], Is.EqualTo(innerq));
    }

    [Test]
    public void removeQuest()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest innerq = new ObjectiveQuest("inner 1", "desc 1");
        ObjectiveQuest innerq2 = new ObjectiveQuest("inner 2", "desc 2");

        mq.addQuest(innerq);
        mq.addQuest(innerq2);
        mq.removeQuest(innerq);
        mq.removeQuest("inner 2");

        List<Quest> subqs = mq.getSubquests();
        Assert.That(subqs.Count, Is.EqualTo(0));
        Assert.That(mq.isComplete(), Is.True);
    }

    [Test]
    public void getSubquests_noQuests()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");

        List<Quest> subqs = mq.getSubquests();
        Assert.That(subqs, Is.EqualTo(new List<Quest>()));
    }

    [Test]
    public void checkAndUpdateCompletionStatus_False()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest fq = new ObjectiveQuest("inner 1", "inner 2");
        Objective qi = new Objective("sword_0", 1, 0);

        //mq => fq => qi
        mq.addQuest(fq);
        fq.addItem(qi);

        Assert.That(mq.isComplete(), Is.False);
    }

    [Test]
    public void checkAndUpdateCompletionStatus_Success()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest fq = new ObjectiveQuest("inner 1", "inner 2");
        Objective qi = new Objective("sword_0", 1, 0);

        //mq => fq => qi
        mq.addQuest(fq);
        fq.addItem(qi);

        fq.incrementCountForObjective("sword_0");

        Assert.That(mq.isComplete(), Is.True);
    }

    [Test]
    public void checkAndUpdateCompletionStatus_FalseOtherCase()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest fq = new ObjectiveQuest("inner 1", "inner 2");
        Objective qi = new Objective("sword_0", 1, 0);
        Objective qi2 = new Objective("sword_1", 1, 0);

        //mq => fq => qi
        mq.addQuest(fq);
        fq.addItem(qi);
        fq.addItem(qi2);

        fq.incrementCountForObjective("sword_0");

        Assert.That(mq.isComplete(), Is.False);
        Assert.That(fq.isComplete(), Is.False);
        Assert.That(qi.isComplete(), Is.True);
    }

    [Test]
    public void checkAndUpdateCompletionStatus_FalseMultiChildren()
    {
        MultiQuest mq = new MultiQuest("Title 1", "Desc 1");
        ObjectiveQuest fq = new ObjectiveQuest("inner 1", "desc 1");
        ObjectiveQuest fq2 = new ObjectiveQuest("inner 2", "desc 2");
        Objective qi = new Objective("sword_0", 1, 0);
        Objective qi2 = new Objective("sword_1", 1, 0);

        //mq => fq => qi
        // | => fq2 => qi2
        mq.addQuest(fq);
        mq.addQuest(fq2);
        fq.addItem(qi);
        fq2.addItem(qi2);

        fq.incrementCountForObjective("sword_0");

        Assert.That(fq.isComplete(), Is.True);
        Assert.That(fq2.isComplete(), Is.False);
        Assert.That(mq.isComplete(), Is.False);
    }
}

public class SequenceQuests {
    [Test]
    public void Initialization()
    {
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        Assert.True(sq.getTitle().Equals("Title 1"));
        Assert.True(sq.getDescription().Equals("Desc 1"));
        Assert.False(sq.isComplete());
        Assert.False(sq.isAvailable());
    }

    [Test]
    public void addQuests_markAsAvailable()
    {
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        ObjectiveQuest ob = new ObjectiveQuest("title", "desc");
        ObjectiveQuest ob2 = new ObjectiveQuest("title2", "desc2");
        sq.addQuest(ob);
        sq.addQuest(ob2);
        Assert.True(ob.isAvailable());
        Assert.False(ob2.isAvailable());
    }

    [Test]
    public void completionStatus()
    {
        //sequence quest to get a flower THEN a sword
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        ObjectiveQuest ob = new ObjectiveQuest("title", "desc");
        ObjectiveQuest ob2 = new ObjectiveQuest("title2", "desc2");

        EventItem item = new EventItem("flower", 1, 0);
        EventItem item2 = new EventItem("sword", 1, 0);
        
        ob.addItem(item);
        ob2.addItem(item2);

        sq.addQuest(ob);
        sq.addQuest(ob2);
        
        Assert.False(sq.isComplete());

        ob.incrementCountForObjective("flower");
        Assert.False(sq.isComplete());
        Assert.True(ob2.isAvailable());
        ob2.incrementCountForObjective("sword");
        Assert.True(sq.isComplete());

    }

    [Test]
    public void completionStatus_OneIsAlreadyComplete()
    {
        //sequence quest to get a flower THEN a sword
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        ObjectiveQuest ob = new ObjectiveQuest("title", "desc");
        ObjectiveQuest ob2 = new ObjectiveQuest("title2", "desc2");

        EventItem item = new EventItem("flower", 1, 0);
        EventItem item2 = new EventItem("sword", 1, 1);

        ob.addItem(item);
        ob2.addItem(item2);

        sq.addQuest(ob);
        sq.addQuest(ob2);

        Assert.False(sq.isComplete());

        ob.incrementCountForObjective("flower");
        Assert.True(sq.isComplete());

    }

    [Test]
    public void removeQuest_willMarkAsComplete()
    {
        //sequence quest to get a flower THEN a sword
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        ObjectiveQuest ob = new ObjectiveQuest("title", "desc");
        ObjectiveQuest ob2 = new ObjectiveQuest("title2", "desc2");

        EventItem item = new EventItem("flower", 1, 0);
        EventItem item2 = new EventItem("sword", 1, 1);

        ob.addItem(item);
        ob2.addItem(item2);

        sq.addQuest(ob);
        sq.addQuest(ob2);

        Assert.False(sq.isComplete());

        sq.removeQuest(ob);
        Assert.True(sq.isComplete());

    }

    [Test]
    public void removeQuest_willSetCurrentQuestToLastQuestIfOutOfRange()
    {
        //sequence quest to get a flower THEN a sword
        SequenceQuest sq = new SequenceQuest("Title 1", "Desc 1");

        ObjectiveQuest ob = new ObjectiveQuest("title", "desc");
        ObjectiveQuest ob2 = new ObjectiveQuest("title2", "desc2");

        EventItem item = new EventItem("flower", 1, 0);
        EventItem item2 = new EventItem("sword", 1, 1);

        ob.addItem(item);
        ob2.addItem(item2);

        sq.addQuest(ob);
        sq.addQuest(ob2);

        Assert.False(sq.isComplete());

        //current quest will be index 1, count will update to 1. index will be zero (no errors)
        ob.incrementCountForObjective("flower");
        sq.removeQuest(ob);
        Assert.True(sq.isComplete());

    }
}