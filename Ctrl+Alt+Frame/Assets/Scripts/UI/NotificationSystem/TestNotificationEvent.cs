using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotificationEvent : MonoBehaviour
{

    public GameEvent notificationEvent;

    public void sendSuccessNotification()
    {
        notificationEvent.Raise(this, new NotificationSetup("Sucesso!!! Algo deu certo aqui!", "success"));
    }

    public void sendWarningNotification()
    {
        notificationEvent.Raise(this, new NotificationSetup("Atenção!!! Algo pode dar errado...", "warning"));
    }

    public void sendDefaultNotification()
    {
        notificationEvent.Raise(this, new NotificationSetup("Informação: Alguma coisa é algo.", ""));
    }
}
