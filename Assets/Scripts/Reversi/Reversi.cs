using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reversi : MonoBehaviour
{
    private int _cellcount = 8;
    
    [SerializeField] 
    private GridLayoutGroup _grid;

    private ReversiCell[,] _cells = null;

    [SerializeField]
    private ReversiCell _cellPrehab;
    // Start is called before the first frame update
    void Start()
    {
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _cellcount;

        _cells = new ReversiCell[_cellcount, _cellcount];
        var parent = _grid.gameObject.transform;
        for(var i = 0;i < _cellcount;i++)
        {
            for(var j = 0;j < _cellcount;j++)
            {
                var cell = Instantiate(_cellPrehab);
                cell.transform.SetParent(parent);

                _cells[i,j] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
