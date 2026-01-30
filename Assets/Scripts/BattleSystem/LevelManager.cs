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

    public static LevelPack ReturnPack()
    {
        return null;
    }
}

//关卡配置包
public class LevelPack
{
    private MapPack MapPack;
    public MapPack MapPack_ { get => MapPack; set => MapPack = value; }
}

//地图配置包
public class MapPack
{

}