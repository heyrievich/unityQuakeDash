using UnityEngine;

public class DisappearAfterStart : MonoBehaviour
{
    // Время, через которое объекты будут удалены
    public float disappearDelay = 2f;

    // Объекты, которые будут исчезать после начала сцены
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    void Start()
    {
        // Удаляем объекты через указанное время
        Destroy(object1, disappearDelay);
        Destroy(object2, disappearDelay);
        Destroy(object3, disappearDelay);
        Destroy(object4, disappearDelay);
    }
}
