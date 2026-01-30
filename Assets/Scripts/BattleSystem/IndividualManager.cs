using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> IndividualObs = new();

    private List<Individual> Individuals = new();
    private List<Actor> Actors = new();
    private List<Enemy> Enemies = new();

    public static IndividualManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static GameObject ReturnIndividual(string name)
    {
        foreach(var ob in Instance.IndividualObs)
        {
            Individual indi = ob.GetComponent<Individual>();
            if(indi != null && indi.Name_ == name)
            {
                return ob;
            }
        }

        Debug.LogError($"未查找到该名字的单位：{name}");
        return null;
    }

    //创造单位
    public void CreateIndividual(string name)
    {
        GameObject model = ReturnIndividual(name);
        if (model == null) return;
        //如果对应格子已有单位则驳回
        GameObject ob = Instantiate(model);
        Individual indi = ob.GetComponent<Individual>();
        Individuals.Add(indi);
        if(indi is Actor actor)
        {
            Actors.Add(actor);
        }
        else if(indi is Enemy enemy)
        {
            Enemies.Add(enemy);
        }
    }

    #region 获取单位
    public static List<Individual> ReturnAllIndividuals()
    {
        return Instance.Individuals;
    }

    public static List<Actor> ReturnAllActors()
    {
        return Instance.Actors;
    }

    public static List<Enemy> ReturnAllEnemys()
    {
        return Instance.Enemies;
    }
    #endregion
}