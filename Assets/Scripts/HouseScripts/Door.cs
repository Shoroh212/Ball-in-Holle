using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _player;           // Игрок
    [SerializeField] private Vector3 _newPlayerPosition;   // Новая позиция игрока после "телепорта"

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что столкнулся именно игрок
        if (collision.gameObject == _player)
        {
            _player.transform.position = _newPlayerPosition;
        }
    }
}
