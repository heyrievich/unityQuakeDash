using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Level2CharacterController : MonoBehaviour
{

    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource deathMusic;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource takeSound;
    [SerializeField] private AudioSource winMusic;
    public GameObject victoryPanel;
    public bool destroyShkafIsPossible2 = false;
    public GameObject triggerShkafObject;
    public GameObject triggerShkafObject2;
    public GameObject triggerBlameObject;
    public Camera mainCamera1;
    public Camera mainCamera2;
    public GameObject shkaf2;
    public GameObject shkaf;
    public GameObject blame;
    public Button destroyButton;
    public float speedMultiplier = 5f;
    public Joystick joystick;
    public Image fireSprite;
    public Image toporSprite;
    public GameObject firePrefab;
    public GameObject destroyShkafText;
    public GameObject destroyBlameText;
    public GameObject toporPrefab;
    public Button useButton;
    public GameObject takeFireText;
    public GameObject takeToporText;
    public GameObject inventoryIsFullText;
    public Button throwButton;
    public bool toporIsTaken = false;
    public bool fireIsTaken = false;
    public bool inventoryIsFull = false;
    public GameObject head;
    public GameObject gameOverPanel;
    public bool destroyShkafIsPossible = false;
    public bool destroyBlameIsPossible = false;
    private GameObject currentObject = null;
    public Button destroyButton2;


    private Animator animator;
    private int state = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StopMusicAfterDelay());
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * direction;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        transform.position += moveDirection * speedMultiplier * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            state = 1;
            if (!walkSound.isPlaying)
                walkSound.Play();
        }
        else
        {
            state = 0;
            walkSound.Stop();
        }

        animator.SetInteger("State", state);
    }

    private IEnumerator StopMusicAfterDelay()
    {
        yield return new WaitForSeconds(45f);
        bgMusic.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fire"))
        {
            if (!inventoryIsFull)
            {
                takeFireText.SetActive(true);
                useButton.gameObject.SetActive(true);
                currentObject = firePrefab;
            }
            else
            {
                inventoryIsFullText.SetActive(true);
                useButton.gameObject.SetActive(false);
            }
        }
        else if (other.CompareTag("Topor"))
        {
            if (!inventoryIsFull)
            {
                takeToporText.SetActive(true);
                useButton.gameObject.SetActive(true);
                currentObject = toporPrefab;
            }
            else
            {
                inventoryIsFullText.SetActive(true);
                useButton.gameObject.SetActive(false);
            }
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
        else if (other.CompareTag("changeCamera"))
        {
            mainCamera1.enabled = !mainCamera1.enabled;
            mainCamera2.enabled = !mainCamera2.enabled;


        }
        else if (other.CompareTag("destroyShkaf"))
        {
            if(toporIsTaken)
            {
                destroyShkafText.SetActive(true);
                destroyShkafIsPossible = true;
                destroyButton.gameObject.SetActive(true);
                
            }
            else
            {
                destroyShkafText.SetActive(true);
               
            }
            
        }
        else if (other.CompareTag("destroyBlame"))
        {
            if (fireIsTaken)
            {
                destroyBlameText.SetActive(true);
                destroyBlameIsPossible = true;
                destroyButton.gameObject.SetActive(true);

            }
            else
            {
                destroyBlameText.SetActive(true);

            }

        }
        else if (other.CompareTag("destroyShkaf2"))
        {
            if (toporIsTaken)
            {
                destroyShkafText.SetActive(true);
                destroyShkafIsPossible2 = true;
                destroyButton2.gameObject.SetActive(true);

            }
            else
            {
                destroyShkafText.SetActive(true);

            }

        }



    }

   private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Fire"))
    {
        takeFireText.SetActive(false);
        useButton.gameObject.SetActive(false);
        currentObject = null;
        inventoryIsFullText.SetActive(false);
    }
    else if (other.CompareTag("Topor"))
    {
        takeToporText.SetActive(false);
        useButton.gameObject.SetActive(false);
        currentObject = null;
        inventoryIsFullText.SetActive(false);
    }
    else if (other.CompareTag("destroyShkaf"))
    {
        destroyShkafText.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        currentObject = null;
        destroyShkafIsPossible = false;
    }
    else if (other.CompareTag("destroyBlame"))
    {
        destroyBlameText.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        currentObject = null;
        destroyBlameIsPossible = false;
    }
    else if (other.CompareTag("destroyShkaf2"))
    {
        destroyShkafText.SetActive(false);
        destroyButton2.gameObject.SetActive(false);
        currentObject = null;
        destroyShkafIsPossible2 = false;
    }
    else if (other.CompareTag("win2Level"))
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        winMusic.Play();
        bgMusic.Stop();
    }
}


    public void UseObject()
    {
        if (currentObject != null && !inventoryIsFull)
        {
            if (currentObject == firePrefab)
            {
                takeFireText.SetActive(false);
                firePrefab.SetActive(false);
                fireSprite.gameObject.SetActive(true);
                fireIsTaken = true;
                toporIsTaken = false;
                inventoryIsFull = true;
            }
            else if (currentObject == toporPrefab)
            {
                takeToporText.SetActive(false);
                toporPrefab.SetActive(false);
                toporSprite.gameObject.SetActive(true);
                fireIsTaken = false;
                toporIsTaken = true;
                inventoryIsFull = true;
                
            }
           
            useButton.gameObject.SetActive(false);

        }
    }
    public void DestroyObject()
    {
        if (destroyShkafIsPossible && toporIsTaken)
        {
            destroyShkafText.SetActive(false);
            Destroy(shkaf);
            Destroy(triggerShkafObject);
            destroyButton.gameObject.SetActive(false);
        }
        else if (destroyBlameIsPossible && fireIsTaken)
        {
            destroyBlameText.SetActive(false);
            Destroy(blame);
            Destroy(triggerBlameObject);
            destroyButton.gameObject.SetActive(false);
        }
       
    }
    public void DestroyObject2()
    {
        if (destroyShkafIsPossible2 && toporIsTaken)
        {
            destroyShkafText.SetActive(false);
            shkaf2.SetActive(false);
            triggerShkafObject2.SetActive(false);
            destroyButton.gameObject.SetActive(false);
            Debug.Log("Hello, Unity!");
        }
    }


    public void ThrowObject()
    {
        if (inventoryIsFull)
        {
            inventoryIsFull = false;
            inventoryIsFullText.SetActive(false);
            if (fireIsTaken)
            {
                fireIsTaken = false;
                Vector3 playerPos = transform.position;
                firePrefab.transform.position = new Vector3(playerPos.x, firePrefab.transform.position.y, playerPos.z);
                firePrefab.SetActive(true);
                fireSprite.gameObject.SetActive(false);
            }
            else if (toporIsTaken)
            {
                toporIsTaken = false;
                Vector3 playerPos = transform.position;
                toporPrefab.transform.position = new Vector3(playerPos.x, toporPrefab.transform.position.y, playerPos.z);
                toporPrefab.SetActive(true);
                toporSprite.gameObject.SetActive(false);
            }
        }
    }

}