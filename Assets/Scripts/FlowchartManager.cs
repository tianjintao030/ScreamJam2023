using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartManager : MonoBehaviour
{
    public Flowchart flow_chart;
    public List<string> blocks = new List<string>();
    private string current_block_name;

    
    public void SetCurrentBlock(int i)
    {
        current_block_name = blocks[i];
    }

    public void ExecuteCurrentBlock()
    {
        flow_chart.ExecuteBlock(current_block_name);
    }

}
