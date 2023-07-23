using UnityEngine;
using UnityEngine.UI;

public class SampleAnswer : MonoBehaviour
{
    [SerializeField]
    private int _count = 5;

    private Image[] _cells; // �Z���̔z��

    private int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            var oldCell = _cells[_selectedIndex]; // �I������Ă����Z��
            var newCell = _cells[value]; // �I�������Z��
            oldCell.color = Color.white;
            newCell.color = Color.red;

            _selectedIndex = value;
        }
    }
    private int _selectedIndex; // �I������Ă���Z���ԍ�

    private void Start()
    {
        _cells = new Image[_count]; // �z��̏�����
        for (var i = 0; i < _cells.Length; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            var image = obj.AddComponent<Image>();
            if (i == 0) { image.color = Color.red; }
            else { image.color = Color.white; }

            _cells[i] = image; // �z��ɕۑ�
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���L�[��������
        {
            TryMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // �E�L�[��������
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

        if (!TryMoveLeft()) // ���̑I�������s������
        {
            TryMoveRight(); // �E��I������
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