using UnityEngine;
using System.Collections;

public class CameraShake1 : MonoBehaviour
{
    // ������������� ������ ������
    public float shakeIntensity = 0.1f;

    // ������������ ������ ������
    public float shakeDuration = 0.5f;

    // ��������� ������� ������
    private Vector3 originalPosition;

    void Start()
    {
        // ��������� ��������� ������� ������
        originalPosition = transform.localPosition;

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
