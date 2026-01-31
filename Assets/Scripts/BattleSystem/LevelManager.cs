using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    //显示和隐藏
    public void Show(bool b)
    {
        if (b)
        {

        }
        else
        {

        }
    }

    //选定关卡
    public void ChooseLevel()
    {

    }

    //开始游戏
    public void StartLevel()
    {

    }

    public static LevelPack ReturnPack()
    {
        LevelPack pack = new();
        MapPack map = new();
        map.Tiles = new int[16, 16]{ { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                     { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}};
        pack.AddIndividual("见习战士", 5, 5);
        pack.AddIndividual("骷髅", 6, 6);
        pack.MapPack_ = map;
        return pack;
    }
}

//关卡配置包
public class LevelPack
{
    private MapPack MapPack;
    public MapPack MapPack_ { get => MapPack; set => MapPack = value; }

    private Dictionary<(int, int), string> IndividualNames = new();
    public Dictionary<(int, int), string> IndividualNames_ => IndividualNames;
    public void AddIndividual(string name, int x, int y)
    {
        if(IndividualNames.ContainsKey((x, y)))
        {
            Debug.LogError($"在位置({x}, {y})处添加名为{name}的角色失败！该位置已有名为{IndividualNames[(x, y)]}的角色！");
        }
        IndividualNames.Add((x, y), name);
    }
}

//地图配置包
public class MapPack
{
    public int[,] Tiles;
}