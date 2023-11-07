using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcs : MonoBehaviour
{
    public List<GameObject> npcs;

    public void EscolheNpcs(){
        System.Random random = new System.Random();
        int total = npcs.Count;
        for (int i = 1; i < 6; i++)
        {
            int num = random.Next(0, total);
            GameManager.npcs.Add(new Npc(i, npcs[num]));
            npcs.RemoveAt(num);
            total--;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.npcs.Count <= 0)
            EscolheNpcs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
