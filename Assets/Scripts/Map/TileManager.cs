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
    [SerializeField]
    public int maxWidth{ get; private set; }
    [SerializeField]
    public int maxHeight{ get; private set; }

    private List<GameObject> tileList = new List<GameObject>();//地图格子实体列表

    private Tile tile;
    private Camera main2DCamera;
    private void Awake()
    {
        main2DCamera = Camera.main;
        tile = tilePrefab.GetComponent<Tile>();
    }
    private void Start()
    {
        if (tile == null)
        {
            Debug.LogError("Tile component not found on the tilePrefab.");
        }
        maxWidth = 64;
        maxHeight = 64;
        GenerateMap();
    }

    private void Update()
    {
        TileHighlight();//检测选中地图高亮显示
    }

    private void GenerateMap()
    {
        for (int i =0;i < maxWidth; i++)
        {
            for(int j = 0; j < maxHeight; j++)
            {
                tile.Initialize("Grass", i*j+j, spriteList[0]);
                GameObject newtile = Instantiate(tilePrefab, this.transform.position*0.16f + new Vector3((i+0.5f) * 0.16f, (j+0.5f) * 0.16f,0), Quaternion.identity,this.transform);
                tileList.Add(newtile);
            }
        }
    }

    private void TileHighlight()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //Camera main2DCamera = Camera.main;
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = main2DCamera.ScreenToWorldPoint(new Vector3(
            mouseScreenPos.x,
            mouseScreenPos.y,
            0));


            int SelectIndexX = Mathf.FloorToInt((mouseWorldPos.x - this.transform.position.x * 0.16f) / 0.16f);
            int SelectIndexY = Mathf.FloorToInt((mouseWorldPos.y - this.transform.position.y * 0.16f) / 0.16f);


            int SelectIndex = SelectIndexX* maxHeight+SelectIndexY;
            Debug.Log("Selected Tile Index: " + SelectIndex);

            tile = tileList[SelectIndex].GetComponent<Tile>();
            tile.Highlight(true);
        }
    }

    public Tile PositionToTile(Vector2 vec)
    {
        int SelectIndex = Mathf.FloorToInt((vec.x-1) * maxHeight + vec.y);
        tile = tileList[SelectIndex].GetComponent<Tile>();
        return tile;
    }
}
