using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileType Type;

    private int Row, Column;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private SpriteRenderer HighLight;

    public int Row_ => Row;
    public int Column_ => Column;

    private List<Individual> Individuals = new();
    public List<Individual> Individuals_ => Individuals;

    public void Initialize(TileType type, int row, int column, Sprite tileSprite)
    {
        Type = type;
        Row = row;
        Column = column;
        spriteRenderer.sprite = tileSprite;
    }

    public float Distance(Individual indi)
    {
        return Vector2.Distance(transform.position, indi.transform.position);
    }

    //获取与另一个图块的曼哈顿距离
    public int ManDis(Tile another)
    {
        return Math.Abs(Row - another.Row) + Math.Abs(Column - another.Column);
    }

    public void whenChosen(bool isChosen)
    {
        if (isChosen)
        {
            BattleManager.Instance.ChooseTile(this);
        }
    }
    public void Highlight(bool isHighlight)
    {
        if (isHighlight)
        {
            HighLight.color = new Color(1f, 1f, 1f, 0.6f);
        }
        else
        {
            HighLight.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}

//方格类型
public enum TileType
{
    陆地, 水
}