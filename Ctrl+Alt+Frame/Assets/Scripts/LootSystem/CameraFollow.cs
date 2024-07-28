using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Referência ao transform do jogador
    public Transform player;

    // Posição de offset da câmera em relação ao jogador
    public Vector3 offset;

    void Start()
    {
        // Calcula e armazena o offset inicial entre a câmera e o jogador
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Define a posição da câmera baseada na posição do jogador e no offset
        transform.position = player.position + offset;
    }
}
