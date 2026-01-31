using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> IndividualObs = new();

    [SerializeField]
    private Material NormalMat;
    public Material NormalMat_ => NormalMat;
    [SerializeField]
    private Material HighLightMat;
    public Material HighLightMat_ => HighLightMat;

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
    public void CreateIndividual(string name, Tile tile)
    {
        GameObject model = ReturnIndividual(name);
        if (model == null) return;
        GameObject ob = Instantiate(model, tile.transform.position, Quaternion.identity, tile.transform);
        Individual indi = ob.GetComponent<Individual>();
        tile.Individuals_.Add(indi);
        indi.SetTile(tile);
    }

    #region 获取单位
    public static Individual[] ReturnAllIndividuals()
    {
        return Instance.GetComponentsInChildren<Individual>();
    }

    public static Actor[] ReturnAllActors()
    {
        return Instance.GetComponentsInChildren<Actor>();
    }

    public static Enemy[] ReturnAllEnemys()
    {
        return Instance.GetComponentsInChildren<Enemy>();
    }
    #endregion
}