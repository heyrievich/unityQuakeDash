using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject level1Beach; // Ссылка на пляж первого уровня

    public GameObject StartButtom;
    public GameObject SettingButtom;
    public GameObject communitypage;
    public GameObject communityCloseButton;
    public GameObject HuiButtom;

    void Start()
    {
        // Изначально пляж первого уровня видим, а пляж второго уровня скрыт
        level1Beach.SetActive(false);

    }

    // Метод, вызываемый при нажатии кнопки "Start"
    public void StartGame()
    {
        // Показываем пляж первого уровня, скрываем пляж второго уровня
        level1Beach.SetActive(true);
        StartButtom.SetActive(false);
        SettingButtom.SetActive(false);
        HuiButtom.SetActive(false);
    }

    public void communityOpen()
    {
        communitypage.SetActive(true);
        communityCloseButton.SetActive(true);

    }

    public void communityClose()
    {
        communitypage.SetActive(false);
        communityCloseButton.gameObject.SetActive(false);

    }

}
