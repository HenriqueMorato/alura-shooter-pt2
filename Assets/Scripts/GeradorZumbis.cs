using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour {

    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public float DistanciaDoJogadorGeracao = 22;
    private Transform player;
    private float DistanciaDeGeracao = 3;
    private float quantidadeMaximaDeZumbis = 2;
    private float quantidadeDeZumbis;
    private float tempoAumentoDificuldade = 30;
    private float contadorDifuculdade = 0;
    public AudioClip SomDeGrunhido;
    public LayerMask layerZumbi;

    private const float PorcentagemDeGerarComSom = 0.1f;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag(Tags.Jogador).transform;
        for (int i = 0; i < quantidadeMaximaDeZumbis; i++) {
            StartCoroutine(GerarZumbiNovo(false));
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(Vector3.Distance(transform.position, player.position) > DistanciaDoJogadorGeracao)
        {
            contadorTempo += Time.deltaTime;

            if(contadorTempo >= TempoGerarZumbi && quantidadeDeZumbis < quantidadeMaximaDeZumbis)
            {
                StartCoroutine(GerarZumbiNovo(true));
                contadorTempo = 0;
            }
        }

        if(Time.timeSinceLevelLoad > contadorDifuculdade)
        {
            contadorDifuculdade = Time.timeSinceLevelLoad + tempoAumentoDificuldade;
            quantidadeMaximaDeZumbis++;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeGeracao);
    }

    IEnumerator GerarZumbiNovo (bool utilizarSom)
    {
        Vector3 positionToInstantiate = AleatorizarPosicao();
        Collider[] hitColliders = Physics.OverlapSphere(positionToInstantiate, 1, layerZumbi);;

        while (hitColliders.Length > 0)
        {
            positionToInstantiate = AleatorizarPosicao();
            hitColliders = Physics.OverlapSphere(positionToInstantiate, 1, layerZumbi);
            yield return null;
        }
 
        ControlaInimigo zumbi = Instantiate(Zumbi, positionToInstantiate, Quaternion.identity).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeDeZumbis++;
        if(Random.value < PorcentagemDeGerarComSom && utilizarSom)
        {
            ControlaAudio.instancia.PlayOneShot(SomDeGrunhido);
        }
    }

    public void DiminuiQtdZumbis ()
    {
        quantidadeDeZumbis--;
    }

    Vector3 AleatorizarPosicao ()
	{
		Vector3 posicaoAleatoria = transform.position + (Random.insideUnitSphere * DistanciaDeGeracao);
		posicaoAleatoria.y = 0;

		return posicaoAleatoria;
	}
}
