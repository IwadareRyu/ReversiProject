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
    int rows = 10;
    
    [SerializeField] 
    int columns = 10;

    [SerializeField]
    GridLayoutGroup _grid;

    [SerializeField]
    LifeGameCell _cellPrehab;

    private LifeGameCell[,] _cells;

    private LifeGameState _gameState = LifeGameState.Stand;

    // Start is called before the first frame update
    void Start()
    {
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = columns;

        _cells = new LifeGameCell[rows, columns];
        var parent = _grid.gameObject.transform;
        for(var i = 0; i < rows;i++)
        {
            for(var j = 0;j < columns;j++)
            {
                var cell = Instantiate(_cellPrehab);
                cell.transform.SetParent(parent);

                _cells[i,j] = cell;
            }
        }
        _startButton.onClick.AddListener(GameStart);
        _stopButton.onClick.AddListener(GameStop);
    }

    void GameStart()
    {
        _gameState = LifeGameState.Game;
    }

    void GameStop()
    {
        _gameState = LifeGameState.Stand;
    }

    // Update is called once per frame
    void Update()
    {
        
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
