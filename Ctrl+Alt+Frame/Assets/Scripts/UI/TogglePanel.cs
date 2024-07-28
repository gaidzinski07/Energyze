using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    // Referência ao GameObject que queremos abrir/fechar
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }
    void Update()
    {
        // Verifica se a tecla P foi pressionada
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Se o painel está ativo, desativa. Se está desativado, ativa.
            panel.SetActive(!panel.activeSelf);
        }
        // Verifica se a tecla Esc foi pressionada e o painel está ativo
        else if (Input.GetKeyDown(KeyCode.Escape) && panel.activeSelf)
        {
            // Desativa o painel
            panel.SetActive(false);
        }
    }
}
