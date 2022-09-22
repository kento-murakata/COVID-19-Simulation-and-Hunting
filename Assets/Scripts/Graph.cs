using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Image view;
    public int verticalCount = 10;
    public float horizontalOffsetYAxis = 90.0f;
    public float verticalOffsetYAxis = 20.0f;
    public float horizontalOffsetXAxis = 0.0f;
    public float verticalOffsetXAxis = 0.0f;

    private RectTransform m_rtView;
    private RectTransform m_templateLabelX;
    private RectTransform m_templateLabelY;

    private RectTransform m_templateBarVertical;
    private RectTransform m_templateBarHorizontal;

    public void MakeGraph()
    {
        //Unityで作成した "view" のRectTransformオブジェクト取得
        //m_rtView = transform.Find("view").GetComponent<RectTransform>();
        m_rtView = view.GetComponent<RectTransform>();
        RectTransform graphSystem = view.transform.parent.GetComponent<RectTransform>();
        graphSystem.gameObject.SetActive(true);


        //try
        //{
        //    RectTransform[] dots = m_rtView.transform.Find("dot").GetComponents<RectTransform>();
        //    foreach(RectTransform dot in dots)
        //    {
        //        Destroy(dot);
        //    }

        //    RectTransform[] labelXs = m_rtView.transform.Find("labelX(Clone)").GetComponents<RectTransform>();
        //    foreach( RectTransform labelX in labelXs)
        //    {
        //        Destroy(labelX);
        //    }

        //    RectTransform[] labelYs = m_rtView.transform.Find("labelY(Clone)").GetComponents<RectTransform>();
        //    foreach (RectTransform labelY in labelYs)
        //    {
        //        Destroy(labelY);
        //    }

        //    RectTransform[] barVerticals = m_rtView.transform.Find("barVertical(Clone)").GetComponents<RectTransform>();
        //    foreach (RectTransform barVertical in barVerticals)
        //    {
        //        Destroy(barVertical);
        //    }

        //    RectTransform[] barHorizontals = m_rtView.transform.Find("barHorizontal(Clone)").GetComponents<RectTransform>();
        //    foreach (RectTransform barHorizontal in barHorizontals)
        //    {
        //        Destroy(barHorizontal);
        //    }

        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e);
        //}
        //finally
        //{
        //}
        


        //ラベルテンプレ読み込み
        //m_templateLabelX = transform.Find("view/labelX").GetComponent<RectTransform>();
        //m_templateLabelY = transform.Find("view/labelY").GetComponent<RectTransform>();
        m_templateLabelX = view.transform.Find ("labelX").GetComponent<RectTransform>();
        m_templateLabelY = view.transform.Find("labelY").GetComponent<RectTransform>();
        //罫線テンプレ読み込み
        //m_templateBarVertical = transform.Find("view/barVertical").GetComponent<RectTransform>();
        //m_templateBarHorizontal = transform.Find("view/barHorizontal").GetComponent<RectTransform>();
        m_templateBarVertical = view.transform.Find("barVertical").GetComponent<RectTransform>();
        m_templateBarHorizontal = view.transform.Find("barHorizontal").GetComponent<RectTransform>();

        //グラフ作成用テストデータリスト
        //List<float> testData = new List<float> {
        //    1010,1015,1020,1090,1070,1040,1045,1060,1055,1050,1045,1030};

        GameObject[] goArr = GameObject.FindGameObjectsWithTag("Human");

        infectedDataBase IDB = new infectedDataBase();
        IDB.MonthlyNewInfector(goArr);

        //グラフ描画
        ShowGraph(IDB.InfectionNumDictionary);
    }

//グラフのプロット(サブ関数)
    private GameObject CreateDot(Vector2 position)
    {
    //GameObject "dot" の追加
        GameObject objDot = new GameObject("dot", typeof(Image));

    //プロットする点("dot")の色を設定
        objDot.GetComponent<Image>().color = Color.black;

    //プロットする点("dot")の親オブジェクトを指定
        objDot.transform.SetParent(m_rtView,false);

    //長方形描画オブジェクトの作成
        RectTransform rtDot = objDot.GetComponent<RectTransform>();

    //プロット位置の指定(引数通りに設定)
        rtDot.anchoredPosition = position;

    //プロットする点("dot")の大きさを指定
        rtDot.sizeDelta = new Vector2(10.0f,10.0f);

    //プロットする点("dot")のベース座標位置を設定(親オブジェクトのローカル座標基準)
        rtDot.anchorMin = Vector2.zero;
        rtDot.anchorMax = Vector2.zero;

        return objDot;

    }

