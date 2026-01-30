using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//角色面板（显示角色数值和技能的面板）
public class ActorPanel : MonoBehaviour
{
    public static ActorPanel Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    //设置选择的角色
    public void SetActor(Actor actor)
    {

    }
}