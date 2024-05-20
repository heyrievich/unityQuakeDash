using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBlink : MonoBehaviour
{
    public Image imageToBlink; // Ссылка на изображение, которое нужно моргать

    void Start()
    {
        // Запускаем корутину для моргания изображения
        StartCoroutine(BlinkImage());
    }

    private IEnumerator BlinkImage()
    {
        // Запускаем бесконечный цикл моргания изображения
        while (true)
        {
            // Активируем изображение
            imageToBlink.gameObject.SetActive(true);
            // Ждем 0.5 секунды
            yield return new WaitForSeconds(1f);
            // Деактивируем изображение
            imageToBlink.gameObject.SetActive(false);
            // Ждем еще 0.5 секунды
            yield return new WaitForSeconds(0.5f);
        }
    }
}
