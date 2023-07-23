using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Player // プレイヤー
{
    Circle,     // 〇
    Cross       // ×
}

public class TicTacToeAnswer : MonoBehaviour
{
    [SerializeField]
    private Color _normalCell = Color.white;

    [SerializeField]
    private Color _selectedCell = Color.cyan;

    [SerializeField]
    private Sprite _circle = null;

    [SerializeField]
    private Sprite _cross = null;

    private const int Size = 3;
    private GameObject[,] _cells;

    private int _selectedRow;
    private int _selectedColumn;

    private Player _currentPlayer = Player.Circle;

    private void Start()
    {
        _cells = new GameObject[Size, Size];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r},{c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c] = cell;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selectedColumn--; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { _selectedColumn++; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { _selectedRow--; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { _selectedRow++; }

        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= Size) { _selectedColumn = Size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= Size) { _selectedRow = Size - 1; }

        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = _cells[r, c];
                var image = cell.GetComponent<Image>();
                image.color =
                    (r == _selectedRow && c == _selectedColumn)
                    ? _selectedCell : _normalCell;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (TryPlace(_selectedRow, _selectedColumn, _currentPlayer))
            {
                if (IsWinning(_currentPlayer)) // 勝敗判定
                {
                    Debug.Log($"勝者: {_currentPlayer}");
                }
                else if (IsBoardFull())
                {
                    Debug.Log("引き分け");
                }
                else // ゲーム続行
                {
                    SwitchPlayer();
                }
            }
        }
    }

    /// <summary>
    /// 指定のプレイヤーが勝利しているかどうか。
    /// </summary>
    /// <param name="player">判定するプレイヤー。</param>
    /// <returns><paramref name="player"/> が勝利していれば true。そうでなければ false。</returns>
    private bool IsWinning(Player player)
    {

        var topLeft = _cells[0, 0].GetComponent<Image>();
        var topCenter = _cells[0, 1].GetComponent<Image>();
        var topRight = _cells[0, 2].GetComponent<Image>();

        var middleLeft = _cells[1, 0].GetComponent<Image>();
        var middleCenter = _cells[1, 1].GetComponent<Image>();
        var middleRight = _cells[1, 2].GetComponent<Image>();

        var bottomLeft = _cells[2, 0].GetComponent<Image>();
        var bottomCenter = _cells[2, 1].GetComponent<Image>();
        var bottomRight = _cells[2, 2].GetComponent<Image>();

        var sprite = player == Player.Circle ? _circle : _cross;
        return (topLeft.sprite == sprite && topCenter.sprite == sprite && topRight.sprite == sprite)
            || (middleLeft.sprite == sprite && middleCenter.sprite == sprite && middleRight.sprite == sprite)
            || (bottomLeft.sprite == sprite && bottomCenter.sprite == sprite && bottomRight.sprite == sprite)
            || (topLeft.sprite == sprite && middleLeft.sprite == sprite && bottomLeft.sprite == sprite)
            || (topCenter.sprite == sprite && middleCenter.sprite == sprite && bottomCenter.sprite == sprite)
            || (topRight.sprite == sprite && middleRight.sprite == sprite && bottomRight.sprite == sprite)
            || (topLeft.sprite == sprite && middleCenter.sprite == sprite && bottomRight.sprite == sprite)
            || (topRight.sprite == sprite && middleCenter.sprite == sprite && bottomLeft.sprite == sprite)
        ;
    }

    /// <summary>
    /// すべてのセルが埋められているかどうか。
    /// </summary>
    /// <returns>すべてのセルが埋まっている状態であれば true。そうでなければ false。</returns>
    private bool IsBoardFull()
    {
        foreach (var cell in _cells)
        {
            var image = cell.GetComponent<Image>();
            if (image.sprite is null) { return false; }
        }
        return true;
    }

    /// <summary>
    /// 指定の場所に、指定のプレイヤーのマークを設置する。
    /// </summary>
    /// <param name="row">行番号。</param>
    /// <param name="column">列番号。</param>
    /// <param name="player">プレイヤー。</param>
    /// <returns>プレイヤーのマークを設置できれば ture。そうでなければ false。</returns>
    private bool TryPlace(int row, int column, Player player)
    {
        // 境界チェック
        if (row < 0 || column < 0 || row >= Size || column >= Size) { return false; }

        var cell = _cells[row, column];
        var image = cell.GetComponent<Image>();

        // すでに設置済みかどうか
        if (image.sprite is not null) { return false; }

        image.sprite = player == Player.Circle ? _circle : _cross;
        return true;
    }

    /// <summary>
    /// プレイヤーを切り替える。
    /// </summary>
    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == Player.Circle ? Player.Cross : Player.Circle;
    }

}
