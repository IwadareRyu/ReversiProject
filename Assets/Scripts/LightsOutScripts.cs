using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEditor;

public class LightsOutScripts : MonoBehaviour, IPointerClickHandler
{
    private bool _clearbool;

    [SerializeField] Text _clearText;
    [SerializeField] Text _timeText;
    [SerializeField] Text _turnText;
    int _turn = 0;
    float _time;
    int _blackcount;
    [SerializeField] int _row = 5;
    [SerializeField] int _column = 5;
    // Start is called before the first frame update
    void Start()
    {
        _blackcount = _column * _row / 2; 
        for(var r = 0;r < _row;r++)
        {
            for(var c = 0;c < _column;c++)
            {
                var cell = new GameObject($"Cell({r},{c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                Setting(cell);
            }
        }
        if(_clearText) _clearText.gameObject.SetActive(false);
        if (_turnText) _turnText.text = _turn.ToString();
        GetComponent<GridLayoutGroup>().constraintCount = _column;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clearbool)
        {
            _time += Time.deltaTime;
            if (_timeText) { _timeText.text = _time.ToString("0.00"); }
        }
    }

    /// <summary>クリックした時の処理</summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_clearbool)
        {
            //クリックした位置のセル
            var cell = eventData.pointerCurrentRaycast.gameObject;
            var image = cell.GetComponent<Image>();
            ColorChange(image);
            //名前から番号と取り出し、縦、横の文字を読み取る。
            string[] cellName = cell.name.Replace("Cell(", "").Replace(")", "").Split(",");
            int row = int.Parse(cellName[0]);
            int col = int.Parse(cellName[1]);
            //上のセル
            ChackCell(row - 1, col);
            //下のセル
            ChackCell(row + 1, col);
            //左のセル
            ChackCell(row, col - 1);
            //右のセル
            ChackCell(row, col + 1);

            _turn++;
            if (_turnText) _turnText.text = _turn.ToString();
            _clearbool = ClearChack();
        }
    }

    void Setting(GameObject cell)
    {
        var image = cell.GetComponent<Image>();
        var ram = Random.Range(0,2);
        if(ram == 0)
        {
            image.color = Color.black;
        }
    }

    /// <summary>上下左右のセルを見つける処理</summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    void ChackCell(int row,int col)
    {
        var cell = GameObject.Find($"Cell({row},{col})");
        if(cell)
        {
            var image = cell.GetComponent<Image>();
            ColorChange(image);
        }
    }

    /// <summary>色を変更するときの処理</summary>
    /// <param name="image"></param>
    void ColorChange(Image image) 
    {
        if (image.color == Color.black){ image.color = Color.white; }
        else{ image.color = Color.black; }
    }

    bool ClearChack()
    {
        Image[] images = GetComponentsInChildren<Image>();
        bool clear = true;
        foreach(var image in images)
        {
            if(image.color == Color.white)
            {
                clear = false;
            }
        }

        if(clear)
        {

            if(_clearText) 
            {
                _clearText.gameObject.SetActive(true);
                _clearText.text = "おちんぽ射爆了！"; 
            }
        }
        return clear;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_clearText)
        {
            Debug.Log("わけわかめ");
            var Ai = GetComponent<NavMeshAgent>();
            Ai.nextPosition = Vector2.zero;
            //Ai.agentTypeID = 1;
        }
        else if (_clearbool)
        {
            Debug.Log("まりもり");
            var Ai = GetComponent<NavMeshAgent>();
            Ai.nextPosition = new Vector2(2,6);
            //Ai.agentTypeID = 4;
        }
        else if(_column == 100)
        {
            Debug.Log("ゆうきち");
            var Ai = GetComponent<NavMeshAgent>();
            Ai.nextPosition = new Vector2(1, 3);
            //Ai.agentTypeID = 8;
            goto Tinpo;
        }
        else
        {
            Debug.Log("の神髄");
            var Ai = GetComponent<NavMeshAgent>();
            Ai.nextPosition = new Vector2(51, 72);
            //Ai.agentTypeID = 7;
        }
        Tinpo: Debug.Log("GOwwwwTOwwwwwww");
    }
}
