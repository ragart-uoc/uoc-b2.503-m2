using UnityEngine;
using Mirror;

public class TankController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    private void Update()
    {
        if (!isLocalPlayer) {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    public override void OnStartLocalPlayer()
    {
        foreach (MeshRenderer child in GetComponentsInChildren<MeshRenderer>())
        {
            child.material.color = Color.blue;
        }
    }

    [Command]
    private void CmdFire() {
        
        var bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);
 
        NetworkServer.Spawn(bullet);
 
        Destroy (bullet, 2.0f);
    }
}
