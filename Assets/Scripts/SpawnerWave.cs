using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWave : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public string namaWave;
        public GameObject[] prefabsDiWaveIni; 
        public int jumlahMusuh;
        public float jedaSpawn;
        public float kecepatanMusuh;
    }

    [Header("Pengaturan Wave")]
    public Wave[] daftarWave;
    
    private int waveSekarang = 0;

    void Start() {
        StartCoroutine(MulaiWave());
    }

    IEnumerator MulaiWave() {
        while (waveSekarang < daftarWave.Length) {
            Wave w = daftarWave[waveSekarang];
            Debug.Log("Memulai: " + w.namaWave);

            for (int i = 0; i < w.jumlahMusuh; i++) {
                // Kita kirim daftar prefab wave ini ke fungsi MunculkanMusuh
                MunculkanMusuh(w.prefabsDiWaveIni, w.kecepatanMusuh);
                yield return new WaitForSeconds(w.jedaSpawn);
            }

            waveSekarang++;
            yield return new WaitForSeconds(3f); // Jeda antar wave
        }
    }

    void MunculkanMusuh(GameObject[] prefabs, float speed) {
        // Keamanan: Cek apakah prefabs sudah diisi di Inspector
        if (prefabs.Length == 0) return;

        float xAcak = Random.Range(-2.5f, 2.5f);
        Vector3 pos = new Vector3(xAcak, transform.position.y, 0);
        
        // Memilih salah satu prefab secara acak dari daftar milik Wave ini saja
        int index = Random.Range(0, prefabs.Length);
        GameObject musuh = Instantiate(prefabs[index], pos, Quaternion.identity);
        
        // Mengatur kecepatan musuh sesuai input di Inspector Wave
        if(musuh.GetComponent<PergerakanObject>() != null) {
            musuh.GetComponent<PergerakanObject>().kecepatanTurun = speed;
        }
    }
}