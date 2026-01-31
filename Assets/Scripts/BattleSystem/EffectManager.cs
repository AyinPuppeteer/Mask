using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public void PlayAnim(string name)
    {
        GetComponent<Animator>().Play(name);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}