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

    private float Duration = 0.5f;
    private float Timer;

    private Vector3 InitialPos;

    public void SetText(string text, Color color)
    {
        Text.text = text;
        Text.color = color;
    }

    private void Start()
    {
        Distance = Random.Range(-1f, 1f);
        InitialPos = transform.position;
    }

    private void Update()
    {
        if ((Timer += Time.deltaTime) >= Duration)
        {
            transform.position = InitialPos + new Vector3(Distance, Height, 0);
            Text.alpha = 0f;
        }
        else
        {
            Vector3 pos = new(0, 0, 0);
            pos.x = Distance / Duration * Timer;
            float a = -4 * Height / (Distance * Distance);
            pos.y = a * pos.x * pos.x - a * Distance * pos.x;
            transform.position = pos + InitialPos;
            if(pos.x >= Distance * 0.7f)
            {
                Text.alpha = (1 - pos.x / Distance) / 0.3f;
            }
        }
    }
}