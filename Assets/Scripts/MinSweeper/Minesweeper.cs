using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour, IPointerClickHandler
{
    ClickOpen[,] _clickOpen;

    [SerializeField]
    private int _rows = 1;

    [SerializeField]
    private int _columns = 1;

    [SerializeField]
    private int _mineCount = 1;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private Cell _cellPrefab = null;

    Cell[,] _cells = null;

    float _time = 0;

    [SerializeField]
    private Color _noFadeColor;

    [SerializeField]
    private Color _fadeColor;

    [SerializeField]
    private Text _clearText;

    bool _clearbool;

    bool _gameoverbool;

    bool _inishial;

    bool _clocktime;

    // Start is called before the first frame update
    void Awake()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _columns;

        _cells = new Cell[_rows, _columns];
        _clickOpen = new ClickOpen[_rows, _columns];
        var parent = _gridLayoutGroup.gameObject.transform;
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);

                cell.transform.SetParent(parent);
                _cells[r, c] = cell;
            }
        }

        _mineCount = Mathf.Min(_mineCount, _rows * _columns);


        if(_clearText)_clearText.text = "";
    }

    int SetMine(int i,int initialrow,int initialcolumn)
    {
        var r = Random.Range(0, _rows);
        var c = Random.Range(0, _columns);
        var cell = _cells[r, c];
        if (cell.CellState == CellState.Mine || cell == _cells[initialrow,initialcolumn])
        {
            i--;
        }
        else
        {
            cell.CellState = CellState.Mine;
            _clickOpen[r, c] = ClickOpen.Explosion;
            AroundNumber(r, c);
        }
        return i;
    }

    void AroundNumber(int r,int c)
    {
        for (var i = -1; i < 2; i+= 2)
        {
            ChangeCellState(r + i, c + i);
            ChangeCellState(r + i, c);
            ChangeCellState(r, c + i);
        }
        ChangeCellState(r - 1, c + 1);
        ChangeCellState(r + 1, c - 1);
    }

    void ChangeCellState(int r,int c)
    {
        if(r > -1 && c > -1 && r < _rows && c < _columns && _cells[r,c].CellState != CellState.Mine)
        {
            _cells[r, c].CellState += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clearbool && _clocktime) { _time += Time.deltaTime; }
    }

    /// <summary>課題3も先にやりました。</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_clearbool && !_gameoverbool)
        {
            var obj = eventData.pointerCurrentRaycast.gameObject;
            Cell cell = obj.GetComponent<Cell>();
            if (cell)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    if (TeyGetCellPosition(cell, out var row, out var column) && cell._flag == Flag.NotFlag)
                    {
                        cell.GetComponent<Image>().color = Color.white;
                        var text = cell.GetComponentInChildren<Text>();
                        text.enabled = true;
                        if (!_inishial)
                        {
                            _inishial = true;
                            _clocktime = true;
                            for (var i = 0; i < _mineCount; i++){ i = SetMine(i, row, column); }
                        }

                        if (_clickOpen[row, column] == ClickOpen.Explosion){ Explosion(); }
                        else
                        {
                            _clickOpen[row, column] = ClickOpen.Open;
                            cell._flag = Flag.Flag;
                            if (cell.CellState == CellState.None){ BlankSearch(row, column); }
                                bool clear = Juage();
                            if (clear){ Clear(); }
                        }
                    }
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    if (cell._flag == Flag.NotFlag)
                    {
                        cell._image.color = _noFadeColor;
                        cell._flag = Flag.Flag;
                    }
                    else
                    {
                        cell._image.color = _fadeColor;
                        cell._flag = Flag.NotFlag;
                    }
                }
            }
        }
    }

    private void Explosion()
    {
        Debug.Log("お前はもう、死んでいる。");
        if (_clearText) _clearText.text = "ドカーン！！";
        _gameoverbool = true;
    }

    private void Clear()
    {
        Debug.Log("クリアだよ");
        if (_clearText) _clearText.text = $"ゲームクリア！\nクリアタイム{_time.ToString("f2")}秒!";
        _clearbool = true;
    }

    private bool Juage()
    {
        bool clear = true;
        for(var row = 0;row < _clickOpen.GetLength(0);row++)
        {
            for(var column = 0;column < _clickOpen.GetLength(1);column++)
            {
                if (_clickOpen[row,column] == ClickOpen.Close)
                {
                    clear = false;
                    break;
                }
            }
        }
        return clear;
    }

    private bool TeyGetCellPosition(Cell obj,out int row,out int column)
    {
        var cell = obj.GetComponent<Cell>();
        if(cell)
        {
            for(var r = 0;r < _cells.GetLength(0);r++)
            {
                for(var c = 0; c < _cells.GetLength(1);c++)
                {
                    if(cell == _cells[r,c])
                    {
                        row = r;
                        column = c;
                        return true;
                    }
                }
            }
        }

        row = -1;column = -1;
        return false;
    }

    void BlankSearch(int r,int c)
    {
        for (var i = -1; i < 2; i += 2)
        {
            //BlankSearchProcess(r + i, c + i);
            BlankSearchProcess(r + i, c);
            BlankSearchProcess(r, c + i);
        }
        //BlankSearchProcess(r - 1, c + 1);
        //BlankSearchProcess(r + 1, c - 1);
    }

    void BlankSearchProcess(int row,int column)
    {
        if (row > -1 && column > -1 && row < _rows && column < _columns)
        {
            if (_clickOpen[row, column] == ClickOpen.Close)
            {
                _cells[row,column].GetComponent<Image>().color = Color.white;
                _clickOpen[row, column] = ClickOpen.Open;
                _cells[row,column]._flag = Flag.Flag;
                _cells[row, column].GetComponentInChildren<Text>().enabled = true; 
                if (_cells[row, column]._cellState == CellState.None)
                {
                    BlankSearch(row, column);
                }
            }
        }
    }
}

public enum ClickOpen
{
    Close = 0,
    Open = 1,
    Explosion = 2
}
