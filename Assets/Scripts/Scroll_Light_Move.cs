using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Scroll_Light_Move : MonoBehaviour
{
    Rigidbody2D rb;
    Action Ondestroy;
    Vector2 vec_ = new Vector2();
    [SerializeField, Header("爆発時間")] float time_;
    [SerializeField, Header("爆発サイズ")] float maxRadius;
    bool collition_ = false;

    Light2D Light2D;
    public void SetVector(Vector2 vec)
    {
        rb = GetComponent<Rigidbody2D>();
        vec_ = vec;
        rb.linearVelocity = vec_;
        collition_ = false;

        Light2D = transform.GetChild(0).GetComponent<Light2D>();
        Light2D.intensity = 1;
        Light2D.shapeLightFalloffSize = 2;
    }

    public void SetFunc(Action action)
    {
        Ondestroy = action;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collition_)
        {
            collition_ = true;
            StartCoroutine(dertorylight(time_));
        }
    }

    IEnumerator dertorylight(float time)
    {
        rb.linearVelocity = new Vector2();

        float timer = 0f;
        float startRadius = 0f;
        float startIntensity = 3f;

        Light2D.pointLightOuterRadius = startRadius;
        Light2D.intensity = startIntensity;

        while (timer < time)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            float radius;

            if (t < 0.3f) {
                // 爆発的に広がる
                radius = Mathf.Lerp(startRadius, maxRadius, t / 0.3f);
            }
            else {
                // 徐々に縮む
                radius = Mathf.Lerp(maxRadius, 0f, (t - 0.3f) / 0.7f);
            }

            Light2D.shapeLightFalloffSize = radius;

            // 光量減衰
            Light2D.intensity = Mathf.Lerp(startIntensity, 0f, t);

            yield return null;
        }
        Ondestroy?.Invoke();
        collition_ = false;
        yield break;
    }
}
