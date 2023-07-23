using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    private const int _size = 3;

    private GameObject[,] _cells;

    [SerializeField]
    private Color _normalColor = Color.white;

    [SerializeField]
    private Color _selectedColor = Color.cyan;

    [SerializeField]
    private Sprite _circle;

    [SerializeField]
    private Sprite _cross;

    [SerializeField]
    private Text _wintext;

    private int _selectedRow;
    private int _selectedColumn;

    private bool _putbool;

    bool _winLose;

    private Player _player;

    int _turn = 1;

    float _time = 0;

    // Start is called before the first frame update
    void Start()
    {
        _cells = new GameObject[_size, _size];
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
        if (_wintext) { _wintext.text = ""; }
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのカーソル移動
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _selectedColumn--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _selectedColumn++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _selectedRow--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _selectedRow++;
        }

        // カーソルが範囲外に出ないように制限
        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= _size) { _selectedColumn = _size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= _size) { _selectedRow = _size - 1; }

        // プレイヤーの番でスペースキーを押すとシンボルを配置
        if (Input.GetKeyDown(KeyCode.Space) && !_putbool && _player == Player.Player)
        {
            var cell = _cells[_selectedRow, _selectedColumn];
            var image = cell.GetComponent<Image>();
            if (image.sprite == null)
            {
                image.sprite = _circle;
                _putbool = true;
            }
            else
            {
                Debug.Log("置けないよ！");
            }
        }

        // AIの番で一定時間経過後にランダムにシンボルを配置
        if (!_putbool && !_winLose && _player == Player.AI)
        {
            _time += Time.deltaTime;
            if (_time >= 2f)
            {
                while (!_putbool)
                {
                    var ramwidth = Random.Range(0, _size);
                    var ramvertical = Random.Range(0, _size);
                    var image = _cells[ramvertical, ramwidth].GetComponent<Image>();
                    if (image.sprite == null)
                    {
                        image.sprite = _cross;
                        _putbool = true;
                        _time = 0;
                    }
                }
            }
        }

        // カーソルの位置を示すためにセルの色を変更
        for (var r = 0; r < _size; r++)
        {
            for (var c = 0; c < _size; c++)
            {
                var cell = _cells[r, c];
                var image = cell.GetComponent<Image>();
                image.color = (r == _selectedRow && c == _selectedColumn) ? _selectedColor : _normalColor;
            }
        }

        // 勝敗判定と手番の切り替え
        if (_putbool && !_winLose)
        {
            if (_player == Player.Player)
            {
                _winLose = WinLose(_circle);
            }
            else
            {
                _winLose = WinLose(_cross);
            }

            if (_winLose == true)
            {
                Debug.Log($"{_player}の勝利！");
                if (_wintext) { _wintext.text = $"{_player} の勝利！"; }
                StartCoroutine(ResetCoroutine());
            }
            else
            {
                if (_turn == _size * _size)
                {
                    Debug.Log("引き分け！");
                    if (_wintext) { _wintext.text = "引き分け！"; }
                    _winLose = true;
                    StartCoroutine(ResetCoroutine());
                }
                else
                {
                    var tmp = (int)(_player + 1) % 2;
                    _player = (Player)tmp;
                    _putbool = false;
                    _turn++;
                }
            }
        }
    }

    /// <summary>勝敗判定</summary>
    bool WinLose(Sprite sprite)
    {
        bool[] slantingbool = new bool[2] { true, true };
        for (var i = 0; i < _size; i++)
        {
            // 斜め(右上から左下)
            if (slantingbool[0] == true) { slantingbool[0] = ChackWin(0, 0, i, i, sprite); }
            // 斜め(左上から右下)
            if (slantingbool[1] == true) { slantingbool[1] = ChackWin(0, _size - 1, i, -i, sprite); }
        }
        // 斜めのみ一回だけチェックすればよいので一回のみ勝敗判定
        if (slantingbool.Contains(true))
        {
            return true;
        }

        // 縦横の勝敗判定
        for (var i = 0; i < _size; i++)
        {
            bool[] winbool = new bool[2] { true, true };
            for (var j = 0; j < _size; j++)
            {
                // 縦
                if (winbool[0] == true) { winbool[0] = ChackWin(i, 0, 0, j, sprite); }
                // 横
                if (winbool[1] == true) { winbool[1] = ChackWin(0, i, j, 0, sprite); }
            }
            if (winbool.Contains(true))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>勝っているか負けているかの判定(縦横斜めどれでも判定できるようにしている)</summary>
    bool ChackWin(int r, int c, int vertical, int width, Sprite sprite)
    {
        var image = _cells[r + vertical, c + width].GetComponent<Image>();
        if (image.sprite != sprite)
        {
            return false;
        }
        return true;
    }

    /// <summary>リセット処理</summary>
    /// <returns></returns>
    IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(5f);
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var image = _cells[r, c].GetComponent<Image>();
                image.sprite = null;
            }
        }
        _winLose = false;
        _putbool = false;
        _player = Player.Player;
        _turn = 1;
        if (_wintext) { _wintext.text = ""; }
    }

    enum Player
    {
        Player,
        AI
    }
}