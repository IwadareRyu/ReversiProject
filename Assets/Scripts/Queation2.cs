using UnityEngine;
using UnityEngine.UI;

public class Queation2 : MonoBehaviour
{
    [SerializeField] int _row;
    [SerializeField] int _column;
    Image[,] _cells;
    GridLayoutGroup _group;
    int _width;
    int _height;
    private int LeftRightSelectedIndex
    {
        get => _width;
        set
        {
            if (_cells[_height, _width].enabled)
            {
                var oldPanel = _cells[_height, _width].color = Color.white;
            }
            var newPanel = _cells[_height, value].color = Color.red;

            _width = value;
        }
    }
    private int UpDownSelectedIndex
    {
        get => _height;
        set
        {
            if (_cells[_height, _width].enabled)
            {
                var oldPanel = _cells[_height, _width].color = Color.white;
            }
            var newPanel = _cells[value, _width].color = Color.red;

            _height = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _group = GetComponent<GridLayoutGroup>();
        _group.constraintCount = _row;
        _cells = new Image[_column, _row];
        for (var r = 0;r < _column;r++)
        {
            for(var c = 0;c < _row;c++)
            {
                var obj = new GameObject($"Cell({r},{c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0) { image.color = Color.red; }
                else { image.color = Color.white; }
                _cells[r, c] = image;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) // 左キーを押した
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) // 右キーを押した
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) // 上キーを押した
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) // 下キーを押した
        {
            MoveDown();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _cells[_height, _width].enabled = false;
            //_cells[_height,_width].gameObject.SetActive(false);
        }
    }

    void MoveUp()
    {
        var selectIndex = UpDownSelectedIndex - 1;
        while (selectIndex >= 0)
        {
            if (_cells[selectIndex, _width].enabled)
            {
                UpDownSelectedIndex = selectIndex;
                return;
            }
            selectIndex--;
        }
    }

    void MoveDown()
    {
        var selectIndex = UpDownSelectedIndex + 1;
        while (selectIndex < _cells.GetLength(0))
        {
            if (_cells[selectIndex, _width].enabled)
            {
                UpDownSelectedIndex = selectIndex;
                return;
            }
            selectIndex++;
        }
    }

    void MoveLeft()
    {
        var selectIndex = LeftRightSelectedIndex - 1;
        while(selectIndex >= 0)
        {
            if(_cells[_height, selectIndex].enabled)
            {
                LeftRightSelectedIndex = selectIndex;
                return;
            }
            selectIndex--;
        }
    }

    void MoveRight()
    {
        var selectIndex = LeftRightSelectedIndex + 1;
        while (selectIndex < _cells.GetLength(1))
        {
            if (_cells[_height, selectIndex].enabled)
            {
                LeftRightSelectedIndex = selectIndex;
                return;
            }
            selectIndex++;
        }
    }
}
