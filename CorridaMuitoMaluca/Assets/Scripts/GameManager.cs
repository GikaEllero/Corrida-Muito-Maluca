using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> playerCards = new List<GameObject>();
    public static List<GameObject> mapCards = new List<GameObject>();
    private int AtualPlayer;

    public void Anda(){
        AtualPlayer++;
    }

    // Start is called before the first frame update
    void Start()
    {
        AtualPlayer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
