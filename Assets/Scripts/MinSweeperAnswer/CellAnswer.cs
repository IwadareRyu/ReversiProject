using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellAnswer : MonoBehaviour
{
    [SerializeField]
    private Text _view = null; // ��Ԃ�\������e�L�X�g

    [SerializeField]
    private Image _cover = null; // �w�ʂ��B���W

    [SerializeField]
    private CellState _cellState = CellState.None;
    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    /// <summary>
    /// �Z�����J���Ă��邩�ǂ����B
    /// </summary>
    public bool IsOpen => !_cover.enabled;

    /// <summary>
    /// ���Ă���Z�����J���B
    /// </summary>
    public void Open() => _cover.enabled = false;

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (_view == null) { return; }

        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }
}
