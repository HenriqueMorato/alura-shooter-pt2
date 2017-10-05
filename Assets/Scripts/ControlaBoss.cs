using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class ControlaBoss : MonoBehaviour 
{
	private NavMeshAgent agente;
	private Transform jogador;
	private AnimacaoPersonagem animacaoPersonagem;
	private MovimentoPersonagem movimentoPersonagem;
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
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
		movimentoPersonagem = GetComponent<MovimentoPersonagem>();
		sliderVidaChefe = transform.GetComponentInChildren<Slider>();
		sliderVidaChefe.value = sliderVidaChefe.maxValue = status.VidaInicial;
		AtualizarInterface();
	}

	void Update()
	{
		agente.destination = jogador.position;
		animacaoPersonagem.AnimarMovimento(agente.velocity);

		if(!agente.pathPending)
		{
			if(agente.remainingDistance <= agente.stoppingDistance)
			{
				animacaoPersonagem.Atacar(true);
				Vector3 direcao = jogador.position - transform.position;
				movimentoPersonagem.Rotacionar(direcao);	
			}	
			else
			{
				animacaoPersonagem.Atacar(false);
			}
		}	
	}

	void DanoJogador ()
    {
        jogador.GetComponent<ControlaJogador>().TomarDano(Random.Range(40,60));
    }

	public void TomarDano(int dano)
	{
		status.Vida -= dano;
		AtualizarInterface();
		if(status.Vida <= 0)
		{
			MorteChefe();
		}
	}

	void MorteChefe ()
	{
		movimentoPersonagem.CairPeloChao();
        animacaoPersonagem.Morte();
        Destroy(gameObject, 2);
        movimentoPersonagem.enabled = false;
		agente.enabled = false;
        this.enabled = false;
        //ControlaAudio.instancia.PlayOneShot(SomMorte);
        ControlaInterface.instancia.MorteZumbiInterface(10);

        GameObject kitMedico = Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(kitMedico, 5);
	}

	void AtualizarInterface ()
	{
		sliderVidaChefe.value = status.Vida;

		SliderImagem.color = Color.Lerp(CorVidaMinima, CorVidaMaxima, status.Vida / status.VidaInicial);
	}
}