//グラフの正規化と表示(メイン関数)
    private void ShowGraph(Dictionary<string,float> infectionD)
    {
        List<float> dataList = new List<float>(infectionD.Values);


    //グラフの上下空き代
        float fGraphMargin = 50;
        
    //グラフの最大値
        float fGraphTop = m_rtView.sizeDelta.y - fGraphMargin;
        
    //グラフ正規化のための最大値と最小値取得用
        float fMaxY;
        float fMinY;

    //グラフのX軸幅設定(要素数に合わせて可変)
        float fpitchX = m_rtView.sizeDelta.x / (infectionD.Count + 1);

    //最初のプロットを右にオフセット
        float fOffsetX = 30.0f;

        GameObject objLast = null;

    //dataListが空でなければ1つ目のプロット位置を最大値、最小値として取得(初期化)
        if (dataList == null) 
        {
            return;
        }
        else
        {
            fMinY = dataList[0];
            fMaxY = dataList[0];
        }

    //dataList内の最大値と最小値の取得
        for (int i = 0; i < dataList.Count; i++)
        {
            if (fMinY > dataList[i])
            {
                fMinY = dataList[i];
            }
            if (fMaxY < dataList[i])
            {
                fMaxY = dataList[i];
            }
        }

    //dataList内の数値をグラフ高さに加工(正規化＊グラフ高さ)
        for (int i = 0; i < dataList.Count; i++)
        {
        //X座標をプロット数*ピッチ幅で設定
            float fPosX = i * fpitchX + fOffsetX;

        //Y座標を、正規化して上下マージンを付与し設定
            float fPosY =
                ((dataList[i] - fMinY)
                / (fMaxY - fMinY))
                * (fGraphTop - fGraphMargin)
                + fGraphMargin;

        //プロット関数コール(戻り値としてドットの位置を取得)
            GameObject objDot = CreateDot(new Vector2(fPosX, fPosY));

        //2点目のドットができたら、線を引く
            if(objLast != null)
            {
                CreateLine(
                    objLast.GetComponent<RectTransform>().anchoredPosition,
                    objDot.GetComponent<RectTransform>().anchoredPosition
                    );
            }

        //前回ドット位置取得
            objLast = objDot;

    //X軸ラベル追加
            RectTransform rtLabelX = Instantiate(m_templateLabelX, m_rtView);
            rtLabelX.gameObject.SetActive(true);
            rtLabelX.anchoredPosition = new Vector2(fPosX + horizontalOffsetXAxis, 0.0f + verticalOffsetXAxis);
            rtLabelX.GetComponent<Text>().text = (i+1).ToString()+"月";

            RectTransform rtBarVertical = Instantiate(m_templateBarVertical, m_rtView);
            rtBarVertical.gameObject.SetActive(true);
            rtBarVertical.anchoredPosition = new Vector2(fPosX, 0.0f);
        }

    //Y軸ラベル追加 (verticalCountは分割数)      
        for(int i = 0; i <= verticalCount; i++)
        {
            RectTransform rtLabelY = Instantiate(m_templateLabelY, m_rtView);
            rtLabelY.gameObject.SetActive(true);

            float normalizedValue = i * 1.0f / verticalCount;
            float labelHeight = normalizedValue * fGraphTop;

            rtLabelY.anchoredPosition = new Vector2(horizontalOffsetYAxis, labelHeight + verticalOffsetYAxis);
            rtLabelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * fMaxY).ToString();

            RectTransform rtBarHorizontal = Instantiate(m_templateBarHorizontal, m_rtView);
            rtBarHorizontal.gameObject.SetActive(true);
            rtBarHorizontal.anchoredPosition = new Vector2(0.0f, labelHeight);
        }
    }

//２点の位置から線を引く(サブ関数)
    private void CreateLine(Vector2 pointA, Vector2 pointB)
    {
    //線オブジェクトの作成
        GameObject objLine = new GameObject("dotLine", typeof(Image));

    //線色の設定
        objLine.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    //線オブジェクトの親オブジェクト設定
        objLine.transform.SetParent(m_rtView,false);

    //線オブジェクトのインスタンス化
        RectTransform rtLine = objLine.GetComponent<RectTransform>();

    //2点間の距離を算出(Vector2)、正規化
        Vector2 dir = (pointB - pointA).normalized;

    //2点間の距離を算出(float)
        float fDistance = Vector2.Distance(pointA, pointB);

    //プロットする点("dot")のベース座標位置を設定(親オブジェクトのローカル座標基準)
        rtLine.anchorMin = Vector2.zero;
        rtLine.anchorMax = Vector2.zero;
      
    //線の形状を設定 
        rtLine.sizeDelta = new Vector2(fDistance, 5.0f);
    
    //線の傾きを設定(zパラメータ)→Sign｛(1,0),dir} で算出
        rtLine.localEulerAngles = new Vector3(
            0.0f, 
            0.0f,
            Vector2.SignedAngle(new Vector2(1.0f, 0.0f),dir));

    //線の開始位置を設定( pointA[始点] + 2点間ベクトル ×　2点間距離 × 係数)←謎
        rtLine.anchoredPosition = pointA + dir * fDistance * 0.5f;
    }

    public void onCloseGraphClick()
    {
        RectTransform graphSystem = 
            view.transform.parent.GetComponent<RectTransform>();
        graphSystem.gameObject.SetActive(false);
        Debug.Log("Graphic Close!!");
    }
}
