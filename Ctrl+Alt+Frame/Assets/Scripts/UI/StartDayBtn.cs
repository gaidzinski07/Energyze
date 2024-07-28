using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDayBtn : MonoBehaviour
{
    public GameEvent startDayEvent;

    public void startNewDay()
    {
        startDayEvent.Raise(this, null);
    }
}
