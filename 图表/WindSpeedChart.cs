using UnityEngine;
using XCharts.Runtime;

public class WindSpeedChart : MonoBehaviour
{
    private LineChart chart;
    private Serie windSpeedSerie;
    private float timeElapsed = 0f;
    private const float updateInterval = 1f; // ÿ�����һ��
    private const int maxDataPoints = 5; // ������ݵ�����

    void Start()
    {
       
        // ��ȡLineChart���
        chart = GetComponent<LineChart>();

        // ��ȡSeries���
        windSpeedSerie = chart.GetSerie(0);

        // ����SeriesΪƽ������
        windSpeedSerie.lineType = LineType.Smooth;

    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= updateInterval)
        {
            // �����������ֵ����Χ��0��5֮��
            float windSpeed = Random.Range(0f, 5f);

            // ������ݵ�
            windSpeedSerie.AddData(timeElapsed, windSpeed);

            // ������ݵ���������������ƣ��Ƴ���ɵ����ݵ�
            if (windSpeedSerie.dataCount > maxDataPoints)
            {
                windSpeedSerie.RemoveData(0); // �Ƴ���ɵĵ�
            }

            // ����ͼ��
            chart.RefreshChart();

            // ���ü�ʱ��
            timeElapsed = 0f;
        }
    }
}
