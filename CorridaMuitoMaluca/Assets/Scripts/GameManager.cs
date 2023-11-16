using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Cards cards;
    private static TextUpdate text;
    public static List<(int idx, GameObject card)> playerCards = new List<(int, GameObject)>();
    public static List<(int idx, GameObject card)> mapCards = new List<(int, GameObject)>();
    public static List<int> freeIdxCards = new List<int>(); 
    public static List<Skill> skills = new List<Skill>(); 
    public static List<Npc> npcs = new List<Npc>();
    public static List<int> traps =  new List<int>();
    public static int atualPlayer = 0;
    private static bool firstCard = true;
    public static int dickPosition = 0;
    public static bool dickAlive = true;
    private string lastCard;
    public static bool playerBlocked = false;
    public static Traps trapsScript = new Traps();

    public static void DesativaSkill(){
        System.Random random = new System.Random();
        int num = random.Next(0, 4);
        if(skills.Where(w => !w.used).Any()){
            while(skills[num].used){
                num = random.Next(0, 4);
            }

            skills[num].used = true;
        }
    }

    public static void AtivaSkill(){
        System.Random random = new System.Random();
        int num = random.Next(0, 4);
        if(skills.Where(w => w.used).Any()){
            while(!skills[num].used){
                num = random.Next(0, 4);
            }

            skills[num].used = false;
        }
    }

    public bool VerificaHabilidades(){
        if(skills.Count == 4){
            return true;
        }
        else{
            text.SkillsError();
            return false;
        }
    }

    public int FirstPlace(){
        int pos = atualPlayer;

        foreach (var item in npcs)
        {
            if(item.idx > pos)
                pos = item.idx;
        }

        return pos;
    }

    public int LastPlace(){
        int pos = atualPlayer;

        foreach (var item in npcs)
        {
            if(item.idx < pos)
                pos = item.idx;
        }
        
        return pos;
    }

    public static bool CaiuTrap(int pos, bool npc){
        List<int> trap = new List<int>();
        if(npc)
            trap.AddRange(traps.Where(w => w == npcs[pos].idx).ToList());
        else
            trap.AddRange(traps.Where(w => w == atualPlayer).ToList());


        if(trap.Count > 0){
            traps.Remove(pos);
            trapsScript.GeraTrap(pos, npc);
            SceneManager.LoadScene("Armadilha");
            return true;
        }
        else{
            return false;
        }
    }

    public void MovimentaDick(string name){
        Debug.Log(name);
        if(dickAlive){
            int idx = -1;
        
            for (int i = 0; i < mapCards.Count; i++)
            {
                if(name.Contains(mapCards[i].card.name) && mapCards[i].idx > dickPosition){
                    if(traps.Count > 0){
                        if(!traps.Where(w => w == i).Any()){
                            idx = i;
                            break;
                        }
                    }
                    else{
                        idx = i;
                        break;
                    }
                }
            }

            if(idx == -1)
                dickAlive = false;
            
            dickPosition = idx;

            if(dickPosition > FirstPlace()){
                traps.Add(dickPosition);
                int pos = LastPlace() - 1;
                dickPosition = pos >= 0 ? pos : 0;
            }
        }
    }

    public static GameObject AtualMapa(){
        return mapCards[atualPlayer].card;
    }

    public void MovimentaNpcs(){
        for (int i = 0; i < npcs.Count; i++)
        {
            //StartCoroutine(Wait());
            AvancaNpc(i);
            if(CaiuTrap(i, true))
                npcs[i].blocked = true;
        }

        for (int i = 0; i < 2; i++)
        {
            var card = cards.TiraCarta();
            lastCard = card;

            for (int j = 0; j < npcs.Count; j++)
            {
                var npc = npcs[j];
                if(card.Contains(mapCards[npc.idx].card.name) && !npc.blocked){
                    //StartCoroutine(Wait());
                    AvancaNpc(j);
                    if(CaiuTrap(j, true))
                        npcs[j].blocked = true;
                }
                    
            }
        }
    }

    public void FinalizarTurno(){
        text.Clear();
        if(!firstCard && !playerBlocked){
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
            Debug.Log("Antes Npc " + lastCard);
            MovimentaDick(lastCard);
            Background.AtualizaBackground();
            Buttons.MapClick();
            MovimentaNpcs();
            Debug.Log("Depois Npc " + lastCard);
            MovimentaDick(lastCard);
            playerBlocked = false;
        }
        else{
            text.TurnError();
        }
        
    }

    public static void AvancaPlayer(bool ConsideraTrap){
        bool certo = false;

        while(!certo){
            atualPlayer++;
            if(npcs.Where(w => w.idx == atualPlayer).Count() <= 1){
                certo = true;
            }
        }
        
        if(ConsideraTrap){
            if(CaiuTrap(atualPlayer, false)){
                playerBlocked = true;
            }
        }
        else{
            if(traps.Count > 0){
                if(!traps.Where(w => w == atualPlayer).Any()){
                    while(!certo){
                        atualPlayer++;
                        if(npcs.Where(w => w.idx == atualPlayer).Count() <= 1){
                            certo = true;
                        }
                    }
                }
            }
        }
        

        if(atualPlayer >= 31)
            SceneManager.LoadScene("Ganhou");
    }

    public static void RecuaPlayer(){
        bool certo = false;

        while(!certo){
            atualPlayer--;
            if(npcs.Where(w => w.idx == atualPlayer).Count() <= 1){
                certo = true;
            }
        }
    }

    public static void AvancaNpc(int idx){
        bool certo = false;

        while(!certo){
            npcs[idx].idx++;
            if((atualPlayer != npcs[idx].idx && npcs.Where(w => w.idx == npcs[idx].idx).Count() <= 2) || 
                (atualPlayer == npcs[idx].idx && npcs.Where(w => w.idx == npcs[idx].idx).Count() <= 1)){
                certo = true;
            }
        }

        if(npcs[idx].idx >= 31)
            SceneManager.LoadScene("Perdeu");

    }

    public static void RecuaNpc(int idx){
        bool certo = false;

        while(!certo){
            npcs[idx].idx--;
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
            if(!playerBlocked){
                if(firstCard){
                    freeIdxCards.Add((int)cardIdx);
                    var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                    lastCard = card.card.name;
                    Destroy(card.card);
                    playerCards.Remove(card);
                    firstCard = false;
                    AvancaPlayer(true);
                    Background.AtualizaBackground();
                }
                else{
                    var card = playerCards.Where(w => w.idx == cardIdx).FirstOrDefault();
                    if(card.card.name.Contains(mapCards[atualPlayer].card.name)){
                        lastCard = card.card.name;
                        freeIdxCards.Add((int)cardIdx);
                        Destroy(card.card);
                        playerCards.Remove(card);
                        AvancaPlayer(true);
                        Background.AtualizaBackground();
                    }
                    else{
                        text.CardError();
                    }
                }
            }
            else{
                text.Blocked();
            }
            
        }
        else{
            text.NoCard();
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

        if(SceneManager.GetActiveScene().name == "SelecionaHabilidades"){
            text = GameObject.Find("Erro").GetComponent<TextUpdate>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
