using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructSansyo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 10;
        string str = "����������";

        Transform t = transform; //class �Q�ƌ^
        Vector3 y = Vector3.zero; //struct �l�^

        // class �� struct �ȊO�̌^�����݂��邪�A������ class/struct �̓���Ή�
        // �Ⴆ�� enum �^�AUnity ���� KeyCode �Ƃ��APrimitiveType �Ƃ��B
        // enum �̎��̂͐����ł���A������ sturct �Ȃ̂Œl�^
        KeyCode k = KeyCode.Backspace; //enum�^
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
