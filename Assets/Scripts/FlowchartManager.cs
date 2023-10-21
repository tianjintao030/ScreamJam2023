using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartManager : MonoSingleton<FlowchartManager>
{
    public Flowchart flow_chart;
    public List<string> blocks = new List<string>();

    private void Start()
    {
        ExecuteBlockByIndex(0);
    }

    public void ExecuteBlockByIndex(int i)
    {
        flow_chart.ExecuteBlock(blocks[i]);
    }
    
}
