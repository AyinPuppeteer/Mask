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
    public void Highlight(bool isHighlight)
    {
        if (isHighlight)
        {
            renderer.color = new Color(1f, 0.2f, 0.2f, 0.7f);
        }
        else
        {
            renderer.color = new Color(0,0,0,0);
        }
    }
}
