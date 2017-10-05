using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ControlaInterface : MonoBehaviour 
{
	public Slider sliderVidaJogador;
	private ControlaJogador controlaJogador;
	public static ControlaInterface instancia;
	public GameObject GameOverPanel;
	public Text TextTempoSobrevivencia;
	private int qtdMorteZumbis = 0;
	public Text TextQtdMorteZumbis;
	public Text TextQtdMorteZumbisGameOver;
	public Text TextChefeAparece;

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
		TextTempoSobrevivencia.text = "Você sobreviveu " + Mathf.Floor(Time.timeSinceLevelLoad/60) + " min e " + Mathf.Floor(Time.timeSinceLevelLoad%60) + " segundos";
		StartCoroutine(MostrarObjeto(GameOverPanel, 1));
		StartCoroutine(IncrementaValorAte(qtdMorteZumbis, 2));				
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

	public void MorteZumbiInterface(int score)
	{
		qtdMorteZumbis += score;
		TextQtdMorteZumbis.text = "x " + qtdMorteZumbis;		
	}

	IEnumerator IncrementaValorAte(int valor, int duracao)
	{
		yield return new WaitForSecondsRealtime(1);
		int min = 0;
		float relogio = 0;
		while(min <= valor)
		{
			relogio += Time.unscaledDeltaTime;		
			float progresso = relogio / duracao;
			min = (int)Mathf.Lerp(0, valor, progresso);
			TextQtdMorteZumbisGameOver.text = min.ToString();
			yield return null;
		}
	}

	public void TextoChefeAparece ()
	{
		StartCoroutine(DesaparecerTexto(2, TextChefeAparece));
	}

	public void TrocarCenaJogo ()
	{
		SceneManager.LoadScene("game");
	}

	IEnumerator DesaparecerTexto (float tempoDeSumico, Text text)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		text.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);	
		while(text.color.a > 0)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / tempoDeSumico));
			if(text.color.a <= 0)
			{
				text.gameObject.SetActive(false);
			}
			yield return null;
		}
	}
}
