using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    public TextMesh text;

    public void CardError(){
        text.text = "A carta escolhida tem \nque estar de acordo \ncom o terreno";
    }

    public void TurnError(){
        text.text = "Você deve jogar \n uma carta antes \nde finalizar o turno";
    }

    public void NoCard(){
        text.text = "Clique em uma carta \npara seleciona-la";
    }

    public void Blocked(){
        text.text = "Você não pode jogar\n uma carta após cair\n numa armadilha";
    }

    public void SkillsError(){
        text.text = "Você precisa\nselecionar\n4 habilidades";
    }

    public void Clear(){
        text.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
