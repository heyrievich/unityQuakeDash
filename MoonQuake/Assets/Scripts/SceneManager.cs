using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private AudioSource destroyBuilding;
    [SerializeField] private AudioSource bgMusic1;
    public Canvas pauseMenuCanvas; // ������ �� ������ ���� �����
    public GameObject ideaPanel; // ������ ��� ����������� ���������
    public TextMeshProUGUI hintsText; // ����� ��� ����������� ���������� ���������
    public int maxHints = 2; // ������������ ���������� ���������
    private int hintsAvailable; // ������� ���������� ��������� ���������
    public GameObject closeButton;
    public GameObject closeButton2;
    public GameObject adMenu;
    public GameObject watchAdButton;// ������ �� ������ ��� �������� ���������
    public Canvas gameOverCanvas; // ������ �� Canvas ��� ����������� ������ Game Over
    private float timeRemaining = 60f; // �����, ���������� �� ����������� ������ Game Over

    void Start()
    {
        // �������� ������������ ���������� ���������
        LoadHints();
        UpdateHintsText(); // ���������� ����������� ���������� ���������

        // ������ ������� ��� ����������� ������ Game Over ����� 30 ������
        Invoke("DisplayGameOverScreen", 45f);
    }

    // ����� ��� ����������� ������� �����
    public void RestartScene()
    {
        // ������������ �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    // ����� ��� �������� �� ����� "Levels"
    public void LoadLevelsScene()
    {
        SceneManager.LoadScene("mainMenu2");
        Time.timeScale = 1f;
    }

    // ����� ��� ����� ����
    public void PauseGame()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f; // ������������� �����
            pauseMenuCanvas.gameObject.SetActive(true); // ���������� ���� �����
        }
    }

    // ����� ��� ����������� ���� ����� �����
    public void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ �����
        pauseMenuCanvas.gameObject.SetActive(false); // �������� ���� �����
    }

    // ����� ��� ������������� ���������
    public void UseHint()
    {
        if (hintsAvailable > 0)
        {
            DecreaseHints(); // ��������� ���������� ���������
            ideaPanel.SetActive(true);
            closeButton.SetActive(true);
            Time.timeScale = 0f; // ������������� �����
        }
        else if (hintsAvailable == 0)
        {
            closeButton2.SetActive(true);
            adMenu.SetActive(true);
            watchAdButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // ����� ��� ���������� ���������� ���������
    public void DecreaseHints()
    {
        if (hintsAvailable > 0)
        {
            hintsAvailable--; // ��������� ���������� ��������� ���������
            UpdateHintsText(); // ��������� ����������� ���������� ���������
            SaveHints(); // ��������� ���������� ���������
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

    // ����� ��� ���������� ����������� ���������� ���������
    private void UpdateHintsText()
    {
        hintsText.text = "Count of hints: " + hintsAvailable.ToString();
    }

    // ����� ��� ���������� ���������� ���������
    private void SaveHints()
    {
        PlayerPrefs.SetInt("HintsAvailable", hintsAvailable);
        PlayerPrefs.Save(); // ��������� ������
    }

    // ����� ��� �������� ���������� ���������
    private void LoadHints()
    {
        if (PlayerPrefs.HasKey("HintsAvailable"))
        {
            hintsAvailable = PlayerPrefs.GetInt("HintsAvailable");
        }
        else
        {
            hintsAvailable = maxHints; // ���� ������ �� �������, ������������� ������������ ���������� ���������
        }
    }

    // ����� ��� ������ ������ � ����������
    public void ShowHintPanel()
    {
        UseHint();
    }

    // ����� ��� ������� ������ � ����������
    public void HideHintPanel()
    {
        closeButton.SetActive(false);
        ideaPanel.SetActive(false);
        Time.timeScale = 1f; // ������������ �����
    }

    // ����� ��� ����������� ������ Game Over
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
