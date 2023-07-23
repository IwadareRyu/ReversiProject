using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    void Start()
    {
        ulong data = 0b11111111_10011001_10100101_11110001_10001111_10000001_10000001_11111111;
        GenerateMap(data);
    }

    private void GenerateMap(ulong data)
    {
        for (var r = 0; r < 8; r++)
        {
            var line = (data >> (r * 8));
            GenerateLine(line, r);
        }
    }
    private void GenerateLine(ulong data, int y)
    {
        var x = 0;
        for (var i = 7; i >= 0; i--, x++)
        {
            var cell = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cell.transform.position = new(x * 1.1F, y * 1.1F, 0);

            var isWhite = ((data >> i) & 1) == 0;
            var renderer = cell.GetComponent<Renderer>();
            renderer.material.color = isWhite ? Color.white : Color.black;
        }
    }
}
