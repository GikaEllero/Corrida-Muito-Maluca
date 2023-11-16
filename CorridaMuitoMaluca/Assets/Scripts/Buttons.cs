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
        SceneManager.LoadScene("Historia");
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

    public void InicarJogo(){
        if(gameManager.VerificaHabilidades())
            SceneManager.LoadScene("Inicio");
    }

    public void SelecionaHabilidades(){
        SceneManager.LoadScene("SelecionaHabilidades");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Restart(){
    }

    public void UseSkill(){
        foreach (var item in GameManager.skills)
        {
            if(item.GetSelected()){
                if(!item.used){
                    Habilidades.UsaHabilidade(item);
                    SceneManager.LoadScene("Mapa");
                    break;
                }
            }
        }
    }
}
