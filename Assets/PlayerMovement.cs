using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private float moveSpeed = 5f;

    private Vector2 input;

    void Update()
    {
        if (isLocalPlayer)
        {
            // Получаем ввод от игрока
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // Применяем ввод для движения
            SendInput(new Vector2(horizontal, vertical));
        }
        if (isServer)
        {
            transform.position = transform.position + Vector3.Normalize((Vector3)input) * moveSpeed * Time.deltaTime;
        }
    }

    [Command]
    void SendInput(Vector2 inp)
    {
        input = inp;
    }
}
