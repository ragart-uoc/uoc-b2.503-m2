using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    public bool destroyOnDeath;

    private const int MaxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = MaxHealth;

    public RectTransform healthBar;

    private NetworkStartPosition[] m_SpawnPoints;

    private void Start()
    {
        if (isLocalPlayer)
        {
            m_SpawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)  {
            return;
        }

        currentHealth -= amount;
        if (currentHealth > 0) return;
        if (destroyOnDeath) {
            Destroy(gameObject);
        }
        else
        {
            currentHealth = MaxHealth;
            RpcRespawn();
        }
    }

    public void OnChangeHealth (int oldHealth, int newHealth) {
        healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        if (!isLocalPlayer) return;
        var spawnPoint = Vector3.zero;

        if (m_SpawnPoints is { Length: > 0 }) {
            spawnPoint = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].transform.position;
        }

        transform.position = spawnPoint;
    }
}
