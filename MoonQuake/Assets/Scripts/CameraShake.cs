using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Интенсивность тряски камеры
    public float shakeIntensity = 0.1f;

    // Длительность тряски камеры
    public float shakeDuration = 0.5f;

    // Начальная позиция камеры
    private Vector3 originalPosition;

    // Интервал между трясками
    public float initialShakeInterval = 5f;
    private float currentShakeInterval;
    [SerializeField] private AudioSource shakeSound; // Звук пола

    void Start()
    {
        // Сохраняем начальную позицию камеры
        originalPosition = transform.localPosition;

        // Устанавливаем начальное значение интервала
        currentShakeInterval = initialShakeInterval;

        // Запускаем корутину для тряски камеры с интервалом
        StartCoroutine(ShakeCoroutineWithInterval());
    }

    // Корутина для тряски камеры с изменяющимся интервалом
    private IEnumerator ShakeCoroutineWithInterval()
    {
        while (true)
        {
            // Ожидаем текущий интервал
            yield return new WaitForSeconds(currentShakeInterval);

            // Уменьшаем интервал перед следующей тряской
            currentShakeInterval = Mathf.Max(currentShakeInterval - 0.15f, 0.1f);

            // Запускаем тряску камеры
            Shake();
        }
    }

    // Метод для запуска тряски камеры
    public void Shake()
    {
        shakeSound.Play();
        // Запускаем корутину для тряски камеры
        StartCoroutine(ShakeCoroutine());
    }

    // Корутина для тряски камеры
    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;
        float startIntensity = shakeIntensity;

        while (elapsedTime < shakeDuration)
        {
            // Изменяем интенсивность с течением времени
            float progress = elapsedTime / shakeDuration;
            shakeIntensity = Mathf.Lerp(startIntensity, 0f, progress);

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
        // Возвращаем начальное значение интенсивности
        shakeIntensity = startIntensity;
    }
}
