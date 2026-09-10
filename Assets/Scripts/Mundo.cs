using UnityEngine;

public class Mundo : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

[SerializeField]
private GameObject acumulador;

private int valorRR;
private int i;

 [SerializeField]
private GameObject sonido;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valorRR=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
private void OcultarExplosion()
    {
        for (i=0; i<explosion.gameObject.transform.childCount; i++)
        {
            explosion.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        sonido.gameObject.GetComponent<AudioSource>().Play();
        explosion.gameObject.transform.GetChild(valorRR).gameObject.transform.position = other.gameObject.transform.position;
        explosion.gameObject.transform.GetChild(valorRR).gameObject.SetActive(true);
        Invoke("OcultarExplosion", 0.3f);
        valorRR++;
        if (valorRR >= explosion.gameObject.transform.childCount)
        {
            valorRR=0;
        }

        Debug.Log ("Chocando con "+ other.gameObject);
    
        Destroy(other.gameObject);
        acumulador.gameObject.GetComponent<UIController>().MostrarPanelDerrota();
        //Destroy(this.gameObject);
    }
}
