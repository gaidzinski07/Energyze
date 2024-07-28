using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationBox : MonoBehaviour
{
    public GameObject notificationPrefab;
    private List<GameObject> notifications;
    public int notificationLimit;
    public float startHeight;
    public float notificationOffset;
    public int lastIndex;

    private void Start()
    {
        notifications = new List<GameObject>();
        for (int i = 0; i < notificationLimit; i++)
        {
            notifications.Add(null);
        }
        startHeight = GetComponent<RectTransform>().rect.width - notificationOffset;
    }

    public void onNotificationHappened(Component sender, object data)
    {
        if(data is NotificationSetup)
        {
            addNotification((NotificationSetup)data);
        }
    }

    public void addNotification(NotificationSetup setup)
    {
        int indexOf = notifications.IndexOf(null);

        if(indexOf != -1)
        {
            GameObject go = Instantiate(notificationPrefab, transform);
            notifications[indexOf] = go;
            Notification n = go.GetComponent<Notification>();
            n.Setup(setup);
            n.parent = this;
            float height = startHeight - (notificationOffset * (indexOf + 1));
            go.transform.localPosition = new Vector3(0, height, 0);
        }
    }

    public void removeNotification(GameObject not)
    {
        if (notifications.Contains(not))
        {
            notifications[notifications.IndexOf(not)] = null;
        }
    }

}
