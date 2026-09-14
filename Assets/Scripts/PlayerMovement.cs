using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;    // Movement speed
    public float rotateSpeed = 100f;  // Rotation speed

    private Animator m_Animator;

    public Color[] m_Colors;

    public MeshRenderer m_MeshRenderer;

    private int m_CustomClientID;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        SpawnInRandomPos();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        m_MeshRenderer.material.color = m_Colors[OwnerClientId];
    }
    private void SpawnInRandomPos()
    {
        transform.position = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
    }
    private void Start()
    {
        if (IsOwner)
        {
            GetComponentInChildren<Camera>().enabled = true;
            GameObject.Find("Main Camera").SetActive(false);
        }
    }
    private void Update()
    {
        if (IsOwner)
        {
            // Get horizontal and vertical input
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            transform.Translate(moveVertical * moveSpeed * transform.forward * Time.deltaTime, Space.World);
            transform.Rotate(moveHorizontal * rotateSpeed * Vector3.up * Time.deltaTime, Space.World);

            m_Animator.SetBool("Walking", (moveVertical != 0 ? true : false));
        }
    }
}
