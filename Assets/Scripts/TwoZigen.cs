using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoZigen : MonoBehaviour
{
    private void Start()
    {
        // �񎟌��z��̏�����
        // �ʏ�̔z��Ɠ��l�ɁA�������Ƃ� { } �ŗv�f���������ł���
        // int[,] data = new int[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // �������q�̗v�f����^���_�ł���ꍇ�A�������q�̌^�͏ȗ��\
        // int[,] data = new[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // �ϐ��^����^���_�ł���ꍇ new ���ȗ��\
        // int[,] data = { { 10, 20, 30 }, { 40, 50, 60 } };

        // var �Ő錾����Ȃ珉�����q���琄�_�ł��Ȃ���΂Ȃ�Ȃ�
        var data = new[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // �z��S�̗̂v�f���� Length �v���p�e�B�Ŏ擾�\
        Debug.Log($"Length={data.Length}");

        // �z��̎������� Rank �v���p�e�B�Ŏ擾�\
        Debug.Log($"Rank={data.Rank}");

        // �������Ƃ̗v�f���� GetLength() ���\�b�h�Ŏ擾�\
        for (var r = 0; r < data.Rank; r++)
        {
            Debug.Log($"GetLength({r})={data.GetLength(r)}");
        }

        // 2�����z��̑S�v�f���A�s�ԍ��E��ԍ�������o��
        for (var r = 0; r < data.GetLength(0); r++) // �s�P��
        {
            for (var c = 0; c < data.GetLength(1); c++) // ��P��
            {
                Debug.Log($"data[{r}, {c}]={data[r, c]}");
            }
        }

        // �������z��Ƃ͕ʂɃW���O�z��Ƃ����T�O������
        // ���Ă���悤�ŕʕ��Ȃ̂ō������Ȃ��悤�ɒ���
        int[][] data2; // �W���O�z��i�z��̔z��j
        data2 = new int[3][]; // �T�C�Y3�̃W���O�z��̐���
                             // �W���O�z��̗v�f�͔z��
        data2[0] = new int[] { 1, 2, 3 }; // �v�f�̌^���z��
        data2[1] = new int[] { 10, 20 };
        data2[2] = new int[] { };

    }

}
