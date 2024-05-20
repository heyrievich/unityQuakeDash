using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Ссылка на компонент TextMeshProUGUI для отображения таймера
    public Canvas gameOverCanvas; // Ссылка на Canvas, который нужно отобразить по истечении времени
    public AudioSource timerSound;// Ссылка на компонент AudioSource для воспроизведения звука таймера
    private float timeRemaining = 45f; // Время, оставшееся до окончания таймера
    private bool redColorEnabled = false; // Флаг для отслеживания активации красного цвета
    private bool playedSound = false;
    [SerializeField] private AudioSource deathSound;
    // Флаг для отслеживания воспроизведения звука

    void Update()
    {
        // Уменьшаем оставшееся время на каждом кадре
        timeRemaining -= Time.deltaTime;

        // Проверяем, не закончилось ли время
        if (timeRemaining <= 0)
        {
            timeRemaining = 0; // Устанавливаем оставшееся время в ноль

            // Открываем Canvas gameOverCanvas
            gameOverCanvas.gameObject.SetActive(true);
            // Останавливаем время в игре
        }

        // Обновляем отображение таймера
        DisplayTime(timeRemaining);

        // Воспроизводим звук при оставшихся 10 секундах
        if (!playedSound && timeRemaining <= 10)
        {
            timerSound.Play();
            playedSound = true; // Устанавливаем флаг воспроизведения звука
        }
    }

    // Метод для отображения времени в компоненте TextMeshProUGUI
    void DisplayTime(float timeToDisplay)
    {
        // Вычисляем минуты и секунды
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Форматируем время в виде строки
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Обновляем текст в компоненте TextMeshProUGUI
        timerText.text = timeString;

        // Изменяем цвет текста на красный, если остается менее 10 секунд
        if (timeToDisplay <= 10)
        {
            // Включаем или выключаем мигание красным цветом
            if (Time.time % 1 < 0.5f)
            {
                timerText.color = Color.red;
            }
            else
            {
                timerText.color = Color.white;
            }
        }
        else
        {
            // Возвращаем белый цвет текста, если остается больше 10 секунд
            timerText.color = Color.white;
        }
    }
}
