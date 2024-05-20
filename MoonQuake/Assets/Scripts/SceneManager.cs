using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private AudioSource destroyBuilding;
    [SerializeField] private AudioSource bgMusic1;
    public Canvas pauseMenuCanvas; // Ссылка на канвас меню паузы
    public GameObject ideaPanel; // Панель для отображения подсказки
    public TextMeshProUGUI hintsText; // Текст для отображения количества подсказок
    public int maxHints = 2; // Максимальное количество подсказок
    private int hintsAvailable; // Текущее количество доступных подсказок
    public GameObject closeButton;
    public GameObject closeButton2;
    public GameObject adMenu;
    public GameObject watchAdButton;// Ссылка на кнопку для закрытия подсказки
    public Canvas gameOverCanvas; // Ссылка на Canvas для отображения экрана Game Over
    private float timeRemaining = 60f; // Время, оставшееся до отображения экрана Game Over

    void Start()
    {
        // Загрузка сохраненного количества подсказок
        LoadHints();
        UpdateHintsText(); // Обновление отображения количества подсказок

        // Запуск таймера для отображения экрана Game Over через 30 секунд
        Invoke("DisplayGameOverScreen", 45f);
    }

    // Метод для перезапуска текущей сцены
    public void RestartScene()
    {
        // Возобновляем время
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    // Метод для перехода на сцену "Levels"
    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("mainMenu2");
        Time.timeScale = 1f;
    }

    // Метод для паузы игры
    public void PauseGame()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f; // Останавливаем время
            pauseMenuCanvas.gameObject.SetActive(true); // Показываем меню паузы
        }
    }

    // Метод для продолжения игры после паузы
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время
        pauseMenuCanvas.gameObject.SetActive(false); // Скрываем меню паузы
    }

    // Метод для использования подсказки
    public void UseHint()
    {
        if (hintsAvailable > 0)
        {
            DecreaseHints(); // Уменьшаем количество подсказок
            ideaPanel.SetActive(true);
            closeButton.SetActive(true);
            Time.timeScale = 0f; // Останавливаем время
        }
        else if (hintsAvailable == 0)
        {
            closeButton2.SetActive(true);
            adMenu.SetActive(true);
            watchAdButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // Метод для уменьшения количества подсказок
    public void DecreaseHints()
    {
        if (hintsAvailable > 0)
        {
            hintsAvailable--; // Уменьшаем количество доступных подсказок
            UpdateHintsText(); // Обновляем отображение количества подсказок
            SaveHints(); // Сохраняем количество подсказок
        }
    }

    public void CloseAdMenu()
    {
        adMenu.SetActive(false);
        closeButton2.SetActive(false);
        watchAdButton.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddHunt()
    {
        hintsAvailable++;
        UpdateHintsText();
        SaveHints();
    }

    // Метод для обновления отображения количества подсказок
    private void UpdateHintsText()
    {
        hintsText.text = "Count of hints: " + hintsAvailable.ToString();
    }

    // Метод для сохранения количества подсказок
    private void SaveHints()
    {
        PlayerPrefs.SetInt("HintsAvailable", hintsAvailable);
        PlayerPrefs.Save(); // Сохраняем данные
    }

    // Метод для загрузки количества подсказок
    private void LoadHints()
    {
        if (PlayerPrefs.HasKey("HintsAvailable"))
        {
            hintsAvailable = PlayerPrefs.GetInt("HintsAvailable");
        }
        else
        {
            hintsAvailable = maxHints; // Если данные не найдены, устанавливаем максимальное количество подсказок
        }
    }

    // Метод для вызова панели с подсказкой
    public void ShowHintPanel()
    {
        UseHint();
    }

    // Метод для скрытия панели с подсказкой
    public void HideHintPanel()
    {
        closeButton.SetActive(false);
        ideaPanel.SetActive(false);
        Time.timeScale = 1f; // Возобновляем время
    }

    // Метод для отображения экрана Game Over
    private void DisplayGameOverScreen()
    {
        gameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        destroyBuilding.Play();
        bgMusic1.Stop();
    }
    public void level2Open()
    {
        SceneManager.LoadScene("Level2");
        Time.timeScale = 1f;

    }
    public void level3Open()
    {
        SceneManager.LoadScene("Level3");
        Time.timeScale = 1f;

    }
   

}
