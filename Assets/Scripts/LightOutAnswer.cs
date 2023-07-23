using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightOutAnswer : MonoBehaviour, IPointerClickHandler
{
    private GameObject[,] _cells; // �Z���̓񎟌��z��

    private void Start()
    {
        _cells = new GameObject[5, 5]; // �񎟌��z��̏�����
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c] = cell; // ���������Z����z��ɕۑ�
            }
        }

        Reset();
    }

    /// <summary>
    /// �Q�[��������������B
    /// </summary>
    private void Reset()
    {
        // �N���A���Ă����Ԃ�
        Clear(Color.black);

        do
        {
            var rows = _cells.GetLength(0);
            var columns = _cells.GetLength(1);
            for (var i = 0; i < 10; i++) // �K���ȉ񐔃����_���ɃN���b�N
            {
                var r = Random.Range(0, rows);
                var c = Random.Range(0, columns);
                ClickCell(r, c);

                var cell = _cells[r, c];
                Debug.Log($"Click {cell.name}", cell);
            }
        } while (IsAllCell(Color.black) || IsSolvableNext());
        // �N���A��ԁA�܂��͂��ƈ��ŃN���A�\�Ȃ�������
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        if (TryGetCellPosition(cell, out var row, out var column))
        {
            ClickCell(row, column);
            if (IsAllCell(Color.black)) { Debug.Log("�Q�[���N���A"); }
        }
    }

    /// <summary>
    /// ���ׂẴZ���̐F���A�w��̐F�ɏ���������B
    /// </summary>
    /// <param name="color">�F�B</param>
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
    /// ���ׂẴZ�����A�w��̐F�ɐ��܂��Ă��邩�ǂ����B
    /// </summary>
    /// <param name="color">���ׂ�F�B</param>
    /// <returns>���ׂẴZ���� <paramref name="color"/> �ł���� true�B�����łȂ���� false�B</returns>
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
    /// ���̈��ŃQ�[���N���A�\���ǂ����B
    /// </summary>
    /// <returns>���̈��ŃQ�[���N���A�\�Ȃ� true�B�����łȂ���� false�B</returns>
    private bool IsSolvableNext()
    {
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                ClickCell(r, c);
                var isAll = IsAllCell(Color.black);
                ClickCell(r, c); // ���ɖ߂�
                if (isAll) { return true; }
            }
        }
        return false;
    }

    /// <summary>
    /// �w��̍s�ԍ��E��ԍ��̃Z�����N���b�N����B
    /// </summary>
    /// <param name="row">�s�ԍ��B</param>
    /// <param name="column">��ԍ��B</param>
    private void ClickCell(int row, int column)
    {
        SwitchCell(row, column);
        SwitchCell(row + 1, column);
        SwitchCell(row, column + 1);
        SwitchCell(row - 1, column);
        SwitchCell(row, column - 1);
    }

    /// <summary>
    /// �w�肵���Z���̍s�ԍ��Ɨ�ԍ���Ԃ��B
    /// </summary>
    /// <param name="cell">���ׂ�Z���B</param>
    /// <param name="row">�s�ԍ��B</param>
    /// <param name="column">��ԍ��B</param>
    /// <returns>��������� true�B���s����� false�B</returns>
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
    /// �w��̍s�ԍ��E��ԍ��̃Z���̐F�𔽓]������B
    /// </summary>
    /// <param name="row">�s�ԍ��B</param>
    /// <param name="column">��ԍ��B</param>
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
    /// �w�肵���Z���̐F�𔽓]������B
    /// </summary>
    /// <param name="cell">�Z���B</param>
    private void SwitchCell(GameObject cell)
    {
        if (cell.TryGetComponent<Image>(out var image))
        {
            image.color = image.color == Color.white ? Color.black : Color.white;
        }
    }
}
