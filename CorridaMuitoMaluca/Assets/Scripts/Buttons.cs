using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void SkillClick(){
        SceneManager.LoadScene("Skills");
    }

    public void MapClick(){
        SceneManager.LoadScene("Mapa");
    }

    public void StartClick(){
        SceneManager.LoadScene("Inicio");
    }

    public void BackClick(){
        SceneManager.LoadScene("Inicio");
    }
}
