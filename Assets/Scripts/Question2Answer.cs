using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question2Answer : MonoBehaviour
{
    [SerializeField]
    private int _rows = 5; // �s��

    [SerializeField]
    private int _columns = 5; // ��

    private Image[,] _cells; // �Z����2�����z��
    private int _selectedRow; // �I�𒆂̍s�ԍ�
    private int _selectedColumn; // �I�𒆂̗�ԍ�

    private void Start()
    {
        _cells = new Image[_rows, _columns]; // �z��̏�����

        // ���C�A�E�g�̐ݒ�BGridLayoutGroup �̌Œ�񐔂̏㏑��
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
                _cells[r, c] = image; // �z��ɃZ�����i�[����
            }
        }
    }

    private void Update()
    {
        // �L�[���͔���
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���L�[��������
        {
            TryMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // �E�L�[��������
        {
            TryMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // ��L�[��������
        {
            TryMoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // ���L�[��������
        {
            TryMoveDown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveCell();
        }
    }

    private bool TryMoveLeft() // ���Ɉړ�
    {
        for (var c = _selectedColumn - 1; c >= 0; c--)
        {
            if (TrySelectCell(_selectedRow, c)) { return true; }
        }
        return false;
    }
    private bool TryMoveRight() // �E�Ɉړ�
    {
        for (var c = _selectedColumn + 1; c < _cells.GetLength(1); c++)
        {
            if (TrySelectCell(_selectedRow, c)) { return true; }
        }
        return false;
    }
    private bool TryMoveUp() // ��Ɉړ�
    {
        for (var r = _selectedRow - 1; r >= 0; r--)
        {
            if (TrySelectCell(r, _selectedColumn)) { return true; }
        }
        return false;
    }
    private bool TryMoveDown() // ���Ɉړ�
    {
        for (var r = _selectedRow + 1; r < _cells.GetLength(0); r++)
        {
            if (TrySelectCell(r, _selectedColumn)) { return true; }
        }
        return false;
    }

    private void RemoveCell() // �I�𒆂̃Z��������
    {
        var cell = _cells[_selectedRow, _selectedColumn];
        cell.enabled = false;

        // ��������̑I���Z���̈ړ�
        if (!TryMoveLeft()) // ���̑I�������s������
        {
            if (!TryMoveRight()) // �E�̑I�������s������
            {
                if (!TryMoveUp()) // ��̑I�������s������
                {
                    TryMoveDown();
                }
            }
        }
        // ��� if �K�w�A�ȉ��̂悤�ɏ������Ƃ��ł���
        // _ = TryMoveLeft() || TryMoveRight() || TryMoveUp() || TryMoveDown();
    }

    private bool TrySelectCell(int row, int column) // �Z����I������
    {
        if (row < 0 || row >= _cells.GetLength(0) || column < 0 || column >= _cells.GetLength(1))
        {
            return false;
        }

        var newCell = _cells[row, column]; // �I�������Z��
        if (!newCell.enabled) { return false; } // ��\���̃Z���͑I���ł��Ȃ��̂Ŏ��s

        var oldCell = _cells[_selectedRow, _selectedColumn]; // �I������Ă����Z��
        oldCell.color = Color.white;
        newCell.color = Color.red;

        _selectedRow = row;
        _selectedColumn = column;
        return true;
    }
}
