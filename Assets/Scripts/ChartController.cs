using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

public class ChartController : MonoBehaviour
{
    /// <summary>
    /// ゲームオブジェクト・コンポーネント
    /// -----------------------------------------
    /// </summary>

    //パブリック
    [Header("LineChart本体")]
    public GameObject LineChart;       //LineChart本体

    //プライベート
    private LineChart linechart;       //LineChartコンポーネント



    /// <summary>
    /// メソッド
    /// -----------------------------------------
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        linechart = LineChart.GetComponent<LineChart>();
        linechart.RemoveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}