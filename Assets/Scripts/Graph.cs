using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    #region PUBLIC MEMBERS
    public static int verticalCount = 10;
    public static float horizontalOffsetYAxis = 90.0f;
    public static float verticalOffsetYAxis = 20.0f;
    public static float horizontalOffsetXAxis = 0.0f;
    public static float verticalOffsetXAxis = 0.0f;
    #endregion

    #region PRIVATE MEMBERS
    private static Image view;
    private static RectTransform m_rtView;
    private static RectTransform m_templateLabelX;
    private static RectTransform m_templateLabelY;

    private static RectTransform m_templateBarVertical;
    private static RectTransform m_templateBarHorizontal;
    #endregion

    #region PUBLIC METHODS
    //main function of this class about create graph's data and view
    public static void MakeGraph()
    {
        //load the base of graph  
        LoadBase();

        //delete existing dots and lines
        DeleteExistObjects();

        //create graph
        ShowGraph(infectedDataBase.InfectionNumDictionary);
    }

    //close the graph window by pressing the close button (register for ButtonManager)
    public static void onCloseGraphClick()
    {
        m_rtView.transform.parent.GetComponent<RectTransform>().gameObject.SetActive(false);
    }
    #endregion

    #region PRIVATE METHODS
    //load the base of graph
    private static void LoadBase()
    {
        //get the component whitch name is "view"
        view = GameObject.Find("GraphSystem/Canvas/Graph/view").GetComponent<Image>();

        //get the object as the type of "RectTransform"
        m_rtView = view.GetComponent<RectTransform>();

        //get the parent object whitch name is "Graph" and activate
        RectTransform graphSystem = view.transform.parent.GetComponent<RectTransform>();
        graphSystem.gameObject.SetActive(true);

        //load label template
        m_templateLabelX = view.transform.Find("labelX").GetComponent<RectTransform>();
        m_templateLabelY = view.transform.Find("labelY").GetComponent<RectTransform>();

        //load line tenplate
        m_templateBarVertical = view.transform.Find("barVertical").GetComponent<RectTransform>();
        m_templateBarHorizontal = view.transform.Find("barHorizontal").GetComponent<RectTransform>();
    }

    //delete existing objects after second access
    private static void DeleteExistObjects()
    {
        for (int i = 0; i < m_rtView.transform.childCount; i++)
        {
            if (m_rtView.transform.GetChild(i).name == "labelX(Clone)"
                || m_rtView.transform.GetChild(i).name == "labelY(Clone)"
                || m_rtView.transform.GetChild(i).name == "dot"
                || m_rtView.transform.GetChild(i).name == "dotLine"
                || m_rtView.transform.GetChild(i).name == "barVertical(Clone)"
                || m_rtView.transform.GetChild(i).name == "barHorizontal(Clone)")
            {
                Destroy(m_rtView.transform.GetChild(i).gameObject);
            }
        }
    }

    //create dot in the graph
    private static GameObject CreateDot(Vector2 position)
    {
        //add dot object
        GameObject objDot = new GameObject("dot", typeof(Image));

        //edit dots color
        objDot.GetComponent<Image>().color = Color.black;

        //set dot parent
        objDot.transform.SetParent(m_rtView, false);

        //create rectangle
        RectTransform rtDot = objDot.GetComponent<RectTransform>();

        //specify dot anchor position by the argument
        rtDot.anchoredPosition = position;

        //specify dot scale
        rtDot.sizeDelta = new Vector2(10.0f, 10.0f);

        //specify dot base coordinate
        rtDot.anchorMin = Vector2.zero;
        rtDot.anchorMax = Vector2.zero;

        return objDot;

    }

    //normalize the graph and display the graph
    private static void ShowGraph(Dictionary<string, float> infectionD)
    {
        List<float> dataList = new List<float>(infectionD.Values);


        //chart top and bottom margin
        float fGraphMargin = 50;

        //graph maximum
        float fGraphTop = m_rtView.sizeDelta.y - fGraphMargin;

        //declare the variables about graph maximum and minimum
        float fMaxY;
        float fMinY;

        //set X axis width
        float fpitchX = m_rtView.sizeDelta.x / (infectionD.Count + 1);

        //offset right the first plot
        float fOffsetX = 30.0f;

        GameObject objLast = null;

        //initialize the variable about maximum and minimum if datalist is not null
        if (dataList == null)
        {
            return;
        }
        else
        {
            fMinY = dataList[0];
            fMaxY = dataList[0];
        }

        //get maximum and minimum in the List
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

        //set the height of the graph by the value in the list
        for (int i = 0; i < dataList.Count; i++)
        {
            //set X axis
            float fPosX = i * fpitchX + fOffsetX;

            //set Y axis
            float fPosY =
                ((dataList[i] - fMinY)
                / (fMaxY - fMinY))
                * (fGraphTop - fGraphMargin)
                + fGraphMargin;

            //call CreateDot
            GameObject objDot = CreateDot(new Vector2(fPosX, fPosY));

            //create line when creating dot second time 
            if (objLast != null)
            {
                CreateLine(
                    objLast.GetComponent<RectTransform>().anchoredPosition,
                    objDot.GetComponent<RectTransform>().anchoredPosition
                    );
            }

            //get the position of last dot
            objLast = objDot;

            //add X label
            RectTransform rtLabelX = Instantiate(m_templateLabelX, m_rtView);
            rtLabelX.gameObject.SetActive(true);
            rtLabelX.anchoredPosition = new Vector2(fPosX + horizontalOffsetXAxis, 0.0f + verticalOffsetXAxis);
            rtLabelX.GetComponent<Text>().text = (i + 1).ToString() + "åé";

            RectTransform rtBarVertical = Instantiate(m_templateBarVertical, m_rtView);
            rtBarVertical.gameObject.SetActive(true);
            rtBarVertical.anchoredPosition = new Vector2(fPosX, 0.0f);
        }

        //add Y label (veticalCount is division number)     
        for (int i = 0; i <= verticalCount; i++)
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

    //create line by the two dots position
    private static void CreateLine(Vector2 pointA, Vector2 pointB)
    {
        //create line object
        GameObject objLine = new GameObject("dotLine", typeof(Image));

        //set color
        objLine.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

        //set parent object
        objLine.transform.SetParent(m_rtView, false);

        //instantiate the line object
        RectTransform rtLine = objLine.GetComponent<RectTransform>();

        //calculate the distance between two dots and normalize the distance
        Vector2 dir = (pointB - pointA).normalized;

        //calculate the distance as type of float
        float fDistance = Vector2.Distance(pointA, pointB);

        //specify line base coordinate
        rtLine.anchorMin = Vector2.zero;
        rtLine.anchorMax = Vector2.zero;

        //set form 
        rtLine.sizeDelta = new Vector2(fDistance, 5.0f);

        //set anglesÅ®calculate by SignÅo(1,0),dir}
        rtLine.localEulerAngles = new Vector3(
            0.0f,
            0.0f,
            Vector2.SignedAngle(new Vector2(1.0f, 0.0f), dir));

        //set start position( pointA[start point] + vetor between two points Å~Å@distance between two points Å~ coefficient)Å©mistery
        rtLine.anchoredPosition = pointA + dir * fDistance * 0.5f;
    }
    #endregion
}
