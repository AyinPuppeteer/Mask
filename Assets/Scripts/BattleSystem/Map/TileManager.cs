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

    private void TileChooseSquare(int length,int width,Vector2 position) { 
        Tile centerTile = GetTile(position);

        centerTile.whenChosen(true);//选中高亮方框中心

        int row = centerTile.Row_;
        int colum = centerTile.Column_;

        if (length % 2 != 0)
        {
            length = (length - 1) / 2;
        }
        else
        {
            length /= 2;
        }
        if (width % 2 != 0)
        {
            width = (width - 1) / 2;
        }
        else
        {
            width /= 2;
        }
            for (int i = row - length; i <= row + length; i++)
            {
                for (int j = colum - width; j <= colum + width; j++)
                {
                    Tile tile = tileList[i, j];
                    tile.Highlight(true);
                }
            }
    }

    private void TileChooseLine(int length,Vector2 position)
    {
        Tile centerTile = GetTile(position);
        Tile actorTile = BattleManager.Instance.ChoosingActor_.GetComponentInParent<Tile>();
        if (Mathf.Abs (centerTile.Row_-actorTile.Row_)  > Mathf.Abs( centerTile.Column_-actorTile.Column_))
        {
            if (centerTile.Row_ > actorTile.Row_) {
                for (int i = actorTile.Row_; i <= actorTile.Row_ + length; i++)
                {
                    tileList[i, actorTile.Column_].Highlight(true);
                }
                tileList[actorTile.Row_ + length, actorTile.Column_].whenChosen(true);//选中条形高亮末端
            }//方向右
            else
            {
                for (int i = actorTile.Row_; i >= actorTile.Row_ - length; i--)
                {
                    tileList[i, actorTile.Column_].Highlight(true);
                }
                tileList[actorTile.Row_ - length, actorTile.Column_].whenChosen(true);//选中条形高亮末端
            }//方向左
        }

        if (Mathf.Abs(centerTile.Row_ - actorTile.Row_) < Mathf.Abs(centerTile.Column_ - actorTile.Column_))
        {
            if (centerTile.Column_ > actorTile.Column_) {
                for (int j = actorTile.Row_; j <= actorTile.Row_ + length; j++)
                {
                    tileList[actorTile.Row_, j].Highlight(true);
                }
                tileList[actorTile.Row_, actorTile.Column_ + length].whenChosen(true);//选中条形高亮末端
            } //方向上
            else
            {
                for (int j = actorTile.Row_; j >= actorTile.Row_ - length; j++)
                {
                    tileList[actorTile.Row_, j].Highlight(true);
                }
                tileList[actorTile.Row_, actorTile.Column_ - length].whenChosen(true);//选中条形高亮末端
            }//方向下
        }
    }
}