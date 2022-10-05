public enum ObjectiveType
{
    OBTAIN,
    EVENT,
    KILL
}

public class Objective
{
    protected string id;
    protected string description;
    protected int countNeeded;
    protected int currentCount;
    public Quest parent;

    ObjectiveType type;

    public Objective(string objectiveId, int countNeeded, int currentCount, ObjectiveType type = ObjectiveType.OBTAIN, string description = "")
    {
        this.id = objectiveId;
        this.countNeeded = countNeeded;
        this.currentCount = currentCount;
        this.type = type;
        this.description = description;
    }

    public void incrementCurrentCount(int x) {
        currentCount += x;
        parent.checkAndUpdateCompletionStatus();
    }
    public void decrimentCurrentCount(int x) { 
        currentCount -= x;
        parent.checkAndUpdateCompletionStatus();
    }
    public int getCountNeeded() => countNeeded;
    public int getCurrentCount() => currentCount;
    public string getId() => (string)id.Clone();
    public string getDescription() => description;
    public ObjectiveType getType() => type;
    public bool isComplete() => currentCount >= countNeeded;

    //@TODO change this later. Seems like the usecases could be bad
    public string toString() {
        string objective = "";

        switch (type) { 
            case ObjectiveType.OBTAIN:
                objective += $"Get {countNeeded} {id}";
                break;
            case ObjectiveType.KILL:
                objective += $"Kill {id}";

                if (countNeeded > 1)
                    objective += $" {countNeeded} time";
                break;
        }
        if (description != null) return description;
        if (countNeeded > 1)
            objective += "s.";

        
        return objective;
    }
}