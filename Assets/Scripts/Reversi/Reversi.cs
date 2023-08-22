using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reversi : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int _cellcount = 8;
    
    [SerializeField] 
    private GridLayoutGroup _grid;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Text _resultText;

    private ReversiCell[,] _cells = null;

    [SerializeField]
    private ReversiCell _cellPrehab;

    private PlayerState _playerState = PlayerState.Player1;

    private float _time;

    private bool _stopBool;

    List<ReversiCell> _changePieceList;

    bool _cantPutOne = false;

    // Start is called before the first frame update
    void Start()
    {
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = _cellcount;
        _changePieceList = new List<ReversiCell>();

        _cells = new ReversiCell[_cellcount, _cellcount];
        var parent = _grid.gameObject.transform;
        for(var i = 0;i < _cellcount;i++)
        {
            for(var j = 0;j < _cellcount;j++)
            {
                var cell = Instantiate(_cellPrehab);
                cell.transform.SetParent(parent);
                cell.name = $"Cell[{i},{j}]";

                _cells[i,j] = cell;
            }
        }
        ResetIntaract();
        _cells[_cellcount / 2 - 1, _cellcount / 2 - 1].CellState = ReversiCellState.Black;
        _cells[_cellcount / 2, _cellcount / 2].CellState = ReversiCellState.Black;
        _cells[_cellcount / 2, _cellcount / 2 - 1].CellState = ReversiCellState.White;
        _cells[_cellcount / 2 - 1, _cellcount / 2].CellState = ReversiCellState.White;
        ChackPutPiece();
        if (_scoreText){ _scoreText.text = ""; }
        if (_resultText) { _resultText.text = ""; }
    }

    void ChackPutPiece()
    {
        bool notPutBool = true;
        for (var i = 0; i < _cellcount; i++)
        {
            for (var j = 0; j < _cellcount; j++)
            {
                var cell = _cells[i, j];

                if (cell.CellState == ReversiCellState.None)
                {
                    var chackBool = false;
                    for (var k = -1; k < 2; k += 2)
                    {
                        ChackAround(i, j, ref chackBool, k);
                        ChackAround(i, j, ref chackBool, 0, k);
                        ChackAround(i, j, ref chackBool, k, k);
                    }
                    ChackAround(i, j, ref chackBool, 1, -1);
                    ChackAround(i, j, ref chackBool, -1, 1);
                    //とれる駒があるかの判定
                    if (chackBool)
                    {
                        StartCoroutine(PutTime(cell));
                        if (notPutBool)
                        {
                            notPutBool = false;
                            _cantPutOne = false;
                        }
                    }
                }
            }
        }
        if(notPutBool)
        {
            if(_cantPutOne)
            {
                Result();
            }
            else
            {
                _cantPutOne = true;
                _playerState = (PlayerState)((int)(_playerState + 1) % 2);
                ChackPutPiece();
            }
        }
    }

    void ChackAround(int i, int j, ref bool chackbool, int iPlus = 0, int jPlus = 0, bool between = false)
    {
        int column = i + iPlus;
        int row = j + jPlus;

        //既に取れる部分があるかのチェック判定と場外判定
        if (!chackbool && column > -1 && column < _cellcount && row > -1 && row < _cellcount)
        {
            var cell = _cells[column, row];
            // 駒があるかの判定
            if (cell.CellState != ReversiCellState.None)
            {
                // プレイヤーはどちらかの判定
                if (_playerState == PlayerState.Player1)
                {
                    //駒の色判定
                    if (cell.CellState == ReversiCellState.White)
                    {
                        between = true;
                        //再帰処理
                        ChackAround(column, row, ref chackbool, iPlus, jPlus, between);
                    }
                    else if (between == true)
                    {
                        chackbool = true;
                        return;
                    }
                }
                else
                {
                    //駒の色判定
                    if (cell.CellState == ReversiCellState.Black)
                    {
                        between = true;
                        //再帰処理
                        ChackAround(column, row, ref chackbool, iPlus, jPlus, between);
                    }
                    else if (between == true)
                    {
                        chackbool = true;
                        return;
                    }
                }
            }
        }
    }

    IEnumerator PutTime(ReversiCell cell)
    {
        yield return new WaitUntil(() => _stopBool);
        cell.OnIntaract();
    }

    void ResetIntaract()
    {
        for (var i = 0; i < _cellcount; i++)
        {
            for (var j = 0; j < _cellcount; j++)
            {
                _cells[i, j].OffIntaract();
            }
        }
    }

    void Result()
    {
        int blackPlayer1Count = 0;
        int whitePlayer2AICount = 0;
        for(var c = 0;c < _cellcount;c++)
        {
            for(var r = 0;r < _cellcount;r++)
            {
                if (_cells[c,r].CellState == ReversiCellState.Black)
                {
                    blackPlayer1Count++;
                }
                else if (_cells[c,r].CellState == ReversiCellState.White)
                {
                    whitePlayer2AICount++;
                }
            }
        }
        if(_scoreText)
        {
            _scoreText.text = $"{blackPlayer1Count}-{whitePlayer2AICount}";
        }
        if(_resultText)
        {
            if(blackPlayer1Count > whitePlayer2AICount)
            {
                _resultText.text = "Player1の勝利！つっよーい！";
            }
            else if(whitePlayer2AICount > blackPlayer1Count)
            {
                _resultText.text = "Player2AIの勝利！ざーこ！";
            }
            else
            {
                _resultText.text = "引き分け！！まことに大儀であった！";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stopBool)
        {
            _time += Time.deltaTime;
        }

        if(_time > 3f)
        {
            _stopBool = true;
            _time = 0f;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        var obj = eventData.pointerCurrentRaycast.gameObject;
        var cell = obj.GetComponent<ReversiCell>();
        if(cell != null && TryGetCellPosition(cell,out var column,out var row))
        {
            ResetIntaract();
            ChangePiece(column,row);
            _stopBool = false;
            ChackPutPiece();
        }
    }

    bool TryGetCellPosition(ReversiCell cell,out int column,out int row)
    {
        for(var c = 0;c < _cellcount;c++)
        {
            for(var r = 0;r < _cellcount;r++)
            {
                if (cell == _cells[c,r])
                {
                    column = c;
                    row = r;
                    return true;
                }
            }
        }

        row = -1; column = -1; 
        return false;
    }

    void ChangePiece(int column,int row)
    {
        var cell = _cells[column,row];
        if (_playerState == PlayerState.Player1)
        {
            cell.CellState = ReversiCellState.Black;

            for (var i = -1; i < 2; i += 2)
            {
                ChangePieceAround(column, row, i);
                ChangePieceAround(column,row,0,i);
                ChangePieceAround(column, row, i, i);
            }
            ChangePieceAround(column, row, -1,1);
            ChangePieceAround(column, row, 1, -1);

            _playerState = PlayerState.Player2AI;
        }
        else
        {
            cell.CellState = ReversiCellState.White;

            for (var i = -1; i < 2; i += 2)
            {
                ChangePieceAround(column, row, i);
                ChangePieceAround(column, row, 0, i);
                ChangePieceAround(column, row, i, i);
            }
            ChangePieceAround(column, row, -1, 1);
            ChangePieceAround(column, row, 1, -1);

            _playerState = PlayerState.Player1;
        }
    }

    void ChangePieceAround(int i,int j,int iPlus = 0,int jPlus = 0)
    {
        int column = i + iPlus;
        int row = j + jPlus;

        //場外判定
        if (column > -1 && column < _cellcount && row > -1 && row < _cellcount)
        {
            var cell = _cells[column, row];
            // 駒があるかの判定
            if (cell.CellState != ReversiCellState.None)
            {
                // プレイヤーはどちらかの判定
                if (_playerState == PlayerState.Player1)
                {
                    //駒の色判定
                    if (cell.CellState == ReversiCellState.White)
                    {
                        _changePieceList.Add(cell);
                        //再帰処理
                        ChangePieceAround(column,row, iPlus, jPlus);
                    }
                    else
                    {
                        ExecutionChangePieceAround(ReversiCellState.Black);
                        return;
                    }
                }
                else
                {
                    //駒の色判定
                    if (cell.CellState == ReversiCellState.Black)
                    {
                        _changePieceList.Add(cell);
                        //再帰処理
                        ChangePieceAround(column,row,iPlus, jPlus);
                    }
                    else
                    {
                        ExecutionChangePieceAround(ReversiCellState.White);
                        return;
                    }
                }
            }
        }
        if (_changePieceList.Count != 0)
        {
            _changePieceList.Clear();
        }
    }

    void ExecutionChangePieceAround(ReversiCellState cellState)
    {
        if (_changePieceList.Count != 0)
        {
            foreach (var cell in _changePieceList)
            {
                cell.CellState = cellState;
            }
            _changePieceList.Clear();
        }
    }
}

public enum PlayerState
{
    Player1,
    Player2AI,
}
