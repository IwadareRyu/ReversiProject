using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class C // �Q�ƌ^
{
    public int Value;
}

struct S // �l�^
{
    public int Value;
}

public class StructSansyo3 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        S obj1 = new S(); // �l�^
        obj1.Value = 10;

        S obj2 = obj1; // �l�i���̂̃R�s�[�j
        obj2.Value = 20;

        Debug.Log($"obj1.Value={obj1.Value}"); // 10
        Debug.Log($"obj2.Value={obj2.Value}"); // 20

        C obj3 = new C(); // �Q�ƌ^
        obj3.Value = 10;

        C obj4 = obj3; //�@�Q�ƌ^�̃R�s�[
        obj4.Value = 20;

        Debug.Log($"obj1.Value={obj1.Value}"); // 20
        Debug.Log($"obj2.Value={obj2.Value}"); // 20
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
