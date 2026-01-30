using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private string type;
    [SerializeField]
    private int index;
    [SerializeField]
    private SpriteRenderer renderer;

    private Color color;

    public void Initialize(string tileType, int selectIndex, Sprite tileSprite)
    {
        type = tileType;
        index = selectIndex;
        renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = tileSprite;
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
            renderer.color = new Color(1f, 0.2f, 0.2f, 0.7f);

            Actor actor = GetComponentInChildren<Actor>();
            if (actor != null&&!actor.Acting_) {
                if(BattleManager.Instance.ChoosingActor_!=null)
                BattleManager.Instance.ChoosingActor_.Highlight(false);//取消之前选中的高亮

                actor.Highlight(true);//高亮当前选中的
                BattleManager.Instance.ChooseActor(actor);//更新战斗管理器中的选中角色
            }
        }
        else
        {
            renderer.color = new Color(0,0,0,0);
        }
    }
}
