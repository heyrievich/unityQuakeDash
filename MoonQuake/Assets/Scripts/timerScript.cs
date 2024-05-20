using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // ������ �� ��������� TextMeshProUGUI ��� ����������� �������
    public Canvas gameOverCanvas; // ������ �� Canvas, ������� ����� ���������� �� ��������� �������
    public AudioSource timerSound;// ������ �� ��������� AudioSource ��� ��������������� ����� �������
    private float timeRemaining = 45f; // �����, ���������� �� ��������� �������
    private bool redColorEnabled = false; // ���� ��� ������������ ��������� �������� �����
    private bool playedSound = false;
    [SerializeField] private AudioSource deathSound;
    // ���� ��� ������������ ��������������� �����

    void Update()
    {
        // ��������� ���������� ����� �� ������ �����
        timeRemaining -= Time.deltaTime;

        // ���������, �� ����������� �� �����
        if (timeRemaining <= 0)
        {
            timeRemaining = 0; // ������������� ���������� ����� � ����

            // ��������� Canvas gameOverCanvas
            gameOverCanvas.gameObject.SetActive(true);
            // ������������� ����� � ����
        }

        // ��������� ����������� �������
        DisplayTime(timeRemaining);

        // ������������� ���� ��� ���������� 10 ��������
        if (!playedSound && timeRemaining <= 10)
        {
            timerSound.Play();
            playedSound = true; // ������������� ���� ��������������� �����
        }
    }

    // ����� ��� ����������� ������� � ���������� TextMeshProUGUI
    void DisplayTime(float timeToDisplay)
    {
        // ��������� ������ � �������
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // ����������� ����� � ���� ������
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // ��������� ����� � ���������� TextMeshProUGUI
        timerText.text = timeString;

        // �������� ���� ������ �� �������, ���� �������� ����� 10 ������
        if (timeToDisplay <= 10)
        {
            // �������� ��� ��������� ������� ������� ������
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
            // ���������� ����� ���� ������, ���� �������� ������ 10 ������
            timerText.color = Color.white;
        }
    }
}
