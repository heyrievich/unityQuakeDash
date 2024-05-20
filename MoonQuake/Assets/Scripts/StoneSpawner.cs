using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneSpawner : MonoBehaviour
{
    public GameObject[] stonePrefabs; // Массив префабов камней
    public List<Vector3> stoneSpawnPoints; // Список позиций для появления камней
    public float spawnInterval = 3f; // Интервал появления камней
    public float stoneLifetime = 4f; // Время жизни камня
    public int stonesPerSpawn = 2; // Количество камней, появляющихся за раз
    [SerializeField] private AudioSource floorSound; // Звук пола

    void Start()
    {
        // Запускаем таймер для появления камней
        StartCoroutine(SpawnStonesWithDelay());
    }

    // Корутина для появления камней с паузой перед звуком
    private IEnumerator SpawnStonesWithDelay()
    {
        while (true)
        {
            // Пауза перед воспроизведением звука
            yield return new WaitForSeconds(0.15f);

            // Воспроизводим звук пола
            floorSound.Play();

            // Пауза перед спауном камней
            yield return new WaitForSeconds(1f);

            // Создаем указанное количество камней за один спаун
            for (int i = 0; i < stonesPerSpawn; i++)
            {
                // Выбираем случайную позицию из списка stoneSpawnPoints
                Vector3 randomSpawnPoint = stoneSpawnPoints[Random.Range(0, stoneSpawnPoints.Count)];
                // Получаем случайную ориентацию
                Quaternion randomRotation = Random.rotation;
                // Выбираем случайный префаб из массива stonePrefabs
                GameObject randomPrefab = stonePrefabs[Random.Range(0, stonePrefabs.Length)];
                // Появление камня на выбранной позиции с случайным поворотом и случайным префабом
                GameObject newStone = Instantiate(randomPrefab, randomSpawnPoint, randomRotation);
                // Уничтожение камня через stoneLifetime секунд
                Destroy(newStone, stoneLifetime);
            }

            // Ждем до следующего спауна камней
            yield return new WaitForSeconds(spawnInterval - 2f);
        }
    }
}
