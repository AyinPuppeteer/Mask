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
}