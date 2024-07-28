using TMPro;
using UnityEngine;

public class DetectObjectsInLayer : MonoBehaviour
{
    public float detectionRadius = 10f; // Raio de detecção ao redor do jogador
    public LayerMask detectionLayer; // Camada dos objetos a serem detectados
    public GameObject spherePrefab; // Prefab da esfera
    public GameObject largeSpherePrefab; // Prefab da esfera
    private GameObject largeDetectionSphere;
    public float largeDetectionRadius = 15f;
    private Vector3 origin;

    private GameObject detectionSphere; // Referência à esfera de detecção

    private GameManager gm;

    private AudioSource BombAudioSource;
    private AudioSource setBombAudioSource;

    public TextMeshProUGUI bombAmountTMP;

    private void Start()
    {
        this.gm = GetComponent<GameManager>();
        this.BombAudioSource = GetComponents<AudioSource>()[2];
        this.setBombAudioSource = GetComponents<AudioSource>()[3];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.gm.bombAmount > 0)
        {
            DetectObjects();
        }
    }

    void DetectObjects()
    {
        // Instanciar a esfera de detecção
        if (detectionSphere == null)
        {
            detectionSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            this.gm.bombAmount -= 1;
            this.bombAmountTMP.text = this.gm.bombAmount.ToString();
            this.origin = detectionSphere.transform.position;
            Destroy(detectionSphere, 1f); // Destruir a esfera após 1 segundo
            this.setBombAudioSource.Play();
            Invoke("playBombAudio", 0.8f);
            Invoke("CreateLargeDetectionSphere", 1f);

        }
    }
    void playBombAudio()
    {
        this.BombAudioSource.Play();
    }
    void CreateLargeDetectionSphere()
    {
        if (largeDetectionSphere == null)
        {
            largeDetectionSphere = Instantiate(largeSpherePrefab, this.origin, Quaternion.identity);
            Destroy(largeDetectionSphere, 0.3f); // Destruir a esfera maior após 1 segundo

            // Detectar inimigos na esfera maior
            Collider[] largeHitColliders = Physics.OverlapSphere(this.origin, largeDetectionRadius, detectionLayer);
            Debug.Log("Número de inimigos detectados na esfera maior: " + largeHitColliders.Length);

            foreach (Collider hitCollider in largeHitColliders)
            {
                // Verificar se é um inimigo e destruí-lo
                Debug.Log("Destruindo inimigo: " + hitCollider.gameObject.name);
                this.gm.IncreaseEnergyAmount(100);
                Destroy(hitCollider.gameObject);
            }
        }
    }
}
