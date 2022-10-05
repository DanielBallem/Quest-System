using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : QuestUpdater
{
    private void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            markEvent(eventId);
        }
    }
}
