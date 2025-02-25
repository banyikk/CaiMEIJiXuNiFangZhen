using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

public class RingChartControl : MonoBehaviour
{
    private RingChart ringChart;
    private Serie serie;
    private int data;
    private float time;
    void Start()
    {
        ringChart=GetComponent<RingChart>();
        serie = ringChart.GetSerie(0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>1f)
        {
            time = 0f;
            UpdateData();
        }
    }
    void UpdateData()
    {
        data= Random.Range(96, 100);
        serie.ClearData();
        serie.AddData(data, 100);
        ringChart.RefreshChart();
    }
}
