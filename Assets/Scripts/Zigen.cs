using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigen : MonoBehaviour
{
    private void Start()
    {
        // ����I�Ȉꎟ���̃f�[�^��i�ꎟ���z��j
        // ������������ 

        // ����𕽖ʏ��2�s3��ɓW�J����i�񎟌��z��j
        // ������
        // ������

        // �񎟌��z��̐錾
        // �v�f�^[,] �ϐ���;
        int[,] data; // �ǉ����鎟�������� , �ŋ�؂�

        // �񎟌��z��̐���
        // new �v�f�^[1�����v�f��,2�����v�f��]
        data = new int[2, 3]; // �Ⴆ��2�s3���2�����z��

        // �񎟌��z��ւ̃A�N�Z�X
        data[0, 0] = 10;
        data[0, 1] = 20;
        data[0, 2] = 30;
        data[1, 0] = 40;
        data[1, 1] = 50;
        data[1, 2] = 60;

        Debug.Log($"data[0, 0]={data[0, 0]}");
        Debug.Log($"data[0, 1]={data[0, 1]}");
        Debug.Log($"data[0, 2]={data[0, 2]}");
        Debug.Log($"data[1, 0]={data[1, 0]}");
        Debug.Log($"data[1, 1]={data[1, 1]}");
        Debug.Log($"data[1, 2]={data[1, 2]}");
    }
}
