using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControlaJogo : MonoBehaviour 
{
	public Slider sliderVidaJogador;
	private ControlaJogador controlaJogador;
	public static ControlaJogo instancia;
	public GameObject GameOverPanel;
	public Text TextTempoSobrevivencia;
	private int qtdMorteZumbis = 0;
	public Text TextQtdMorteZumbis;
	public Text TextQtdMorteZumbisGameOver;
	

	void Start()
	{
		controlaJogador = GameObject.FindObjectOfType(typeof(ControlaJogador)) as ControlaJogador;

		sliderVidaJogador.maxValue = controlaJogador.status.Vida;

		instancia = this;
		Time.timeScale = 1;
	}

	public void AtualizaSliderVidaJogador ()
	{
		sliderVidaJogador.value = controlaJogador.status.Vida;
	}

	public void GameOver ()
	{
		Time.timeScale = 0;
		TextTempoSobrevivencia.text = "Você sobreviveu " + Mathf.Floor(Time.time/60) + " min e " + Mathf.Floor(Time.time%60) + "segundos";
		StartCoroutine(MostrarObjeto(GameOverPanel, 1));
		StartCoroutine(IncrementaValorAte(qtdMorteZumbis));				
	}

	IEnumerator MostrarObjeto(GameObject obj, float tempo)
	{
		yield return new WaitForSecondsRealtime(1);
		obj.SetActive(true);
	}

	public void MorteZumbiInterface()
	{
		qtdMorteZumbis++;
		TextQtdMorteZumbis.text = "x " + qtdMorteZumbis;
	}

	IEnumerator IncrementaValorAte(int valor)
	{
		yield return new WaitForSecondsRealtime(1);
		int min = 0;
		float relogio = 0;
		while(min <= valor)
		{
			relogio += Time.unscaledDeltaTime;		
			float progresso = relogio / 2;
			min = (int)Mathf.Lerp(0, valor, progresso);
			TextQtdMorteZumbisGameOver.text = min.ToString();
			yield return null;
		}

		StopCoroutine("IncrementaValorAte");
	}

	public void ReiniciarJogo ()
	{
		SceneManager.LoadScene("game");
	}
}
