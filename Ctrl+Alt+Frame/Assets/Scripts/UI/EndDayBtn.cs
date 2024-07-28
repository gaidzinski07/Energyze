using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayBtn : MonoBehaviour
{
    public GameEvent endDayEvent;

    public void onEndDay()
    {
        endDayEvent.Raise(this, null);
    }

}
