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
