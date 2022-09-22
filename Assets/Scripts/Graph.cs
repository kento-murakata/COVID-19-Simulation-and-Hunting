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
        //Unity�ō쐬���� "view" ��RectTransform�I�u�W�F�N�g�擾
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
        


        //���x���e���v���ǂݍ���
        //m_templateLabelX = transform.Find("view/labelX").GetComponent<RectTransform>();
        //m_templateLabelY = transform.Find("view/labelY").GetComponent<RectTransform>();
        m_templateLabelX = view.transform.Find ("labelX").GetComponent<RectTransform>();
        m_templateLabelY = view.transform.Find("labelY").GetComponent<RectTransform>();
        //�r���e���v���ǂݍ���
        //m_templateBarVertical = transform.Find("view/barVertical").GetComponent<RectTransform>();
        //m_templateBarHorizontal = transform.Find("view/barHorizontal").GetComponent<RectTransform>();
        m_templateBarVertical = view.transform.Find("barVertical").GetComponent<RectTransform>();
        m_templateBarHorizontal = view.transform.Find("barHorizontal").GetComponent<RectTransform>();

        //�O���t�쐬�p�e�X�g�f�[�^���X�g
        //List<float> testData = new List<float> {
        //    1010,1015,1020,1090,1070,1040,1045,1060,1055,1050,1045,1030};

        GameObject[] goArr = GameObject.FindGameObjectsWithTag("Human");

        infectedDataBase IDB = new infectedDataBase();
        IDB.MonthlyNewInfector(goArr);

        //�O���t�`��
        ShowGraph(IDB.InfectionNumDictionary);
    }

//�O���t�̃v���b�g(�T�u�֐�)
    private GameObject CreateDot(Vector2 position)
    {
    //GameObject "dot" �̒ǉ�
        GameObject objDot = new GameObject("dot", typeof(Image));

    //�v���b�g����_("dot")�̐F��ݒ�
        objDot.GetComponent<Image>().color = Color.black;

    //�v���b�g����_("dot")�̐e�I�u�W�F�N�g���w��
        objDot.transform.SetParent(m_rtView,false);

    //�����`�`��I�u�W�F�N�g�̍쐬
        RectTransform rtDot = objDot.GetComponent<RectTransform>();

    //�v���b�g�ʒu�̎w��(�����ʂ�ɐݒ�)
        rtDot.anchoredPosition = position;

    //�v���b�g����_("dot")�̑傫�����w��
        rtDot.sizeDelta = new Vector2(10.0f,10.0f);

    //�v���b�g����_("dot")�̃x�[�X���W�ʒu��ݒ�(�e�I�u�W�F�N�g�̃��[�J�����W�)
        rtDot.anchorMin = Vector2.zero;
        rtDot.anchorMax = Vector2.zero;

        return objDot;

    }

//�O���t�̐��K���ƕ\��(���C���֐�)
    private void ShowGraph(Dictionary<string,float> infectionD)
    {
        List<float> dataList = new List<float>(infectionD.Values);


    //�O���t�̏㉺�󂫑�
        float fGraphMargin = 50;
        
    //�O���t�̍ő�l
        float fGraphTop = m_rtView.sizeDelta.y - fGraphMargin;
        
    //�O���t���K���̂��߂̍ő�l�ƍŏ��l�擾�p
        float fMaxY;
        float fMinY;

    //�O���t��X�����ݒ�(�v�f���ɍ��킹�ĉ�)
        float fpitchX = m_rtView.sizeDelta.x / (infectionD.Count + 1);

    //�ŏ��̃v���b�g���E�ɃI�t�Z�b�g
        float fOffsetX = 30.0f;

        GameObject objLast = null;

    //dataList����łȂ����1�ڂ̃v���b�g�ʒu���ő�l�A�ŏ��l�Ƃ��Ď擾(������)
        if (dataList == null) 
        {
            return;
        }
        else
        {
            fMinY = dataList[0];
            fMaxY = dataList[0];
        }

    //dataList���̍ő�l�ƍŏ��l�̎擾
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

    //dataList���̐��l���O���t�����ɉ��H(���K�����O���t����)
        for (int i = 0; i < dataList.Count; i++)
        {
        //X���W���v���b�g��*�s�b�`���Őݒ�
            float fPosX = i * fpitchX + fOffsetX;

        //Y���W���A���K�����ď㉺�}�[�W����t�^���ݒ�
            float fPosY =
                ((dataList[i] - fMinY)
                / (fMaxY - fMinY))
                * (fGraphTop - fGraphMargin)
                + fGraphMargin;

        //�v���b�g�֐��R�[��(�߂�l�Ƃ��ăh�b�g�̈ʒu���擾)
            GameObject objDot = CreateDot(new Vector2(fPosX, fPosY));

        //2�_�ڂ̃h�b�g���ł�����A��������
            if(objLast != null)
            {
                CreateLine(
                    objLast.GetComponent<RectTransform>().anchoredPosition,
                    objDot.GetComponent<RectTransform>().anchoredPosition
                    );
            }

        //�O��h�b�g�ʒu�擾
            objLast = objDot;

    //X�����x���ǉ�
            RectTransform rtLabelX = Instantiate(m_templateLabelX, m_rtView);
            rtLabelX.gameObject.SetActive(true);
            rtLabelX.anchoredPosition = new Vector2(fPosX + horizontalOffsetXAxis, 0.0f + verticalOffsetXAxis);
            rtLabelX.GetComponent<Text>().text = (i+1).ToString()+"��";

            RectTransform rtBarVertical = Instantiate(m_templateBarVertical, m_rtView);
            rtBarVertical.gameObject.SetActive(true);
            rtBarVertical.anchoredPosition = new Vector2(fPosX, 0.0f);
        }

    //Y�����x���ǉ� (verticalCount�͕�����)      
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

//�Q�_�̈ʒu�����������(�T�u�֐�)
    private void CreateLine(Vector2 pointA, Vector2 pointB)
    {
    //���I�u�W�F�N�g�̍쐬
        GameObject objLine = new GameObject("dotLine", typeof(Image));

    //���F�̐ݒ�
        objLine.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

    //���I�u�W�F�N�g�̐e�I�u�W�F�N�g�ݒ�
        objLine.transform.SetParent(m_rtView,false);

    //���I�u�W�F�N�g�̃C���X�^���X��
        RectTransform rtLine = objLine.GetComponent<RectTransform>();

    //2�_�Ԃ̋������Z�o(Vector2)�A���K��
        Vector2 dir = (pointB - pointA).normalized;

    //2�_�Ԃ̋������Z�o(float)
        float fDistance = Vector2.Distance(pointA, pointB);

    //�v���b�g����_("dot")�̃x�[�X���W�ʒu��ݒ�(�e�I�u�W�F�N�g�̃��[�J�����W�)
        rtLine.anchorMin = Vector2.zero;
        rtLine.anchorMax = Vector2.zero;
      
    //���̌`���ݒ� 
        rtLine.sizeDelta = new Vector2(fDistance, 5.0f);
    
    //���̌X����ݒ�(z�p�����[�^)��Sign�o(1,0),dir} �ŎZ�o
        rtLine.localEulerAngles = new Vector3(
            0.0f, 
            0.0f,
            Vector2.SignedAngle(new Vector2(1.0f, 0.0f),dir));

    //���̊J�n�ʒu��ݒ�( pointA[�n�_] + 2�_�ԃx�N�g�� �~�@2�_�ԋ��� �~ �W��)����
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
