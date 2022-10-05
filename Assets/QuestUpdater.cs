using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUpdater : MonoBehaviour
{
    protected QuestManager qm;
    public string eventId = "";
    // Start is called before the first frame update
    public virtual void Start()
    {
        qm = GameObject.FindGameObjectsWithTag("QuestManager")[0].GetComponent<QuestManager>();
    }

    protected void markEvent(string eventId) { 
        qm.markEvent(eventId);
    }
}
