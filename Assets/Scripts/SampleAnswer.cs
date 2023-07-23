using UnityEngine;
using UnityEngine.UI;

public class SampleAnswer : MonoBehaviour
{
    [SerializeField]
    private int _count = 5;

    private Image[] _cells; // セルの配列

    private int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            var oldCell = _cells[_selectedIndex]; // 選択されていたセル
            var newCell = _cells[value]; // 選択されるセル
            oldCell.color = Color.white;
            newCell.color = Color.red;

            _selectedIndex = value;
        }
    }
    private int _selectedIndex; // 選択されているセル番号

    private void Start()
    {
        _cells = new Image[_count]; // 配列の初期化
        for (var i = 0; i < _cells.Length; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            var image = obj.AddComponent<Image>();
            if (i == 0) { image.color = Color.red; }
            else { image.color = Color.white; }

            _cells[i] = image; // 配列に保存
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            TryMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            TryMoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveCell();
        }
    }

    private void RemoveCell()
    {
        var cell = _cells[SelectedIndex];
        cell.enabled = false;

        if (!TryMoveLeft()) // 左の選択が失敗したら
        {
            TryMoveRight(); // 右を選択する
        }
    }

    private bool TryMoveLeft()
    {
        var selectedIndex = SelectedIndex - 1;
        while (selectedIndex >= 0)
        {
            var cell = _cells[selectedIndex];
            if (cell.enabled)
            {
                SelectedIndex = selectedIndex;
                return true;
            }
            selectedIndex--;
        }
        return false;
    }

    private bool TryMoveRight()
    {
        var selectedIndex = SelectedIndex + 1;
        while (selectedIndex < _cells.Length)
        {
            var cell = _cells[selectedIndex];
            if (cell.enabled)
            {
                SelectedIndex = selectedIndex;
                return true;
            }
            selectedIndex++;
        }
        return false;
    }
}