using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<(int idx, GameObject card)> playerCards = new List<(int, GameObject)>();
    public static List<GameObject> mapCards = new List<GameObject>();
    public static List<int> freeIdx = new List<int>(); 
    private int AtualPlayer;

    public static void SelecionarCarta(){
        var cardIdx = GetCardSelected();
        if(cardIdx != null){
            freeIdx.Add((int)cardIdx);
            var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
            Destroy(card.card);
            playerCards.Remove(card);
        }
    }

    public static int? GetCardSelected(){
        foreach (var item in playerCards)
        {
            if(item.card.GetComponent<Card>().GetSelected()){
                return item.idx;
            }
        }
        return null;
    }

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
