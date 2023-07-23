using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // �z��
        // �����̃f�[�^����̘A�����ꂽ�f�[�^�Ƃ��Ĉ����^
        // ���̎��̌ʂ̃f�[�^�̂��Ƃ��u�v�f�v�ƌĂ�

        // # �z��錾
        // �v�f�^[] �ϐ���;
        int[] iAry;

        // # �z��C���X�^���X�̐���
        // new �v�f�^[�v�f��]
        iAry = new int[3];

        // # �z��v�f�ւ̃A�N�Z�X
        iAry[0] = 10; // �z��v�f�ւ̏�������
        iAry[1] = 20;
        iAry[2] = 30;

        Debug.Log($"iAry[0] = {iAry[0]}"); // �z��v�f����ǂݍ���
        Debug.Log($"iAry[1] = {iAry[1]}");
        Debug.Log($"iAry[2] = {iAry[2]}");

        int[] ary = iAry;

        Debug.Log($"iAry2.Length = {iAry.Length}");

        Debug.Log($"ary.Length = {ary.Length}");

        // # �z��̏�����
        // �ʏ�̕ϐ��Ɠ����悤�ɐ錾�Ɠ����ɏ������ł���
        //int[] iAry2 = new int[3]; // OK

        // �z��̏ꍇ { } �̔z�񏉊����q���g����
        // new �v�f�^[�v�f��] { �v�f1, �v�f2, ... }
        //int[] iAry2 = new int[3] { 10, 20, 30 };

        // �������q����v�f���𔻒�ł���̂ŁA�v�f�����ȗ��ł���
        // int[] iAry2 = new int[] { 10, 20, 30 };

        // �������q�̗v�f����^�𐄘_�ł���
        int[] iAry2 = new[] { 10, 20, 30 };

        // �z��Ɣ�������
        int[] iAry3 = new[] { 10, 20, 30 };
        for (var i = 0; i < iAry.Length; i++)
        {
            Debug.Log($"{i}={iAry3[i]}");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
