using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource deathMusic;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource takeSound;
    [SerializeField] private AudioSource winMusic;
    public float speedMultiplier = 5f; // ��������� �������� �����������
    public Joystick joystick; // �������� ��� ����������
    public GameObject pickupText;
    public GameObject pickupText2;
    public GameObject pickupText3;// ������ �� ����� ��� ����������� �������
    public string objectTag = "TakeGlue"; // ��� �������, ������� ������ ���������� �������
    public string exitTag = "Exit";
    public string wallTag = "GlueWall"; // ��� �������, ������� ������ ���������� ������ �������
    public GameObject gluePrefab; // ������ ���� ��� ���������� � ���������
    public GameObject useButton;
    public GameObject glue;
    public GameObject triggerObject;
    public GameObject arrow;
    private bool isInGlueWallTrigger = false;
    public GameObject TagItem;// ������ �� ������ "Use"
    public GameObject head; // ������ �� ������ ���������
    public GameObject gameOverPanel; // ������ �� ������ GameOver
    public GameObject victoryPanel; // ������ �� ������ GameOver

    private Animator animator;
    private int state = 0; // 0 - Idle, 1 - Walk

    void Start()
    {
        animator = GetComponent<Animator>();
        // ���������� ��������� ����� � ������ "Use"
        pickupText.SetActive(false);
        useButton.SetActive(false);
        glue.SetActive(false);
        pickupText2.SetActive(false);
        arrow.SetActive(false);
        triggerObject.SetActive(false);
        pickupText3.SetActive(false);
        // �������� ������ GameOver
        gameOverPanel.SetActive(false);
        StartCoroutine(StopMusicAfterDelay());
    }

    void Update()
    {
        // �������� �������� ���� ���������
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // �������� ����������� �������� ������������ ������
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * direction;

        // ������������ ��������� � ������������ � ������������ ��������
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // ���������� ���������
        transform.position += moveDirection * speedMultiplier * Time.deltaTime;

        // ���������� ��������� ��������
        if (direction.magnitude >= 0.1f)
        {
            // ���� �������� ��������, ������������� ��������� "Walk"
            state = 1;
            // ������������� ���� ������
            if (!walkSound.isPlaying)
                walkSound.Play();
        }
        else
        {
            // ���� �������� ����� �� �����, ������������� ��������� "Idle"
            state = 0;
            // ������������� ��������������� ����� ������
            walkSound.Stop();
        }

        // ��������� ���������� "State" � ���������
        animator.SetInteger("State", state);
    }
    private IEnumerator StopMusicAfterDelay()
    {
        // ���� 30 ������
        yield return new WaitForSeconds(45f);

        // ������������� ������
        bgMusic.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������� ������� � ����� "TakeGlue"
        if (other.CompareTag(objectTag))
        {
            // ���������� ����� "��������� ����"
            pickupText.SetActive(true);
            // ���������� ������ "Use"
            useButton.SetActive(true);
        }
        else if (other.CompareTag(exitTag))
        {
            // ���������� ����� "�����"
            pickupText2.SetActive(true);
        }
        else if (other.CompareTag(wallTag))
        {
            isInGlueWallTrigger = true;
            // ���������� ����� "�����"
            pickupText3.SetActive(true);
            useButton.SetActive(true);
        }
        else if (other.CompareTag("Stone"))
        {
            // ������������� �������� ���������
            speedMultiplier = 0f;
            deathMusic.Play();
            bgMusic.Stop();
            // ���������� ������ GameOver
            gameOverPanel.SetActive(true);
            deathSound.Play();
            Time.timeScale = 0f;

        }
    }

    void OnTriggerExit(Collider other)
    {
        // ���������, �������� �� ������ ������� � ����� "TakeGlue"
        if (other.CompareTag(objectTag))
        {
            // �������� ����� "��������� ����"
            pickupText.SetActive(false);
            useButton.SetActive(false);
        }
        else if (other.CompareTag(exitTag))
        {
            // �������� ����� "�����"
            pickupText2.SetActive(false);
        }
        else if (other.CompareTag(wallTag))
        {
            // �������� ����� "�����"
            pickupText3.SetActive(false);
            isInGlueWallTrigger = false;
            useButton.SetActive(false);
        }
    }

    // ����� ��� ���������� ���� � ��������� ��� ������� �� ������ "Use"
    public void AddGlueToInventory()
    {
        if (isInGlueWallTrigger)
        {
            victoryPanel.SetActive(true);
            PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
            winMusic.Play();
            bgMusic.Stop();
            Time.timeScale = 0f;


        }
        else
        {
            takeSound.Play();
            glue.SetActive(true);
            Destroy(gluePrefab);
            Destroy(TagItem);
            pickupText.SetActive(false);
            // �������� ������ "Use"
            useButton.SetActive(false);
            arrow.SetActive(true);
            triggerObject.SetActive(true);
        }
    }
}