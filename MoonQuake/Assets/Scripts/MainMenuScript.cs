using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject level1Beach; // ������ �� ���� ������� ������

    public GameObject StartButtom;
    public GameObject SettingButtom;
    public GameObject communitypage;
    public GameObject communityCloseButton;
    public GameObject HuiButtom;

    void Start()
    {
        // ���������� ���� ������� ������ �����, � ���� ������� ������ �����
        level1Beach.SetActive(false);

    }

    // �����, ���������� ��� ������� ������ "Start"
    public void StartGame()
    {
        // ���������� ���� ������� ������, �������� ���� ������� ������
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
