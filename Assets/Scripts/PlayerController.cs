using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

[SerializeField]
private FloatingJoystick fj;
        [SerializeField]
        [Range (1,10)]
private  float speed;

[SerializeField]
private GameObject[] sonidos;

    [SerializeField]
private GameObject limiteD;
[SerializeField]
private GameObject limiteI;
[SerializeField]
private GameObject limiteInf;

[SerializeField]
private GameObject limiteSup;

[SerializeField]
private GameObject explosion;
[SerializeField]
private GameObject misilPlayer;

private int valorRR;
private int i;
private int valorRRMisil;

private float velocidadDisparo;

[SerializeField]
private GameObject acumulador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valorRR=0;
        valorRRMisil=0;
        velocidadDisparo = 1.0f;
        StartCoroutine(DispararMisil());
    }

IEnumerator DispararMisil()
    {
        while (true)
        {
            yield return new WaitForSeconds(velocidadDisparo);
            velocidadDisparo = velocidadDisparo - 0.005f;
            if (velocidadDisparo < 0.3f)
            {
                velocidadDisparo = 0.3f;
            }
            Disparar();
        }
    }

    public void Disparar()
    {
        sonidos[1].gameObject.GetComponent<AudioSource>().Play();
        misilPlayer.gameObject.transform.GetChild(valorRRMisil).gameObject.transform.position = this.gameObject.transform.position;
        misilPlayer.gameObject.transform.GetChild(valorRRMisil).gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0.0f,10.0f);
        
        
        valorRRMisil++;
        if (valorRRMisil >= misilPlayer.gameObject.transform.childCount)
        {
            valorRRMisil=0;
        }
    }

    // Update is called once per frame
    void Update()
    {
    this.transform.Translate(fj.Horizontal * Time.deltaTime * speed, fj.Vertical * Time.deltaTime * speed, 0.0f);

    if (this.transform.position.x > limiteD.transform.position.x)
        this.transform.position = new Vector2(limiteD.transform.position.x, this.transform.position.y);

    if (this.transform.position.x < limiteI.transform.position.x)
        this.transform.position = new Vector2(limiteI.transform.position.x, this.transform.position.y);

    if (this.transform.position.y < limiteInf.transform.position.y)
        this.transform.position = new Vector2(this.transform.position.x, limiteInf.transform.position.y);

    if (this.transform.position.y > limiteSup.transform.position.y)
        this.transform.position = new Vector2(this.transform.position.x, limiteSup.transform.position.y);
       // this.gameObject.transform.Translate(fj.Horizontal * Time.deltaTime * speed, fj.Vertical * Time.deltaTime *speed, 0.0f);
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

        if(!other.gameObject.CompareTag("MisilP")){
        sonidos[0].gameObject.GetComponent<AudioSource>().Play();
        explosion.gameObject.transform.GetChild(valorRR).gameObject.transform.position = this.gameObject.transform.position;
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
        Destroy(this.gameObject);
        }
    }

}
