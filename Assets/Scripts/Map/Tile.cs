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

    [SerializeField]
    private SpriteRenderer Renderer;

    private Color color;

    public void Initialize(TileType type, int row, int column, Sprite tileSprite)
    {
        Type = type;
        Row = row;
        Column = column;
        Renderer = GetComponent<SpriteRenderer>();
        if (Renderer != null)
        {
            Renderer.sprite = tileSprite;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the Tile GameObject.");
        }
    }
    public void whenChosen(bool isChosen)
    {
        if (isChosen)
        {
            Renderer.color = new Color(1f, 0.2f, 0.2f, 0.7f);

            Actor actor = GetComponentInChildren<Actor>();
            if (actor != null && !actor.Acting_) 
            {
                if(BattleManager.Instance.ChoosingActor_!=null)
                BattleManager.Instance.ChoosingActor_.Highlight(false);//取消之前选中的高亮

                actor.Highlight(true);//高亮当前选中的
                BattleManager.Instance.ChooseActor(actor);//更新战斗管理器中的选中角色
            }
        }
        else
        {
            Renderer.color = new Color(0,0,0,0);
        }
    }
}

//方格类型
public enum TileType
{
    陆地, 水
}