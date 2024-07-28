using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.AI;

public class Nave : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Animator anim;
    [Range(1, 1000)]
    public int velocidadeMaxima = 2;
    [Range(0, 10)]
    public float movementSmooth;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        MovePlayer();
    }


    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 moveInput = new Vector3(direction.x, 0, direction.y);

        if(!moveInput.Equals(Vector3.zero))
        {
            //rotaciona para direção do movimento
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveInput), movementSmooth * Time.deltaTime);
            Vector3 newRot = new Vector3(0, transform.eulerAngles.y, 0);
            transform.eulerAngles = newRot;
            anim.SetBool("Moving", true);

            Vector3 newPosition = transform.position + moveInput * Time.deltaTime * velocidadeMaxima;
            newPosition.y = 0;

            NavMeshHit hit;
            bool isValid = NavMesh.SamplePosition(newPosition, out hit, .3f, NavMesh.AllAreas);

            if (isValid)
            {
                if ((transform.position - hit.position).magnitude >= 0.2f)
                {
                    newPosition = hit.position; //moveInput * Time.deltaTime * velocidadeMaxima;
                    newPosition.y = transform.position.y;
                    transform.position = newPosition;
                }
            }

            return;
        }
        anim.SetBool("Moving", false);

    }

}
