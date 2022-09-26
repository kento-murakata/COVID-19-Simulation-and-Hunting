using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

    public class Graph : MonoBehaviour
    {
        public static int verticalCount = 10;
        public static float horizontalOffsetYAxis = 90.0f;
        public static float verticalOffsetYAxis = 20.0f;
        public static float horizontalOffsetXAxis = 0.0f;
        public static float verticalOffsetXAxis = 0.0f;

        private static Image view;
        private static RectTransform m_rtView;
        private static RectTransform m_templateLabelX;
        private static RectTransform m_templateLabelY;

        private static RectTransform m_templateBarVertical;
        private static RectTransform m_templateBarHorizontal;

        public static void MakeGraph()
        {
            //�O���t�̃x�[�X�ƂȂ�I�u�W�F�N�g�̓ǂݍ���
            ReadBase();

            //�����̓_�A���A���x�����폜
            DeleteExistObjects();

            //�O���t�`��
            ShowGraph(GameManager.InfectionNumDictionary);
        }

        //Close�{�^���ŃO���t�����(ButtonManager�ɓo�^)
        public static void onCloseGraphClick()
        {
            m_rtView.transform.parent.GetComponent<RectTransform>().gameObject.SetActive(false);
        }

        //�O���t�̃x�[�X�ǂݍ���
        private static void ReadBase()
        {
            //�A���J�[�ƂȂ�"view"�I�u�W�F�N�g�̎擾
            view = GameObject.Find("GraphSystem/Canvas/Graph/view").GetComponent<Image>();

            //Unity�ō쐬���� "view" ��RectTransform�I�u�W�F�N�g�擾
            m_rtView = view.GetComponent<RectTransform>();

            //view�̐e�I�u�W�F�N�g"Graph"���擾���A�L����(�O���t��\��������)
            RectTransform graphSystem = view.transform.parent.GetComponent<RectTransform>();
            graphSystem.gameObject.SetActive(true);

            //���x���e���v���ǂݍ���
            m_templateLabelX = view.transform.Find("labelX").GetComponent<RectTransform>();
            m_templateLabelY = view.transform.Find("labelY").GetComponent<RectTransform>();

            //�r���e���v���ǂݍ���
            m_templateBarVertical = view.transform.Find("barVertical").GetComponent<RectTransform>();
            m_templateBarHorizontal = view.transform.Find("barHorizontal").GetComponent<RectTransform>();
        }

        //2��ڈȍ~�̃O���t���������A�����̃I�u�W�F�N�g���폜����B
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

        //�O���t�̃v���b�g(�T�u�֐�)
        private static GameObject CreateDot(Vector2 position)
        {
            //GameObject "dot" �̒ǉ�
            GameObject objDot = new GameObject("dot", typeof(Image));

            //�v���b�g����_("dot")�̐F��ݒ�
            objDot.GetComponent<Image>().color = Color.black;

            //�v���b�g����_("dot")�̐e�I�u�W�F�N�g���w��
            objDot.transform.SetParent(m_rtView, false);

            //�����`�`��I�u�W�F�N�g�̍쐬
            RectTransform rtDot = objDot.GetComponent<RectTransform>();

            //�v���b�g�ʒu�̎w��(�����ʂ�ɐݒ�)
            rtDot.anchoredPosition = position;

            //�v���b�g����_("dot")�̑傫�����w��
            rtDot.sizeDelta = new Vector2(10.0f, 10.0f);

            //�v���b�g����_("dot")�̃x�[�X���W�ʒu��ݒ�(�e�I�u�W�F�N�g�̃��[�J�����W�)
            rtDot.anchorMin = Vector2.zero;
            rtDot.anchorMax = Vector2.zero;

            return objDot;

        }

        //�O���t�̐��K���ƕ\��(���C���֐�)
        private static void ShowGraph(Dictionary<string, float> infectionD)
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
                if (objLast != null)
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
                rtLabelX.GetComponent<Text>().text = (i + 1).ToString() + "��";

                RectTransform rtBarVertical = Instantiate(m_templateBarVertical, m_rtView);
                rtBarVertical.gameObject.SetActive(true);
                rtBarVertical.anchoredPosition = new Vector2(fPosX, 0.0f);
            }

            //Y�����x���ǉ� (verticalCount�͕�����)      
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

        //�Q�_�̈ʒu�����������(�T�u�֐�)
        private static void CreateLine(Vector2 pointA, Vector2 pointB)
        {
            //���I�u�W�F�N�g�̍쐬
            GameObject objLine = new GameObject("dotLine", typeof(Image));

            //���F�̐ݒ�
            objLine.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

            //���I�u�W�F�N�g�̐e�I�u�W�F�N�g�ݒ�
            objLine.transform.SetParent(m_rtView, false);

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
                Vector2.SignedAngle(new Vector2(1.0f, 0.0f), dir));

            //���̊J�n�ʒu��ݒ�( pointA[�n�_] + 2�_�ԃx�N�g�� �~�@2�_�ԋ��� �~ �W��)����
            rtLine.anchoredPosition = pointA + dir * fDistance * 0.5f;
        }

    }
