using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualManager : MonoBehaviour
{
    private List<GameObject> ActorObs = new();
    private List<GameObject> EnemyObs = new();

    public static IndividualManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static GameObject ReturnActor(string name)
    {
        foreach(var ob in Instance.ActorObs)
        {
            Individual indi = ob.GetComponent<Individual>();
            if(indi != null && indi.Name_ == name)
            {
                return ob;
            }
        }

        Debug.LogError($"未查找到该名字的角色：{name}");
        return null;
    }

    public static GameObject ReturnEnemy(string name)
    {
        foreach (var ob in Instance.EnemyObs)
        {
            Individual indi = ob.GetComponent<Individual>();
            if (indi != null && indi.Name_ == name)
            {
                return ob;
            }
        }

        Debug.LogError($"未查找到该名字的敌人：{name}");
        return null;
    }
}