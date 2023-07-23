using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zigen : MonoBehaviour
{
    private void Start()
    {
        // 直列的な一次元のデータ列（一次元配列）
        // □□□□□□ 

        // これを平面上に2行3列に展開する（二次元配列）
        // □□□
        // □□□

        // 二次元配列の宣言
        // 要素型[,] 変数名;
        int[,] data; // 追加する次元数だけ , で区切る

        // 二次元配列の生成
        // new 要素型[1次元要素数,2次元要素数]
        data = new int[2, 3]; // 例えば2行3列の2次元配列

        // 二次元配列へのアクセス
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
