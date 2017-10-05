using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class ControlaChefe : MonoBehaviour, IMatavel 
{
	private NavMeshAgent agente;
	private Transform jogador;
	private AnimacaoPersonagem animacaoChefe;
	private MovimentoPersonagem movimentoChefe;
	private Status status;
	public GameObject KitMedicoPrefab;

	[Header("Vida Chefe")]
	public Image SliderImagem;
	private Slider sliderVidaChefe;
	public Color CorVidaMaxima, CorVidaMinima;


	void Start()
	{
		status = GetComponent<Status>();
		agente = GetComponent<NavMeshAgent>();
		agente.speed = status.Velocidade;
		jogador = GameObject.FindWithTag(Tags.Jogador).transform;
		animacaoChefe = GetComponent<AnimacaoPersonagem>();
		movimentoChefe = GetComponent<MovimentoPersonagem>();
		sliderVidaChefe = transform.GetComponentInChildren<Slider>();
		sliderVidaChefe.value = sliderVidaChefe.maxValue = status.VidaInicial;
		AtualizarInterface();
	}

	void Update()
	{
		agente.destination = jogador.position;
		animacaoChefe.AnimarMovimento(agente.velocity);

		if(!agente.pathPending)
		{
			if(agente.remainingDistance <= agente.stoppingDistance)
			{
				animacaoChefe.Atacar(true);
				Vector3 direcao = jogador.position - transform.position;
				movimentoChefe.Rotacionar(direcao);	
			}	
			else
			{
				animacaoChefe.Atacar(false);
			}
		}	
	}

	void BateNoJogador ()
    {
		int dano = Random.Range(40,60);
        jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

	public void TomarDano(int dano)
	{
		status.Vida -= dano;
		AtualizarInterface();
		if(status.Vida <= 0)
		{
			Morrer();
		}
	}

	void AtualizarInterface ()
	{
		sliderVidaChefe.value = status.Vida;

		SliderImagem.color = Color.Lerp(CorVidaMinima, CorVidaMaxima, status.Vida / status.VidaInicial);
	}

    public void Morrer()
    {
        movimentoChefe.CairPeloChao();
        animacaoChefe.Morte();
        Destroy(gameObject, 2);
		agente.enabled = false;
        this.enabled = false;
        //ControlaAudio.instancia.PlayOneShot(SomMorte);
        ControlaInterface.instancia.MorteZumbiInterface(10);

        GameObject kitMedico = Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(kitMedico, 5);
    }
}
