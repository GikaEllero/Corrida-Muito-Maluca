using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void SkillClick(){
        foreach (var item in GameManager.playerCards)
        {
            item.card.SetActive(false);
        }
        SceneManager.LoadScene("Skills");
    }

    public void MapClick(){
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
        GameManager.SelecionarCarta();
    }

    public void TurnoClick(){
        GameManager.FinalizarTurno();
    }
}
