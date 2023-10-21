using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[System.Serializable]
public class DragObjConfig
{
    public Drag drag;
    public Transform correctTF;
    public string block_name;
}

public class DragObjManager : MonoBehaviour
{
    public List<DragObjConfig> dragObjConfigs = new List<DragObjConfig>();

    private void Awake()
    {
        foreach(DragObjConfig config in dragObjConfigs)
        {
            config.drag.correct_trans = config.correctTF;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
