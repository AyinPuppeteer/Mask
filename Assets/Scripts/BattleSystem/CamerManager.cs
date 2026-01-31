using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理摄像机的脚本
public class CamerManager : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;

    private Vector3 RecordMousePosition;//记录鼠标位置
    private bool RightMouseDown;//右键是否按下

    public void MoveTo(Vector3 pos, float duration = 0.3f)
    {
        transform.DOMove(pos, duration);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RecordMousePosition = Input.mousePosition;
            RightMouseDown = true;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            RightMouseDown = false;
        }
        if (RightMouseDown)
        {
            transform.position += MainCamera.ScreenToWorldPoint(RecordMousePosition) - MainCamera.ScreenToWorldPoint(Input.mousePosition);
            RecordMousePosition = Input.mousePosition;
        }
    }
}