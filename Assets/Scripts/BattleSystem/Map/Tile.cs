using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileType Type;

    private int Row, Column;
    public int Row_ => Row;
    public int Column_ => Column;

    private List<Individual> Individuals = new();
    public List<Individual> Individuals_ => Individuals;

    public void Initialize(TileType type, int row, int column, Sprite tileSprite)
    {
        Type = type;
        Row = row;
        Column = column;
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
}

//方格类型
public enum TileType
{
    陆地, 水
}