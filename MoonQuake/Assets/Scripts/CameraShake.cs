using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // ������������� ������ ������
    public float shakeIntensity = 0.1f;

    // ������������ ������ ������
    public float shakeDuration = 0.5f;

    // ��������� ������� ������
    private Vector3 originalPosition;

    // �������� ����� ��������
    public float initialShakeInterval = 5f;
    private float currentShakeInterval;
    [SerializeField] private AudioSource shakeSound; // ���� ����

    void Start()
    {
        // ��������� ��������� ������� ������
        originalPosition = transform.localPosition;

        // ������������� ��������� �������� ���������
        currentShakeInterval = initialShakeInterval;

        // ��������� �������� ��� ������ ������ � ����������
        StartCoroutine(ShakeCoroutineWithInterval());
    }

    // �������� ��� ������ ������ � ������������ ����������
    private IEnumerator ShakeCoroutineWithInterval()
    {
        while (true)
        {
            // ������� ������� ��������
            yield return new WaitForSeconds(currentShakeInterval);

            // ��������� �������� ����� ��������� �������
            currentShakeInterval = Mathf.Max(currentShakeInterval - 0.15f, 0.1f);

            // ��������� ������ ������
            Shake();
        }
    }

    // ����� ��� ������� ������ ������
    public void Shake()
    {
        shakeSound.Play();
        // ��������� �������� ��� ������ ������
        StartCoroutine(ShakeCoroutine());
    }

    // �������� ��� ������ ������
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;
        float startIntensity = shakeIntensity;

        while (elapsedTime < shakeDuration)
        {
            // �������� ������������� � �������� �������
            float progress = elapsedTime / shakeDuration;
            shakeIntensity = Mathf.Lerp(startIntensity, 0f, progress);

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
        // ���������� ��������� �������� �������������
        shakeIntensity = startIntensity;
    }
}
