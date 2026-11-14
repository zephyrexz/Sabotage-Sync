using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Pengaturan Player")]
    // Variabel publik agar Designer kelompok bisa mengatur kecepatan langsung di Inspector
    public float kecepatan = 10f; 
    
    // Batas layar agar player tidak kebablasan keluar dari kamera
    public float batasSumbuX = 2.3f; 
    
    [Header("Komponen Animasi (Otomatis)")]
    private Animator anim;

    void Start()
    {
        // Mengambil komponen Animator yang nempel di GameObject Player secara otomatis
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. MENGAMBIL INPUT KEYBOARD
        float inputHorizontal = Input.GetAxisRaw("Horizontal"); 

        // 2. MENGERAKKAN PLAYER
        // Menggeser posisi player berdasarkan input. Vector3.right mewakili sumbu X.
        transform.Translate(Vector3.right * inputHorizontal * kecepatan * Time.deltaTime);

        // 3. MEMBATASI PERGERAKAN PLAYER AGAR TIDAK KELUAR LAYAR
        if (transform.position.x > batasSumbuX)
        {
            // Mentok kanan
            transform.position = new Vector3(batasSumbuX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -batasSumbuX)
        {
            // Mentok kiri
            transform.position = new Vector3(-batasSumbuX, transform.position.y, transform.position.z);
        }

        // 4. MENGATUR LOGIKA ANIMASI
        // Jika ada input (inputHorizontal tidak sama dengan 0)
        if (inputHorizontal != 0)
        {
            // Nyalakan animasi Move
            anim.SetBool("isMoving", true);
        }
        // Jika tidak ada input (inputHorizontal bernilai 0)
        else
        {
            // Kembali ke animasi Idle
            anim.SetBool("isMoving", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // JIKA yang ditabrak memiliki Tag "Poin" (Buah)
        if (collision.gameObject.CompareTag("Poin"))
        {
            // Panggil GameManager untuk tambah 10 poin
            GameManager.instance.TambahPoin(10);
            
            // Hancurkan buahnya (agar seolah-olah dimakan)
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Musuh"))
        {
            // Panggil GameManager untuk kurangi 1 nyawa
            GameManager.instance.KurangiNyawa(1);
            
            // Hancurkan bomnya
            Destroy(collision.gameObject);
        }
        // JIKA yang ditabrak memiliki Tag "Bom" (Bahaya)
        else if (collision.gameObject.CompareTag("Bom"))
        {
            // Panggil GameManager untuk kurangi 1 nyawa
            GameManager.instance.KurangiNyawa(1);
            
            // Hancurkan bomnya
            Destroy(collision.gameObject);
        }
    }
}
