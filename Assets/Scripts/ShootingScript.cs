using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShootingScript : NetworkBehaviour
{
    public GameObject m_Missile;
    public Transform m_SpawnPoint;
    public int m_MissilesLaunched;

    public List<GameObject> m_Missiles = new List<GameObject>();

    public Camera m_PlayerCamera;
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        GameObject currentTarget = null;




        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(currentTarget != null) ServerSaysSpawnMissilServerRpc();
        }


    }

    [ServerRpc]
    private void ServerSaysSpawnMissilServerRpc()
    {
        Debug.Log("ServerSaysSpawnMissilServerRpc");
        ShootLocallyClientRpc();
    }

    [ClientRpc]
    private void ShootLocallyClientRpc()
    {
        Debug.Log("Server told me to do this");
        ShootActualMissile();
    }

    private void ShootActualMissile()
    {
        GameObject newMissile = Instantiate(m_Missile, m_SpawnPoint.position, m_SpawnPoint.rotation);
        m_MissilesLaunched++;
        newMissile.name = "Missale_" + m_MissilesLaunched.ToString();
        newMissile.GetComponent<ProjectileScript>().m_OwnerPlayer = this;
        m_Missiles.Add(newMissile);
    }

    public void DestroyMissile(string missileName)
    {
        if (IsServer)
        {
            for (int i = 0; i < m_Missiles.Count; i++)
            {
                if(m_Missiles[i] != null)
                {
                    if (m_Missiles[i].name == missileName)
                    {
                        DestroyMissilesClientRpc(missileName);
                        Destroy(m_Missiles[i]);
                        break;
                    }
                }
            }
        }
    }

    [ServerRpc]
    private void ShootingServerRpc()
    {
        GameObject newMissile = Instantiate(m_Missile, m_SpawnPoint.position, m_SpawnPoint.rotation);
        newMissile.GetComponent<NetworkObject>().Spawn();
    }

    [ClientRpc]
    private void DestroyMissilesClientRpc(string missileName)
    {
        for (int i = 0; i < m_Missiles.Count; i++)
        {
            if (m_Missiles[i] != null)
            {
                if (m_Missiles[i].name == missileName)
                {
                    Destroy(m_Missiles[i]);
                    break;
                }
            }
        }
    }
}
