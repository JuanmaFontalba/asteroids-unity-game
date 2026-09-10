using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DmgItemController : MonoBehaviour
{

    [SerializeField]
    private GameObject limiteD;
    [SerializeField]
    private GameObject limiteI;
    [SerializeField]
    private GameObject[] listadoDmgItems;
    private int listaAleatoria;
    private int[] valorRR;
      private float posicionXRandom;

      private float velocidadAparicion;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidadAparicion = 2.0f;
        valorRR = new int[listadoDmgItems.Length];
        StartCoroutine(CambioDePosición());
        //InvokeRepeating("CambiarPosiciones",0.0f,2.0f);
    }

    // Update is called once per frame
   
   IEnumerator CambioDePosición()
    {
        while (true)
        {
            yield return new WaitForSeconds(velocidadAparicion);
            velocidadAparicion = velocidadAparicion - 0.01f;
            if (velocidadAparicion < 0.5f)
            {
                velocidadAparicion = 0.5f;
            }
            CambiarPosiciones();
        }
    }
   
    public void CambiarPosiciones()
    {
        posicionXRandom = Random.Range(limiteI.gameObject.transform.position.x, limiteD.gameObject.transform.position.x);
        listaAleatoria = Random.Range(0,listadoDmgItems.Length);
    
        listadoDmgItems[listaAleatoria].gameObject.transform.GetChild(valorRR[listaAleatoria]).gameObject.transform.position= new Vector2(posicionXRandom,limiteI.gameObject.transform.position.y);
        listadoDmgItems[listaAleatoria].gameObject.transform.GetChild(valorRR[listaAleatoria]).gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
       listadoDmgItems[listaAleatoria].gameObject.transform.GetChild(valorRR[listaAleatoria]).gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.02f;
       //Error transform child out of bound¿?
        valorRR[listaAleatoria]++;
       
       if (valorRR[listaAleatoria] >= listadoDmgItems[listaAleatoria].gameObject.transform.childCount)
        {
            valorRR[listaAleatoria]=0;
        }
         
        
    }
}
