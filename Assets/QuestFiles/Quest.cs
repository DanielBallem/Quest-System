public abstract class Quest
{
    protected bool completed;
    protected bool activated;
    protected bool available;
    string title;
    string description;
    protected Quest parent;

    public Quest(string title, string description, bool available = false) : this(title, description, null, available) {}
    public Quest(string title, string description, Quest parent, bool available = false) {
        this.title = title;
        this.description = description;
        this.parent = parent;
        this.available = available;
        this.completed = false;
        this.activated = false;
    }

    public void markAsComplete()
    {
        completed = true;
    }

    //show quest when activated
    public void activate() => activated = true;
    public void deactivate() => activated = false;
    public void markAsAvailable() => available = true;
    public void markAsActivated() => activated = true;
    public void markAsUnavailable() => available = false;

    protected void updateParentCompletionStatus() {
        //quest completed. Check if parent is also complete
        if (completed) {
            if (parent != null) parent.checkAndUpdateCompletionStatus();
        }
    }

    public string getTitle() => new string(title);
    public string getDescription() => new string(description);
    public virtual bool isComplete() => completed;
    public virtual bool isActivated() => activated;
    public virtual bool isAvailable() => available;
    public void setParent(Quest p) => parent = p;
    public Quest getParent() => parent;
    public virtual void checkAndUpdateCompletionStatus() => updateParentCompletionStatus();
    public virtual string toString() => new string($"{title}: {description}");
}

