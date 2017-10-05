using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public float DistanciaDoJogadorGeracao = 22;
    private Transform player;
    private float DistanciaGeracao = 3;
    private float qtdMaxZumbis = 2;
    private float qtdZumbis;
    private float tempoAumentoDificuldade = 30;
    private float contadorDifuculdade = 0;
    public AudioClip SomGrunhido;
    public LayerMask layerZumbi;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag(Tags.Jogador).transform;
        for (int i = 0; i < qtdMaxZumbis; i++) {
            GerarZumbiNovo(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector3.Distance(transform.position, player.position) > DistanciaDoJogadorGeracao)
        {
            contadorTempo += Time.deltaTime;

            if(contadorTempo >= TempoGerarZumbi && qtdZumbis < qtdMaxZumbis)
            {
                GerarZumbiNovo(true);
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorDifuculdade)
        {
            contadorDifuculdade = Time.timeSinceLevelLoad + tempoAumentoDificuldade;
            qtdMaxZumbis++;
        }
    }

    void GerarZumbi (bool sound)
    {
        ControlaInimigo zumbi = Instantiate(Zumbi, AleatorizarPosicao(), transform.rotation).GetComponent<ControlaInimigo>(); 
        zumbi.meuGerador = this;
        qtdZumbis++;
        if(Random.value < 0.1 && sound)
        {
            ControlaAudio.instancia.PlayOneShot(SomGrunhido);
        }
    }

    void GerarZumbiNovo (bool sound)
    {
        Vector3 positionToInstantiate;
        Collider[] hitColliders;
     
        do
        {
            positionToInstantiate = AleatorizarPosicao();
            hitColliders = Physics.OverlapSphere(positionToInstantiate, 1, layerZumbi);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log(hitColliders[i].name);
            }            
        } while (hitColliders.Length > 0);
 
        ControlaInimigo zumbi = Instantiate(Zumbi, positionToInstantiate, Quaternion.identity).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        qtdZumbis++;
        if(Random.value < 0.1 && sound)
        {
            ControlaAudio.instancia.PlayOneShot(SomGrunhido);
        }
    }

    public void DiminuiQtdZumbis ()
    {
        qtdZumbis--;
    }

    Vector3 AleatorizarPosicao ()
	{
		Vector3 posicaoAleatoria = transform.position + (Random.insideUnitSphere * DistanciaGeracao);
		posicaoAleatoria.y = 0;

		return posicaoAleatoria;
	}
}
