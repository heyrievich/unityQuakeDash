using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
    [SerializeField] private AudioSource floorSound; // Звук пола

    void Start()
    {
        // Настройка звука пола
        floorSound = GetComponent<AudioSource>();

        // Запускаем корутину для воспроизведения звука с интервалом
        StartCoroutine(PlaySoundWithInterval());
    }

    // Корутина для воспроизведения звука с интервалом
    private IEnumerator PlaySoundWithInterval()
    {
        while (true)
        {
            // Ожидаем 2 секунды
            yield return new WaitForSeconds(3.5f);

            // Воспроизводим звук пола
            floorSound.Play();
        }
    }
}
