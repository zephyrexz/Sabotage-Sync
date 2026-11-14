using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Pengaturan Spawner")]
    // Sekarang kita pakai Array [] agar bisa memasukkan banyak jenis buah/musuh
    public GameObject[] kumpulanPrefab; 
    
    public float waktuSpawn = 1.5f; 
    private float timer;

    [Header("Batas Layar (720x1280)")]
    public float batasKiriX = -2.5f; 
    public float batasKananX = 2.5f; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waktuSpawn)
        {
            MunculkanObject();
            timer = 0;
        }
    }

    void MunculkanObject()
    {
        // 1. Mencari posisi X acak
        float posisiXAcak = Random.Range(batasKiriX, batasKananX);
        Vector3 posisiSpawn = new Vector3(posisiXAcak, transform.position.y, 0);

        // 2. MEMILIH OBJECT SECARA ACAK DARI DAFTAR (ARRAY)
        // Kita ambil angka acak dari index 0 sampai jumlah isi array
        int indexAcak = Random.Range(0, kumpulanPrefab.Length);

        // 3. Memunculkan object yang terpilih secara acak
        Instantiate(kumpulanPrefab[indexAcak], posisiSpawn, Quaternion.identity);
    }
}