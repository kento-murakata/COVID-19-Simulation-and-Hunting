using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace InfectionSimulator
{
    //    /*
    //    1��������̗z���� List
    //    1��������̎��Ґ� List
    //    �g�[�^�����Ґ��B�P���ڂ�TOTAL�A�Q���ڂ�TOTAL�݂����ɓ��X�̃g�[�^�����o��
    //    �g�[�^�������ҁB��ƈꏏ
    //    �a�@�g�p��

    //float�̃��X�g=> Graph��

    //     */
    //    public class infectedDataBase : MonoBehaviour
    //    {
    //        public enum infectedStatus
    //        {
    //            NEGATIVE,
    //            EXPOSE,
    //            ASYMPOTOMATIC,
    //            SYMPTONS,
    //            INFECTED;
    //        }
    public class EnemyManager : MonoBehaviour
    {
        GameObject[] enemyObjects;
        int enemyNum;
        void Start()
        {
            enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            enemyNum = enemyObjects.Length
        }
    }
    public void Day()
    {
        int dayCount; // �`���ڂ� "�`"�̕����̕ϐ�
        dayCount = 1;
        string day = $"Day{dayCount}"; //Day1 �� day �ɑ��
    }

    //        public void CountDailyInfector()
    //        {
    //            int[] dailyInfector = new int[];
    //        }

    //        public void CountDailyDeath()
    //        {
    //            int[] dailydeath = new int[];
    //        }

    //        public void CountTotalInfector()
    //        {
    //            dailyInfector.CountDailyInfector();
    //            num.Sum(); // 15
    //            int totalInfector =

    //        }

    //        public void CountTodalDeath()
    //    {
    //        int totalDeath;
    //    }


}









