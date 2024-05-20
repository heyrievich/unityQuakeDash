using UnityEngine;

public class MoveAndDisappear : MonoBehaviour
{
    public Transform targetObject;
    public float moveSpeed = 5f;
    private bool moving = true;
    private float disappearTime = 520f; // Изменено на 8 секунд

    void Start()
    {
        Invoke("Disappear", disappearTime);
    }

    void Update()
    {
        if (moving)
        {
            // Получаем направление к целевому объекту
            Vector3 direction = (targetObject.position - transform.position).normalized;

            // Поворачиваем персонажа в направлении движения
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Двигаем персонажа к целевому объекту
            transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);

            // Если персонаж достиг цели, останавливаем его
            if (transform.position == targetObject.position)
            {
                moving = false;
            }
        }
    }

    void Disappear()
    {
        // Исчезаем через заданное время
        Destroy(gameObject);
    }
}
