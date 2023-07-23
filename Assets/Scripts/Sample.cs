using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    GameObject[] _objArray;
    int _point = 0;
    [SerializeField] int _count = 5;

    //プロパティの例
    //private int SelectedIndex
    //{
    //    get => _selectedIndex;
    //    set
    //    {
    //        var oldCell = _cells[_selectedIndex]; // 選択されていたセル
    //        var newCell = _cells[value]; // 選択されるセル
    //        oldCell.color = Color.white;
    //        newCell.color = Color.red;

    //        _selectedIndex = value;
    //    }
    //}

    //private int _selectedIndex; // 選択されているセル番号


    // Start is called before the first frame update
    void Start()
    {
        _objArray = new GameObject[_count];
        for (var i = 0; i < _count; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;
            _objArray[i] = obj;

            var image = obj.AddComponent<Image>();
            if (i == 0)
            {
                image.color = Color.red;
                _point = i;
            }
            else { image.color = Color.white; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            SelecctedIndex(1);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            SelecctedIndex(-1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_objArray[_point]);
        }
    }

    void SelecctedIndex(int i = 1)
    {
        if (_objArray[_point] != null)
        {
            var image = _objArray[_point].GetComponent<Image>();
            image.color = Color.white;
        }
        var tmpDown = _point;
        _point = (_point + i) % _count;
        if (_point == -1) _point = _count - 1;
        while (_objArray[_point] == null)
        {
            _point = (_point + i) % _count;
            if (_point == -1) _point = _count - 1;
            if (tmpDown == _point) break;
        }
        if (_objArray[_point] != null)
        {
            var changeImage = _objArray[_point].GetComponent<Image>();
            changeImage.color = Color.red;

        }
    }
}
