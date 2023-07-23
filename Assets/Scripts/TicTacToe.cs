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
        // �v���C���[�̃J�[�\���ړ�
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

        // �J�[�\�����͈͊O�ɏo�Ȃ��悤�ɐ���
        if (_selectedColumn < 0) { _selectedColumn = 0; }
        if (_selectedColumn >= _size) { _selectedColumn = _size - 1; }
        if (_selectedRow < 0) { _selectedRow = 0; }
        if (_selectedRow >= _size) { _selectedRow = _size - 1; }

        // �v���C���[�̔ԂŃX�y�[�X�L�[�������ƃV���{����z�u
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
                Debug.Log("�u���Ȃ���I");
            }
        }

        // AI�̔Ԃň�莞�Ԍo�ߌ�Ƀ����_���ɃV���{����z�u
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

        // �J�[�\���̈ʒu���������߂ɃZ���̐F��ύX
        for (var r = 0; r < _size; r++)
        {
            for (var c = 0; c < _size; c++)
            {
                var cell = _cells[r, c];
                var image = cell.GetComponent<Image>();
                image.color = (r == _selectedRow && c == _selectedColumn) ? _selectedColor : _normalColor;
            }
        }

        // ���s����Ǝ�Ԃ̐؂�ւ�
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
                Debug.Log($"{_player}�̏����I");
                if (_wintext) { _wintext.text = $"{_player} �̏����I"; }
                StartCoroutine(ResetCoroutine());
            }
            else
            {
                if (_turn == _size * _size)
                {
                    Debug.Log("���������I");
                    if (_wintext) { _wintext.text = "���������I"; }
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

    /// <summary>���s����</summary>
    bool WinLose(Sprite sprite)
    {
        bool[] slantingbool = new bool[2] { true, true };
        for (var i = 0; i < _size; i++)
        {
            // �΂�(�E�ォ�獶��)
            if (slantingbool[0] == true) { slantingbool[0] = ChackWin(0, 0, i, i, sprite); }
            // �΂�(���ォ��E��)
            if (slantingbool[1] == true) { slantingbool[1] = ChackWin(0, _size - 1, i, -i, sprite); }
        }
        // �΂߂݈̂�񂾂��`�F�b�N����΂悢�̂ň��̂ݏ��s����
        if (slantingbool.Contains(true))
        {
            return true;
        }

        // �c���̏��s����
        for (var i = 0; i < _size; i++)
        {
            bool[] winbool = new bool[2] { true, true };
            for (var j = 0; j < _size; j++)
            {
                // �c
                if (winbool[0] == true) { winbool[0] = ChackWin(i, 0, 0, j, sprite); }
                // ��
                if (winbool[1] == true) { winbool[1] = ChackWin(0, i, j, 0, sprite); }
            }
            if (winbool.Contains(true))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>�����Ă��邩�����Ă��邩�̔���(�c���΂߂ǂ�ł�����ł���悤�ɂ��Ă���)</summary>
    bool ChackWin(int r, int c, int vertical, int width, Sprite sprite)
    {
        var image = _cells[r + vertical, c + width].GetComponent<Image>();
        if (image.sprite != sprite)
        {
            return false;
        }
        return true;
    }

    /// <summary>���Z�b�g����</summary>
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