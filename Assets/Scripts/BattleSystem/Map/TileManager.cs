using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private Sprite[] spriteList;//贴图列表

    public int maxHeight { get; private set; }
    public int maxWidth{ get; private set; }

    private Tile[,] tileList;//地图格子实体列表
    public Tile GetTile(int x, int y)
    {
        if(x > 0 && y > 0 && maxHeight >= x && maxWidth >= y)
        {
            return tileList[x - 1, y - 1];
        }
        else
        {
            return null;
        }
    }

    public Tile GetTile(Vector2 pos)
    {
        int SelectIndexX = Mathf.FloorToInt(maxHeight / 2.0f - (pos.y - transform.position.y) / 0.16f);
        int SelectIndexY = Mathf.FloorToInt((pos.x - transform.position.x) / 0.16f + maxWidth / 2.0f);
        return GetTile(SelectIndexX + 1, SelectIndexY + 1);
    }

    private Camera main2DCamera;

    public static TileManager Instance { get; private set; }

    private void Awake()
    {
        main2DCamera = Camera.main;
        Instance = this;
    }

    private void Update()
    {
        TileChoose();//检测选中地图高亮显示
    }

    public void GenerateMap(MapPack pack)
    {
        if(tileList != null)
        {
            Debug.LogError("重复加载地图！");
        }
        else if (pack == null || pack.Tiles == null)
        {
            Debug.LogWarning("地图规格未设置！");
        }
        else
        {
            maxHeight = pack.Tiles.GetLength(0);
            maxWidth = pack.Tiles.GetLength(1);

            tileList = new Tile[maxHeight, maxWidth];

            for (int i = 0; i < maxHeight; i++)
            {
                for (int j = 0; j < maxWidth; j++)
                {
                    GameObject ob = Instantiate(tilePrefab, transform.position + new Vector3((j - maxWidth / 2.0f) * 0.16f + 0.08f, (maxHeight / 2.0f - i) * 0.16f - 0.08f, 0), Quaternion.identity, transform);
                    Tile tile = ob.GetComponent<Tile>();
                    tile.Initialize((TileType)pack.Tiles[i, j], i, j, spriteList[0]);
                    tileList[i, j] = tile;
                }
            }
        }
    }

    private void TileChoose()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = main2DCamera.ScreenToWorldPoint(Input.mousePosition);

            Tile tile = GetTile(mouseWorldPos);
            if (tile != null) tile.whenChosen(true);
        }
    }

    #region 控制格子高亮
    public void TileChooseSquare(int sx, int sy, int lx, int ly) 
    {
        CancelHighlight();//取消之前的高亮
        for (int i = sx; i < sx + lx; i++)
        {
            for (int j = sy; j < sy + ly; j++)
            {
                Tile tile = GetTile(i, j);
                if(tile != null) tile.Highlight(true);
            }
        }
    }

    public void TileChooseLine(int sx, int sy, int length)
    {
        CancelHighlight();//取消之前的高亮

        Tile centerTile = GetTile(main2DCamera.ScreenToWorldPoint(Input.mousePosition));
        if (centerTile == null) return;
        if (Mathf.Abs (centerTile.Row_- sx)  > Mathf.Abs(centerTile.Column_- sy))
        {
            if (centerTile.Row_ > sx) 
            {
                for (int i = sx; i < sx + length; i++)
                {
                    Tile tile = GetTile(i, sy);
                    if (tile != null) tile.Highlight(true);
                }
            }//方向右
            else
            {
                for (int i = sx; i > sx - length; i--)
                {
                    Tile tile = GetTile(i, sy);
                    if (tile != null) tile.Highlight(true);
                }
            }//方向左
        }
        else
        {
            if (centerTile.Column_ > sy)
            {
                for (int j = sx; j <= sx + length; j++)
                {
                    Tile tile = GetTile(sx, j);
                    if (tile != null) tile.Highlight(true);
                }
            } //方向上
            else
            {
                for (int j = sx; j >= sx - length; j++)
                {
                    Tile tile = GetTile(sx, j);
                    if (tile != null) tile.Highlight(true);
                }
            }//方向下
        }
    }

    public void CancelHighlight()
    {
        for (int i = 0; i < maxHeight; i++)
        {
            for (int j = 0; j < maxWidth; j++)
            {
                tileList[i, j].Highlight(false);
            }
        }
    }
    #endregion
}