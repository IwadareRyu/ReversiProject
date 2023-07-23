using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightOutAnswer : MonoBehaviour, IPointerClickHandler
{
    private GameObject[,] _cells; // セルの二次元配列

    private void Start()
    {
        _cells = new GameObject[5, 5]; // 二次元配列の初期化
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c] = cell; // 生成したセルを配列に保存
            }
        }

        Reset();
    }

    /// <summary>
    /// ゲームを初期化する。
    /// </summary>
    private void Reset()
    {
        // クリアしている状態に
        Clear(Color.black);

        do
        {
            var rows = _cells.GetLength(0);
            var columns = _cells.GetLength(1);
            for (var i = 0; i < 10; i++) // 適当な回数ランダムにクリック
            {
                var r = Random.Range(0, rows);
                var c = Random.Range(0, columns);
                ClickCell(r, c);

                var cell = _cells[r, c];
                Debug.Log($"Click {cell.name}", cell);
            }
        } while (IsAllCell(Color.black) || IsSolvableNext());
        // クリア状態、またはあと一手でクリア可能ならもう一回
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        if (TryGetCellPosition(cell, out var row, out var column))
        {
            ClickCell(row, column);
            if (IsAllCell(Color.black)) { Debug.Log("ゲームクリア"); }
        }
    }

    /// <summary>
    /// すべてのセルの色を、指定の色に初期化する。
    /// </summary>
    /// <param name="color">色。</param>
    private void Clear(Color color)
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var image = _cells[r, c].GetComponent<Image>();
                image.color = color;
            }
        }
    }

    /// <summary>
    /// すべてのセルが、指定の色に染まっているかどうか。
    /// </summary>
    /// <param name="color">調べる色。</param>
    /// <returns>すべてのセルが <paramref name="color"/> であれば true。そうでなければ false。</returns>
    private bool IsAllCell(Color color)
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var image = _cells[r, c].GetComponent<Image>();
                if (image.color != color) { return false; }
            }
        }
        return true;
    }

    /// <summary>
    /// 次の一手でゲームクリア可能かどうか。
    /// </summary>
    /// <returns>次の一手でゲームクリア可能なら true。そうでなければ false。</returns>
    private bool IsSolvableNext()
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                ClickCell(r, c);
                var isAll = IsAllCell(Color.black);
                ClickCell(r, c); // 元に戻す
                if (isAll) { return true; }
            }
        }
        return false;
    }

    /// <summary>
    /// 指定の行番号・列番号のセルをクリックする。
    /// </summary>
    /// <param name="row">行番号。</param>
    /// <param name="column">列番号。</param>
    private void ClickCell(int row, int column)
    {
        SwitchCell(row, column);
        SwitchCell(row + 1, column);
        SwitchCell(row, column + 1);
        SwitchCell(row - 1, column);
        SwitchCell(row, column - 1);
    }

    /// <summary>
    /// 指定したセルの行番号と列番号を返す。
    /// </summary>
    /// <param name="cell">調べるセル。</param>
    /// <param name="row">行番号。</param>
    /// <param name="column">列番号。</param>
    /// <returns>成功すれば true。失敗すれば false。</returns>
    private bool TryGetCellPosition(GameObject cell, out int row, out int column)
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                if (cell == _cells[r, c])
                {
                    row = r;
                    column = c;
                    return true;
                }
            }
        }

        row = 0; column = 0;
        return false;
    }

    /// <summary>
    /// 指定の行番号・列番号のセルの色を反転させる。
    /// </summary>
    /// <param name="row">行番号。</param>
    /// <param name="column">列番号。</param>
    private void SwitchCell(int row, int column)
    {
        var rows = _cells.GetLength(0);
        var columns = _cells.GetLength(1);

        if (row >= 0 && column >= 0 && row < rows && column < columns)
        {
            SwitchCell(_cells[row, column]);
        }
    }

    /// <summary>
    /// 指定したセルの色を反転させる。
    /// </summary>
    /// <param name="cell">セル。</param>
    private void SwitchCell(GameObject cell)
    {
        if (cell.TryGetComponent<Image>(out var image))
        {
            image.color = image.color == Color.white ? Color.black : Color.white;
        }
    }
}
