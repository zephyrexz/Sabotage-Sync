using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Senjata")]
    public GameObject prefabPeluru; // Slot untuk memasukkan Prefab Peluru

    void Update()
    {
        // Jika pemain menekan tombol Spasi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Nembak();
        }
    }

    void Nembak()
    {
        // Menentukan posisi munculnya peluru (sedikit di atas player agar tidak menumpuk)
        Vector3 posisiTembak = transform.position + new Vector3(0, 0.8f, 0);

        // Memunculkan peluru ke layar
        Instantiate(prefabPeluru, posisiTembak, Quaternion.identity);
    }
}