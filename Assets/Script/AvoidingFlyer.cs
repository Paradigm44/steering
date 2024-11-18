using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidingFlyer : MonoBehaviour
{
    // Kecepatan target pergerakan objek
    public float targetVelocity = 10.0f;
    
    // Jumlah ray yang ditembakkan dari objek untuk mendeteksi rintangan
    public int numberOfRays = 17;
    
    // Sudut total distribusi ray di sekitar objek
    public float angle = 90.0f;

    // Jarak maksimal ray untuk mendeteksi rintangan
    public float rayRange = 1;

    // Fungsi Start dipanggil sekali saat inisialisasi objek
    void Start()
    {
        // Tidak ada tindakan yang dilakukan pada Start saat ini
    }

    // Fungsi Update dipanggil setiap frame
    void Update()
    {
        // Menyimpan perubahan posisi yang akan diterapkan ke objek
        var deltaPosition = Vector3.zero;
        
        // Loop untuk menembakkan ray sebanyak numberOfRays
        for (int i = 0; i < numberOfRays; i++)
        {
            // Ambil rotasi objek saat ini
            var rotation = this.transform.rotation;
            
            // Modifikasi rotasi untuk ray ke-i, menggunakan sudut yang berbeda-beda
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays)) * angle * 2 - angle, this.transform.up);
            
            // Hitung arah ray berdasarkan rotasi objek dan modifikasi rotasi
            var direction = rotation * rotationMod * Vector3.forward;

            // Membuat ray dari posisi objek dan arah yang telah dihitung
            var ray = new Ray(this.transform.position, direction);
            
            // Variabel untuk menyimpan informasi hasil raycast (apakah ada yang terkena ray)
            RaycastHit hitinfo;
            
            // Jika ray bertemu dengan objek dalam jarak rayRange
            if (Physics.Raycast(ray, out hitinfo, rayRange))
            {
                // Jika ada rintangan, kurangi posisi objek ke arah berlawanan untuk menghindari
                deltaPosition -=  (1.0f / numberOfRays) * targetVelocity * direction;
            }
            else
            {
                // Jika tidak ada rintangan, tambah posisi objek untuk terus maju ke arah ray
                deltaPosition +=  (1.0f / numberOfRays) * targetVelocity * direction;
            }
        }

        // Terapkan perubahan posisi objek berdasarkan deltaPosition yang dihitung
        this.transform.position += deltaPosition * Time.deltaTime;
    }

    // Fungsi ini dipanggil di editor Unity untuk menggambar ray secara visual di Scene View
    void OnDrawGizmos()
    {
        // Loop untuk menggambar ray yang ditembakkan dalam editor Unity
        for (int i = 0; i < numberOfRays; i++)
        {
            // Ambil rotasi objek saat ini
            var rotation = this.transform.rotation;
            
            // Modifikasi rotasi untuk ray ke-i, menggunakan sudut yang berbeda-beda
            var rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays)) * angle * 2 - angle, this.transform.up);
            
            // Hitung arah ray berdasarkan rotasi objek dan modifikasi rotasi
            var direction = rotation * rotationMod * Vector3.forward;

            // Gambar ray di editor Unity untuk memberikan visualisasi
            Gizmos.DrawRay(this.transform.position, direction);
        }
    }
}
