using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public Transform nave;
    public float naveSpeed;
    public float rotSpeed = 3;

    void Update()
    {
        Vector3 rot = nave.transform.localEulerAngles;
        rot.y += rotSpeed * Time.deltaTime;
        nave.transform.localEulerAngles = rot;
        nave.Translate(Vector3.forward * Time.deltaTime * naveSpeed);
    }

    public void Terminar()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
