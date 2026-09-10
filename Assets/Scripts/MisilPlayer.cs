using UnityEngine;

public class MisilPlayer : MonoBehaviour
{
    private int posDestruccion;
    private int incremento;

    [SerializeField]
    private GameObject explosionMisil;

    [SerializeField]
    private GameObject[] sonidos;

    private int valorRR;
    private int i;

    [SerializeField]
    private GameObject acumulador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valorRR=0;
        posDestruccion = 300;
        incremento = 5;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OcultarExplosion()
    {
        for (i=0; i<explosionMisil.gameObject.transform.childCount; i++)
        {
            explosionMisil.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Mundo")){
        acumulador.gameObject.GetComponent<UIController>().ActualizarPuntuacion(1);
        sonidos[0].gameObject.GetComponent<AudioSource>().Play();
        explosionMisil.gameObject.transform.GetChild(valorRR).gameObject.transform.position = other.gameObject.transform.position;
        explosionMisil.gameObject.transform.GetChild(valorRR).gameObject.SetActive(true);
        Invoke("OcultarExplosion", 1.0f);
        valorRR++;
        if (valorRR >= explosionMisil.gameObject.transform.childCount)
        {
            valorRR=0;
        }
        other.gameObject.transform.position = new Vector2(posDestruccion, 300);
        this.gameObject.transform.position = new Vector2(posDestruccion, 400);
        posDestruccion = posDestruccion + incremento;
       }
    }
}
