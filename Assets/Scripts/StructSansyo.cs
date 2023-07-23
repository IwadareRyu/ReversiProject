using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructSansyo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int i = 10;
        string str = "あいうえお";

        Transform t = transform; //class 参照型
        Vector3 y = Vector3.zero; //struct 値型

        // class や struct 以外の型も存在するが、それらは class/struct の特殊対応
        // 例えば enum 型、Unity だと KeyCode とか、PrimitiveType とか。
        // enum の実体は整数であり、整数は sturct なので値型
        KeyCode k = KeyCode.Backspace; //enum型
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
