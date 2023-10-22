using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FlowchartManager : MonoSingleton<FlowchartManager>
{
    public Flowchart flow_chart;

    public void ExecuteBlockByName(string _name)
    {
        flow_chart.ExecuteBlock(_name);
    }
    

}
