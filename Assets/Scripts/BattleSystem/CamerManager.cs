using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理摄像机的脚本
public class CamerManager : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;

    public void MoveTo(Vector3 pos, float duration = 0.3f)
    {
        transform.DOMove(pos, duration);
    }
}