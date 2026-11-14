using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PergerakanObject : MonoBehaviour
{
    [Header("Pengaturan Object")]
    // Kecepatan turunnya object, Designer bisa ngatur tingkat kesulitannya di sini
    public float kecepatanTurun = 5f; 
    
    // Batas Y di bawah layar. Sesuaikan dengan ukuran kamera di Unity.
    // kalian bisa ubah ini ke Public jika ingin diatur di Inspector nanti oleh Game Designer.
    private float batasBawahY = -6f; 

    void Update()
    {
        // 1. Menggerakkan object ke bawah secara terus-menerus
        // Vector3.down artinya bergerak di sumbu Y negatif
        transform.Translate(Vector3.down * kecepatanTurun * Time.deltaTime);

        // 2. Menghancurkan object (Destroy) jika melewati batas bawah layar
        if (transform.position.y < batasBawahY)
        {
            Destroy(gameObject);
        }
    }
}
