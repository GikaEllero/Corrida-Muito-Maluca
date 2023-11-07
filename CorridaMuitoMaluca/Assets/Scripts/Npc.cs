using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject prefab;
    public int idx;

    public Npc(int idx, GameObject prefab)
    {
        this.prefab = prefab;
        this.idx = idx;
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
