using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LifeGameScripts : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Button _startButton;

    [SerializeField]
    Button _stopButton;

    [SerializeField]
    Button _speedUpButton;

    [SerializeField]
    Button _speedDownButton;

    [SerializeField]
    Text _speedText;

    [SerializeField] 
    int _rows = 10;
    
    [SerializeField] 
    int _columns = 10;

    [SerializeField]
    GridLayoutGroup _grid;

    [SerializeField]
    LifeGameCell _cellPrehab;

    private LifeGameCell[,] _cells;

    private LifeGameState _gameState = LifeGameState.Stand;

    float _time = 0f;

    float _speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _columns;

        _cells = new LifeGameCell[_rows, _columns];
        var parent = _grid.gameObject.transform;
        for(var i = 0; i < _rows;i++)
        {
            for(var j = 0;j < _columns;j++)
            {
                var cell = Instantiate(_cellPrehab);
                cell.transform.SetParent(parent);

                _cells[i,j] = cell;
            }
        }
        _startButton.onClick.AddListener(GameStart);
        _stopButton.onClick.AddListener(GameStop);
        _speedUpButton.onClick.AddListener(() => SpeedUpDown(-0.1f));
        _speedDownButton.onClick.AddListener(() => SpeedUpDown(0.1f));
        ShowText();
    }

    void GameStart()
    {
        _gameState = LifeGameState.Game;
    }

    void GameStop()
    {
        _gameState = LifeGameState.Stand;
    }

    void SpeedUpDown(float count)
    {

        _speed = Mathf.Max(_speed + count,0.1f);

        ShowText();
    }

    void ShowText()
    {
        _speedText.text = _speed.ToString("0.0") + "•b";
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameState == LifeGameState.Game)
        {
            _time += Time.deltaTime;
            if (_time >= _speed)
            {
                CountCells();
                _time = 0;
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.Space))
            {
                _time += Time.deltaTime;
                if(_time >= 0.1f)
                {
                    CountCells();
                    _time = 0;
                }
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                _time = 0;
            }
        }
    }

    void CountCells()
    {
        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _columns; j++)
            {
                var cell = _cells[i, j];
                if(cell.CellState == LifeGameCellState.Alive)
                {
                    for(var k = -1;k < 2;k += 2)
                    {
                        CountUp(i + k, j + k);
                        CountUp(i + k, j);
                        CountUp(i, j + k);
                    }
                    CountUp(i + 1, j - 1);
                    CountUp(i - 1, j + 1);
                }
            }
        }
        ChangeCells();
    }

    void CountUp(int row,int column)
    {
        if (row >= 0 && column >= 0 && row < _rows && column < _columns)
        {
            _cells[row, column]._count++;
        }
    }

    void ChangeCells()
    {
        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _columns; j++)
            {
                var cell = _cells[i, j];
                if(cell.CellState == LifeGameCellState.Alive)
                {
                    if(cell._count != 2 && cell._count != 3)
                    {
                        cell.CellState = LifeGameCellState.Dead;
                    }
                }
                else
                {
                    if(cell._count == 3)
                    {
                        cell.CellState |= LifeGameCellState.Alive;
                    }
                }
                cell._count = 0;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_gameState == LifeGameState.Stand)
        {
            var obj = eventData.pointerCurrentRaycast.gameObject;
            var cell = obj.GetComponent<LifeGameCell>();
            if (cell != null) 
            {
                cell.CellState = (LifeGameCellState)(((int)cell.CellState + 1) % 2); 
            }
        }
    }
}

public enum LifeGameState
{
    Stand,
    Game,
}
