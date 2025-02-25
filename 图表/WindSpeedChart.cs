using UnityEngine;
using XCharts.Runtime;

public class WindSpeedChart : MonoBehaviour
{
    private LineChart chart;
    private Serie windSpeedSerie;
    private float timeElapsed = 0f;
    private const float updateInterval = 1f; // 每秒更新一次
    private const int maxDataPoints = 5; // 最大数据点数量

    void Start()
    {
       
        // 获取LineChart组件
        chart = GetComponent<LineChart>();

        // 获取Series组件
        windSpeedSerie = chart.GetSerie(0);

        // 设置Series为平滑曲线
        windSpeedSerie.lineType = LineType.Smooth;

    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= updateInterval)
        {
            // 生成随机风速值，范围在0到5之间
            float windSpeed = Random.Range(0f, 5f);

            // 添加数据点
            windSpeedSerie.AddData(timeElapsed, windSpeed);

            // 如果数据点数量超过最大限制，移除最旧的数据点
            if (windSpeedSerie.dataCount > maxDataPoints)
            {
                windSpeedSerie.RemoveData(0); // 移除最旧的点
            }

            // 更新图表
            chart.RefreshChart();

            // 重置计时器
            timeElapsed = 0f;
        }
    }
}
