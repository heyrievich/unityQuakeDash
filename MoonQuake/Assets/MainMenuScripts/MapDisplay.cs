using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockImage;

    public void DisplayMap(Map map)
    {

        mapImage.sprite = map.mapImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= map.mapIndex;

        lockImage.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(map.sceneToLoad.name));
      
       
    }
    
}
