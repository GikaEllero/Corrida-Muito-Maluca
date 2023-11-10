using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameManager gameManager = new GameManager();

    public void SkillClick(){
        foreach (var item in GameManager.playerCards)
        {
            item.card.SetActive(false);
        }
        SceneManager.LoadScene("Skills");
    }

    public static void MapClick(){
        foreach (var item in GameManager.playerCards)
        {
            item.card.SetActive(false);
        }
        SceneManager.LoadScene("Mapa");
    }

    public void StartClick(){
        SceneManager.LoadScene("Inicio");
    }

    public void BackClick(){
        SceneManager.LoadScene("Inicio");
    }

    public void SelecionaClick(){
        gameManager.SelecionarCarta();
    }

    public void TurnoClick(){
        gameManager.FinalizarTurno();
    }
}
