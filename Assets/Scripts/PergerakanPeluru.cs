using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PergerakanPeluru : MonoBehaviour
{
    [Header("Pengaturan Peluru")]
    public float kecepatanPeluru = 10f;
    private float batasAtasY = 8f; // Batas atas layar agar peluru hilang kalau meleset

    void Update()
    {
        // 1. Peluru selalu bergerak ke ATAS (Vector3.up)
        transform.Translate(Vector3.up * kecepatanPeluru * Time.deltaTime);

        // 2. Hancurkan peluru jika keluar dari layar atas
        if (transform.position.y > batasAtasY)
        {
            Destroy(gameObject);
        }
    }

    // 3. Sistem Tabrakan Peluru dengan Musuh
    void OnTriggerEnter2D(Collider2D collision)
    {
        // JIKA peluru menabrak objek ber-Tag "Musuh"
        if (collision.gameObject.CompareTag("Musuh"))
        {
            // Panggil GameManager untuk menambah poin
            GameManager.instance.TambahPoin(50);
            
            // Hancurkan musuh
            Destroy(collision.gameObject);
            
            // Hancurkan peluru ini sendiri
            Destroy(gameObject); 
        }
    }
}