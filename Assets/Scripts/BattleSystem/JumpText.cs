using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Text;

    private float Height = 0.15f;
    private float Distance;

    private float Duration = 0.7f;
    private float Timer;

    private Vector3 InitialPos;

    public void SetText(string text, Color color)
    {
        Text.text = text;
        Text.color = color;
    }

    private void Start()
    {
        Distance = Random.Range(-0.15f, 0.15f);
        InitialPos = transform.position;
    }

    private void Update()
    {
        if ((Timer += Time.deltaTime) >= Duration)
        {
            if(Timer >= Duration * 1.3f)
            {
                Destroy(gameObject);
                return;
            }
            Text.alpha = 1.3f - Timer / Duration;
        }
        else
        {
            Vector3 pos = new(0, 0, 0);
            pos.x = Distance / Duration * Timer;
            float a = -4 * Height / (Distance * Distance);
            pos.y = a * pos.x * pos.x - a * Distance * pos.x;
            transform.position = pos + InitialPos;
        }
    }
}