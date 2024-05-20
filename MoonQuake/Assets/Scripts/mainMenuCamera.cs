using UnityEngine;
using System.Collections;

public class MainMenuCameraShake : MonoBehaviour
{
    // ������������� ������ ������
    public float shakeIntensity = 0.1f;

    // ������������ ������ ������
    public float shakeDuration = 0.5f;

    // ��������� ������� ������
    private Vector3 originalPosition;

    // �������� ����� ��������
    public float shakeInterval = 5f;
    private float nextShakeTime;

    void Start()
    {
        // ��������� ��������� ������� ������
        originalPosition = transform.localPosition;

        // ������������� ����� ��������� ������
        nextShakeTime = Time.time + shakeInterval;
    }

    void Update()
    {
        // ���������, ������ �� ����� ��� ��������� ������
        if (Time.time >= nextShakeTime)
        {
            // ��������� ������ ������
            Shake();

            // ������������� ����� ��������� ������ ����� 5 ������
            nextShakeTime += shakeInterval;
        }
    }

    // ����� ��� ������� ������ ������
    public void Shake()
    {
        // ��������� �������� ��� ������ ������
        StartCoroutine(ShakeCoroutine());
    }

    // �������� ��� ������ ������
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // ���������� ��������� �������� ��� ������ ������
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            randomOffset.z = 0f; // �� �� ����� ������ ������ �� ��� Z

            // ��������� �������� � ������� ������
            transform.localPosition = originalPosition + randomOffset;

            // ����������� ������� �������
            elapsedTime += Time.deltaTime;

            // ���� �� ���������� �����
            yield return null;
        }

        // ���������� ������ � �������� ������� ����� ��������� ������
        transform.localPosition = originalPosition;
    }
}
