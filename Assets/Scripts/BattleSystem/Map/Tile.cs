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
            spriteRenderer.color = new Color(0.4f, 0.2f, 0.2f);
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f);
        }
    }
}

//方格类型
public enum TileType
{
    陆地, 水
}