using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGameCell : MonoBehaviour
{

    [SerializeField] 
    LifeGameCellState _cellState = LifeGameCellState.Dead;

    private Image _image;

    public LifeGameCellState CellState
    { 
        get => _cellState;
        set 
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    void OnCellStateChanged()
    {
        if (_image == null) { return; }
        if(_cellState == LifeGameCellState.Dead)
        {
            _image.color = Color.white;
        }
        else
        {
            _image.color = Color.black;
        }
    }
}

public enum LifeGameCellState
{
    Dead,
    Alive,
}
