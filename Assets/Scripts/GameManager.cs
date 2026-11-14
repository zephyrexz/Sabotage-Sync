using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Wajib ditambahkan agar kita bisa mengatur UI Teks
using UnityEngine.SceneManagement; // Wajib agar bisa berpindah Scene

public class GameManager : MonoBehaviour
{
    // Ini namanya Singleton. Fungsinya agar script ini bisa dipanggil
    // dari mana saja tanpa perlu drag-and-drop.
    public static GameManager instance; 

    [Header("Data Game")]
    public int poin = 0;
    public int nyawa = 3;

    [Header("UI Teks")]
    public Text teksPoin;
    public Text teksNyawa;
    public GameObject panelGameOver;

    [Header("Pengaturan Scene")]
    public string namaSceneGameplay;
    public string namaSceneMenu;

    void Awake()
    {
        // Menyeting Singleton saat game baru mulai
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1; //Agar waktu game normal dan dapat bergerak
        UpdateUI(); // Tampilkan UI pertama kali
    }

    // Fungsi untuk menambah skor
    public void TambahPoin(int nilai)
    {
        poin += nilai;
        UpdateUI();
    }

    // Fungsi untuk mengurangi nyawa
    public void KurangiNyawa(int nilai)
    {
        nyawa -= nilai;
        UpdateUI();

        // Mengecek apakah nyawa habis
        if (nyawa <= 0)
        {
            Debug.Log("GAME OVER!");
            Time.timeScale = 0;
            panelGameOver.SetActive(true);
        }
    }

    // Fungsi untuk menyegarkan tampilan teks di layar
    void UpdateUI()
    {
        teksPoin.text = "Poin: " + poin;
        teksNyawa.text = "Nyawa: " + nyawa;
    }

    public void TombolRestart()
    {
        // Me-load ulang scene gameplay
        SceneManager.LoadScene(namaSceneGameplay);
    }

    public void TombolMainMenu()
    {
        // Pindah ke scene menu utama
        SceneManager.LoadScene(namaSceneMenu);
    }

}