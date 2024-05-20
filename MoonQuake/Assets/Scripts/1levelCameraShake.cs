using UnityEngine;
using System.Collections;

public class CameraShake1 : MonoBehaviour
{
    // Интенсивность тряски камеры
    public float shakeIntensity = 0.1f;

    // Длительность тряски камеры
    public float shakeDuration = 0.5f;

    // Начальная позиция камеры
    private Vector3 originalPosition;

    void Start()
    {
        // Сохраняем начальную позицию камеры
        originalPosition = transform.localPosition;

        // Запускаем корутину для тряски камеры
        StartCoroutine(ShakeCoroutine());
    }

    // Корутина для тряски камеры
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Генерируем случайное смещение для тряски камеры
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            randomOffset.z = 0f; // Мы не хотим трясти камеру по оси Z

            // Применяем смещение к позиции камеры
            transform.localPosition = originalPosition + randomOffset;

            // Увеличиваем счетчик времени
            elapsedTime += Time.deltaTime;

            // Ждем до следующего кадра
            yield return null;
        }

        // Возвращаем камеру к исходной позиции после окончания тряски
        transform.localPosition = originalPosition;
    }
}
