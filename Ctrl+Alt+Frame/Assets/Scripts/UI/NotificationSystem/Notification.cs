using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationSetup
{
    public string message;
    public string type;

    public NotificationSetup(string message, string type)
    {
        this.message = message;
        this.type = type;
    }
}

public class Notification : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public float lifeTime;
    public Animator anim;
    public NotificationBox parent;
    public Color successColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color defaultColor = Color.grey;

    public void Setup(NotificationSetup setup)
    {
        text.text = setup.message;
        StartCoroutine(dieCoroutine());

        switch (setup.type)
        {
            case "success":
                image.color = successColor;
                break;
            case "warning":
                image.color = warningColor;
                text.color = Color.black;
                break;
            default:
                image.color = defaultColor;
                break;
        }

    }

    public IEnumerator dieCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        anim.SetTrigger("Die");
        Destroy(gameObject, 0.2f);
        parent.removeNotification(gameObject);
    }

}
