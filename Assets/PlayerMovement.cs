using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float projectileMoveSpeed = 10f;
    [SerializeField] private float projectileCooldown = 10f;
    private GameObject projectilePrefab;
    private Transform projectileSpawnPoint;

    private Vector2 input;
    private bool mouse;
    private float lastFireTime = -100f;

    void Update()
    {
        if (isLocalPlayer)
        {
            LocalPlayerUpdate();
        }
        if (isServer)
        {
            ServerUpdate();
        }
    }

    void LocalPlayerUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        SendMoveInput(new Vector2(horizontal, vertical));

        SendFireInput(Input.GetButtonDown("Fire1"));
    }

    void ServerUpdate()
    {
        transform.position = transform.position + Vector3.Normalize((Vector3)input) * moveSpeed * Time.deltaTime;

        if(mouse && lastFireTime + projectileCooldown < Time.time)
        {
            lastFireTime = Time.time;
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawnPoint.forward * projectileMoveSpeed;
            NetworkServer.Spawn(projectile);
            Destroy(projectile, 2f);
        }
    }

    [Command]
    void SendMoveInput(Vector2 inp)
    {
        input = inp;
    }

    [Command]
    void SendFireInput(bool inp)
    {
        mouse = inp;
    }
}
