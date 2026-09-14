using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileScript : NetworkBehaviour
{
    public float m_Force;
    private Rigidbody m_Rigidbody;
    public ShootingScript m_OwnerPlayer;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = transform.forward * m_Force;
        if (IsServer)
        {
            GetComponent<NetworkObject>().Spawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsServer)
        {
            ExplodeOnClientsClientRpc();
        }
    }

    [ClientRpc]
    private void ExplodeOnClientsClientRpc()
    {
        m_OwnerPlayer.DestroyMissile(gameObject.name);
    }
}
