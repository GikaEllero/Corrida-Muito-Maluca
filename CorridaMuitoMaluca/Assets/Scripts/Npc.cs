using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Npc : MonoBehaviour
{
    public GameObject prefab;
    public int idx;
    public bool blocked;

    public Npc(int idx, GameObject prefab)
    {
        this.prefab = prefab;
        this.idx = idx;
        this.blocked = false;
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
