using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 配列
        // 複数のデータを一つの連結されたデータとして扱う型
        // この時の個別のデータのことを「要素」と呼ぶ

        // # 配列宣言
        // 要素型[] 変数名;
        int[] iAry;

        // # 配列インスタンスの生成
        // new 要素型[要素数]
        iAry = new int[3];

        // # 配列要素へのアクセス
        iAry[0] = 10; // 配列要素への書き込み
        iAry[1] = 20;
        iAry[2] = 30;

        Debug.Log($"iAry[0] = {iAry[0]}"); // 配列要素から読み込み
        Debug.Log($"iAry[1] = {iAry[1]}");
        Debug.Log($"iAry[2] = {iAry[2]}");

        int[] ary = iAry;

        Debug.Log($"iAry2.Length = {iAry.Length}");

        Debug.Log($"ary.Length = {ary.Length}");

        // # 配列の初期化
        // 通常の変数と同じように宣言と同時に初期化できる
        //int[] iAry2 = new int[3]; // OK

        // 配列の場合 { } の配列初期化子を使える
        // new 要素型[要素数] { 要素1, 要素2, ... }
        //int[] iAry2 = new int[3] { 10, 20, 30 };

        // 初期化子から要素数を判定できるので、要素数を省略できる
        // int[] iAry2 = new int[] { 10, 20, 30 };

        // 初期化子の要素から型を推論できる
        int[] iAry2 = new[] { 10, 20, 30 };

        // 配列と反復処理
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
