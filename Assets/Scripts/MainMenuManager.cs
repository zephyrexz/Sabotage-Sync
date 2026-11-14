using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Wajib untuk pindah scene

public class MainMenuManager : MonoBehaviour
{
    [Header("Pengaturan Panel")]
    public GameObject panelUtama;
    public GameObject panelCaraMain;
    public GameObject panelCredit;

    [Header("Pengaturan Scene")]
    public string namaSceneGameplay = "Gameplay"; // Nama scene game kalian

    void Start()
    {
        // Saat game baru dibuka, pastikan hanya Panel Utama yang menyala
        KembaliKeMenu();
    }

    // --- FUNGSI PINDAH SCENE ---
    public void TombolPlay()
    {
        // Memastikan waktu berjalan normal, lalu pindah scene
        Time.timeScale = 1; 
        SceneManager.LoadScene(namaSceneGameplay);
    }

    // --- FUNGSI BUKA TUTUP PANEL ---
    public void BukaCaraMain()
    {
        panelCaraMain.SetActive(true); // Nyalakan cara main
        panelCredit.SetActive(false);  // Pastikan credit mati
    }

    public void BukaCredit()
    {
        panelCaraMain.SetActive(false);
        panelCredit.SetActive(true);   // Nyalakan credit
    }

    public void KembaliKeMenu()
    {
        panelUtama.SetActive(true);    // Nyalakan menu utama
        panelCaraMain.SetActive(false); // Matikan yang lain
        panelCredit.SetActive(false);
    }

    // --- FUNGSI KELUAR GAME ---
    public void KeluarGame()
    {
        Application.Quit();
        Debug.Log("Game Ditutup!"); // Hanya terlihat di dalam Unity Editor
    }
}