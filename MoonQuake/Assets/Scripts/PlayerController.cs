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
    public float speedMultiplier = 5f; // Множитель скорости перемещения
    public Joystick joystick; // Джойстик для управления
    public GameObject pickupText;
    public GameObject pickupText2;
    public GameObject pickupText3;// Ссылка на текст для отображения надписи
    public string objectTag = "TakeGlue"; // Тег объекта, который должен показывать надпись
    public string exitTag = "Exit";
    public string wallTag = "GlueWall"; // Тег объекта, который должен показывать вторую надпись
    public GameObject gluePrefab; // Префаб клея для добавления в инвентарь
    public GameObject useButton;
    public GameObject glue;
    public GameObject triggerObject;
    public GameObject arrow;
    private bool isInGlueWallTrigger = false;
    public GameObject TagItem;// Ссылка на кнопку "Use"
    public GameObject head; // Ссылка на голову персонажа
    public GameObject gameOverPanel; // Ссылка на панель GameOver
    public GameObject victoryPanel; // Ссылка на панель GameOver

    private Animator animator;
    private int state = 0; // 0 - Idle, 1 - Walk

    void Start()
    {
        animator = GetComponent<Animator>();
        // Изначально отключаем текст и кнопку "Use"
        pickupText.SetActive(false);
        useButton.SetActive(false);
        glue.SetActive(false);
        pickupText2.SetActive(false);
        arrow.SetActive(false);
        triggerObject.SetActive(false);
        pickupText3.SetActive(false);
        // Скрываем панель GameOver
        gameOverPanel.SetActive(false);
        StartCoroutine(StopMusicAfterDelay());
    }

    void Update()
    {
        // Получаем значения осей джойстика
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Получаем направление движения относительно камеры
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * direction;

        // Поворачиваем персонажа в соответствии с направлением движения
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Перемещаем персонажа
        transform.position += moveDirection * speedMultiplier * Time.deltaTime;

        // Определяем состояние анимации
        if (direction.magnitude >= 0.1f)
        {
            // Если персонаж движется, устанавливаем состояние "Walk"
            state = 1;
            // Воспроизводим звук ходьбы
            if (!walkSound.isPlaying)
                walkSound.Play();
        }
        else
        {
            // Если персонаж стоит на месте, устанавливаем состояние "Idle"
            state = 0;
            // Останавливаем воспроизведение звука ходьбы
            walkSound.Stop();
        }

        // Обновляем переменную "State" в аниматоре
        animator.SetInteger("State", state);
    }
    private IEnumerator StopMusicAfterDelay()
    {
        // Ждем 30 секунд
        yield return new WaitForSeconds(45f);

        // Останавливаем музыку
        bgMusic.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, касается ли триггер объекта с тэгом "TakeGlue"
        if (other.CompareTag(objectTag))
        {
            // Показываем текст "Подобрать клей"
            pickupText.SetActive(true);
            // Показываем кнопку "Use"
            useButton.SetActive(true);
        }
        else if (other.CompareTag(exitTag))
        {
            // Показываем текст "Выход"
            pickupText2.SetActive(true);
        }
        else if (other.CompareTag(wallTag))
        {
            isInGlueWallTrigger = true;
            // Показываем текст "Выход"
            pickupText3.SetActive(true);
            useButton.SetActive(true);
        }
        else if (other.CompareTag("Stone"))
        {
            // Останавливаем движение персонажа
            speedMultiplier = 0f;
            deathMusic.Play();
            bgMusic.Stop();
            // Показываем панель GameOver
            gameOverPanel.SetActive(true);
            deathSound.Play();
            Time.timeScale = 0f;

        }
    }

    void OnTriggerExit(Collider other)
    {
        // Проверяем, покидает ли объект триггер с тэгом "TakeGlue"
        if (other.CompareTag(objectTag))
        {
            // Скрываем текст "Подобрать клей"
            pickupText.SetActive(false);
            useButton.SetActive(false);
        }
        else if (other.CompareTag(exitTag))
        {
            // Скрываем текст "Выход"
            pickupText2.SetActive(false);
        }
        else if (other.CompareTag(wallTag))
        {
            // Скрываем текст "Выход"
            pickupText3.SetActive(false);
            isInGlueWallTrigger = false;
            useButton.SetActive(false);
        }
    }

    // Метод для добавления клея в инвентарь при нажатии на кнопку "Use"
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
            // Скрываем кнопку "Use"
            useButton.SetActive(false);
            arrow.SetActive(true);
            triggerObject.SetActive(true);
        }
    }
}