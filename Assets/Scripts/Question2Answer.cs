using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question2Answer : MonoBehaviour
{
    [SerializeField]
    private int _rows = 5; // 行数

    [SerializeField]
    private int _columns = 5; // 列数

    private Image[,] _cells; // セルの2次元配列
    private int _selectedRow; // 選択中の行番号
    private int _selectedColumn; // 選択中の列番号

    private void Start()
    {
        _cells = new Image[_rows, _columns]; // 配列の初期化

        // レイアウトの設定。GridLayoutGroup の固定列数の上書き
        var layout = GetComponent<GridLayoutGroup>();
        layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layout.constraintCount = _columns;

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }
                _cells[r, c] = image; // 配列にセルを格納する
            }
        }
    }

    private void Update()
    {
        // キー入力判定
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            TryMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            TryMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {
            TryMoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            TryMoveDown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveCell();
        }
    }

    private bool TryMoveLeft() // 左に移動
    {
        for (var c = _selectedColumn - 1; c >= 0; c--)
        {
            if (TrySelectCell(_selectedRow, c)) { return true; }
        }
        return false;
    }
    private bool TryMoveRight() // 右に移動
    {
        for (var c = _selectedColumn + 1; c < _cells.GetLength(1); c++)
        {
            if (TrySelectCell(_selectedRow, c)) { return true; }
        }
        return false;
    }
    private bool TryMoveUp() // 上に移動
    {
        for (var r = _selectedRow - 1; r >= 0; r--)
        {
            if (TrySelectCell(r, _selectedColumn)) { return true; }
        }
        return false;
    }
    private bool TryMoveDown() // 下に移動
    {
        for (var r = _selectedRow + 1; r < _cells.GetLength(0); r++)
        {
            if (TrySelectCell(r, _selectedColumn)) { return true; }
        }
        return false;
    }

    private void RemoveCell() // 選択中のセルを消す
    {
        var cell = _cells[_selectedRow, _selectedColumn];
        cell.enabled = false;

        // 消した後の選択セルの移動
        if (!TryMoveLeft()) // 左の選択が失敗したら
        {
            if (!TryMoveRight()) // 右の選択が失敗したら
            {
                if (!TryMoveUp()) // 上の選択が失敗したら
                {
                    TryMoveDown();
                }
            }
        }
        // 上の if 階層、以下のように書くこともできる
        // _ = TryMoveLeft() || TryMoveRight() || TryMoveUp() || TryMoveDown();
    }

    private bool TrySelectCell(int row, int column) // セルを選択する
    {
        if (row < 0 || row >= _cells.GetLength(0) || column < 0 || column >= _cells.GetLength(1))
        {
            return false;
        }

        var newCell = _cells[row, column]; // 選択されるセル
        if (!newCell.enabled) { return false; } // 非表示のセルは選択できないので失敗

        var oldCell = _cells[_selectedRow, _selectedColumn]; // 選択されていたセル
        oldCell.color = Color.white;
        newCell.color = Color.red;

        _selectedRow = row;
        _selectedColumn = column;
        return true;
    }
}
