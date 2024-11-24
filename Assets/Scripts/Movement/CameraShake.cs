using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    // duration은 진동시간, magnitude는 진동세기
    public IEnumerator Shake(float duration, float magnitude)
    {
        float timer = 0f;
        while (timer <= duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitSphere * magnitude + _startPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _startPos;
    }
}
