using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoZigen : MonoBehaviour
{
    private void Start()
    {
        // 二次元配列の初期化
        // 通常の配列と同様に、次元ごとに { } で要素を初期化できる
        // int[,] data = new int[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // 初期化子の要素から型推論できる場合、初期化子の型は省略可能
        // int[,] data = new[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // 変数型から型推論できる場合 new も省略可能
        // int[,] data = { { 10, 20, 30 }, { 40, 50, 60 } };

        // var で宣言するなら初期化子から推論できなければならない
        var data = new[,] { { 10, 20, 30 }, { 40, 50, 60 } };

        // 配列全体の要素数は Length プロパティで取得可能
        Debug.Log($"Length={data.Length}");

        // 配列の次元数は Rank プロパティで取得可能
        Debug.Log($"Rank={data.Rank}");

        // 次元ごとの要素数は GetLength() メソッドで取得可能
        for (var r = 0; r < data.Rank; r++)
        {
            Debug.Log($"GetLength({r})={data.GetLength(r)}");
        }

        // 2次元配列の全要素を、行番号・列番号から取り出す
        for (var r = 0; r < data.GetLength(0); r++) // 行単位
        {
            for (var c = 0; c < data.GetLength(1); c++) // 列単位
            {
                Debug.Log($"data[{r}, {c}]={data[r, c]}");
            }
        }

        // 多次元配列とは別にジャグ配列という概念がある
        // 似ているようで別物なので混同しないように注意
        int[][] data2; // ジャグ配列（配列の配列）
        data2 = new int[3][]; // サイズ3のジャグ配列の生成
                             // ジャグ配列の要素は配列
        data2[0] = new int[] { 1, 2, 3 }; // 要素の型が配列
        data2[1] = new int[] { 10, 20 };
        data2[2] = new int[] { };

    }

}
