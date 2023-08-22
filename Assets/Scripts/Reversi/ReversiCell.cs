using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReversiCell : MonoBehaviour
{

    [SerializeField]
    private Image _vaildImage;

    [SerializeField]
    private Image _cellImage;

    [SerializeField]
    private Sprite _noneSprite, _whiteSprite, _blackSprite;

    [SerializeField]
    private ReversiCellState _cellState = ReversiCellState.None;

    public ReversiCellState CellState
    { 
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    void OnCellStateChanged()
    {
        if (_cellState == ReversiCellState.None)
        {
            _cellImage.sprite = _noneSprite;
        }
        else if(_cellState == ReversiCellState.White)
        {
            _cellImage.sprite = _whiteSprite;
        }
        else
        {
            _cellImage.sprite = _blackSprite;
        }
    }

    public void OffIntaract()
    {
        _vaildImage.enabled = false;
        _cellImage.raycastTarget = false;
    }

    public void OnIntaract()
    {
        _vaildImage.enabled = true;
        _cellImage.raycastTarget = true;
    }
}
