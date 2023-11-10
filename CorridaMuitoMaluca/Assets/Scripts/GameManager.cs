using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Cards cards;
    private static TextUpdate text;
    public static List<(int idx, GameObject card)> playerCards = new List<(int, GameObject)>();
    public static List<(int idx, GameObject card)> mapCards = new List<(int, GameObject)>();
    public static List<int> freeIdxCards = new List<int>(); 
    public static List<Npc> npcs = new List<Npc>(); 
    public static int atualPlayer = 0;
    private static bool firstCard = true;

    public static GameObject AtualMapa(){
        return mapCards[atualPlayer].card;
    }

    public void MovimentaNpcs(){
        for (int i = 0; i < npcs.Count; i++)
        {
            AvancaNpc(i);
        }

        for (int i = 0; i < 2; i++)
        {
            var card = cards.TiraCarta();
            Debug.Log(card);

            for (int j = 0; j < npcs.Count; j++)
            {
                var npc = npcs[j];
                if(card.Contains(mapCards[npc.idx].card.name)){
                    AvancaNpc(j);
                }
                    
            }
        }
    }

    public void FinalizarTurno(){
        text.Clear();
        if(!firstCard){
            if(playerCards.Count < 3){
                foreach (var item in freeIdxCards.ToList())
                {
                    freeIdxCards.Remove(item);
                    switch (item)
                    {
                        case 1:
                            cards.GeraCarta(-3.8f, -4.7f, 1);
                            break;
                        case 2:
                            cards.GeraCarta(-0.5f, -4.7f, 2);
                            break;
                        case 3:
                            cards.GeraCarta(2.8f, -4.7f, 3);
                            break;
                    }
                }
            }

            firstCard = true;
            Background.AtualizaBackground();
            Buttons.MapClick();
            MovimentaNpcs();
        }
        else{
            text.TurnError();
        }
        
    }

    public void AvancaPlayer(){
        bool certo = false;

        while(!certo){
            atualPlayer++;
            if(npcs.Where(w => w.idx == atualPlayer).Count() <= 1){
                certo = true;
            }
        }
        
    }

    public void AvancaNpc(int idx){
        bool certo = false;

        while(!certo){
            npcs[idx].idx++;
            if((atualPlayer != npcs[idx].idx && npcs.Where(w => w.idx == npcs[idx].idx).Count() <= 2) || 
                (atualPlayer == npcs[idx].idx && npcs.Where(w => w.idx == npcs[idx].idx).Count() <= 1)){
                certo = true;
            }
        }
    }

    public void SelecionarCarta(){
        var cardIdx = GetCardSelected();
        text.Clear();
        if(cardIdx != null){
            if(firstCard){
                freeIdxCards.Add((int)cardIdx);
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                Destroy(card.card);
                playerCards.Remove(card);
                firstCard = false;
                AvancaPlayer();
                Background.AtualizaBackground();
            }
            else{
                var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                if(card.card.name.Contains(mapCards[atualPlayer].card.name)){
                    freeIdxCards.Add((int)cardIdx);
                    Destroy(card.card);
                    playerCards.Remove(card);
                    AvancaPlayer();
                    Background.AtualizaBackground();
                }
                else{
                    text.CardError();
                }
            }
        }
    }

    public static int? GetCardSelected(){
        foreach (var item in playerCards)
        {
            if(item.card.GetComponent<Card>().GetSelected())
                return item.idx;
        }
        return null;
    }

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(mapCards.Count == 0){
            Map.IniciaMapa();
        }
        
        if(SceneManager.GetActiveScene().name == "Inicio"){
            cards = GameObject.Find("Cards").GetComponent<Cards>();
            text = GameObject.Find("Text").GetComponent<TextUpdate>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
