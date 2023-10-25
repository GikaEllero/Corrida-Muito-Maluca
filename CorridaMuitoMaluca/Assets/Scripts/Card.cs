using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool selected = false;

    public bool GetSelected(){
        return selected;
    }

    public void OnMouseDown(){
        if(GameManager.GetCardSelected() == null){
            transform.position += Vector3.up * 1f;
            selected = true;
        }
        else{
            if(selected){
                transform.position += Vector3.down * 1f;
                selected = false;
            }
        }    
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
