using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 1f; // Скорость движения объекта
    private float initialPositionY; // Начальная позиция по оси Y
    private float targetPositionY; // Конечная позиция по оси Y
    private bool moveForward = true; // Флаг для определения направления движения

    void Start()
    {
        // Задаем начальную позицию по оси Y
        initialPositionY = transform.position.y;
        // Задаем конечную позицию по оси Y
        targetPositionY = initialPositionY + 0.05f; // Пиксель вперед от начальной позиции
    }

    void Update()
    {
        // Проверяем направление движения и меняем его, если достигли конечной позиции или начальной
        if (transform.position.y >= targetPositionY)
        {
            moveForward = false;
        }
        else if (transform.position.y <= initialPositionY)
        {
            moveForward = true;
        }

        // Двигаем объект вперед или назад в зависимости от направления движения
        if (moveForward)
        {
            transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            transform.Translate(0f, -moveSpeed * Time.deltaTime, 0f);
        }
    }
}
