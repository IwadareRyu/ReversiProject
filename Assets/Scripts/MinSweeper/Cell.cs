using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text _view = null;
    [SerializeField]
    public CellState _cellState = CellState.None;
    [SerializeField]
    private Sprite _sprite = null;
    [SerializeField]
    public Image _image = null;

    public Flag _flag = Flag.NotFlag;
    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    private void Start()
    {
        TextEnable();
    }

    void TextEnable()
    {
        GetComponent<Image>().color = Color.blue;
        var text = GetComponentInChildren<Text>();
        if (text != null) { text.enabled = false; };
    }

    // Start is called before the first frame update
    void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if(_view == null) { return; }
        var sprite = GetComponent<Image>();

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
            switch (_cellState)
            {
                case CellState.One:
                    _view.color = Color.blue;
                    break;
                case CellState.Two:
                    _view.color = Color.yellow;
                    break;
                case CellState.Three:
                    _view.color = Color.cyan;
                    break;
                case CellState.Four:
                    _view.color = Color.gray;
                    break;
                case CellState.Five:
                    _view.color = Color.magenta;
                    break;
                case CellState.Six:
                    _view.color = Color.green;
                    break;
                case CellState.Seven:
                    _view.color = Color.black;
                    break;
                case CellState.Eight:
                    _view.color = Color.white;
                    break;
            }
        }
    }
}

public enum Flag
{
    NotFlag = 0,
    Flag = 1,
}
