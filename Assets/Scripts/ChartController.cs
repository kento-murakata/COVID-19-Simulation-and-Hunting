using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;

public class ChartController : MonoBehaviour
{
    /// <summary>
    /// �Q�[���I�u�W�F�N�g�E�R���|�[�l���g
    /// -----------------------------------------
    /// </summary>

    //�p�u���b�N
    [Header("LineChart�{��")]
    public GameObject LineChart;       //LineChart�{��

    //�v���C�x�[�g
    private LineChart linechart;       //LineChart�R���|�[�l���g



    /// <summary>
    /// ���\�b�h
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