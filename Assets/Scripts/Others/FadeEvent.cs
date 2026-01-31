using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//管理转场的脚本
public class FadeEvent : MonoBehaviour
{
    [SerializeField]
    private Animator Anim;

    private string SceneName;//需要切换到的场景名字

    public void SceneTransform()
    {
        if (SceneName == null) return;
        SceneManager.LoadScene(SceneName);
    }

    public void FadeTo(string scenename)
    {
        SceneName = scenename;
        Anim.Play("转场-Show");
    }

    public void FakeFade()
    {
        SceneName = null;
    }
}