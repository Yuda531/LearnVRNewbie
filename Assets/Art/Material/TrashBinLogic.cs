using UnityEngine;

public class TrashBinLogic : MonoBehaviour
{
    [Header("Setting Tong Sampah")]
    public string acceptedTag; // "Organic" atau "Anorganic"

    [Header("Efek Penolakan")]
    public float rejectForce = 5f; // Kekuatan lemparan keluar

    [Header("Efek Suara")]
    public AudioSource audioSource; // Komponen speaker
    public AudioClip correctSound;  
    public AudioClip wrongSound;    

    private void OnTriggerEnter(Collider other)
    {
        // 1. LOGIKA BENAR
        if (other.gameObject.CompareTag(acceptedTag))
        {
            Debug.Log("Benar! Sampah diterima.");
            
            // Mainkan suara BENAR 
            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
            }

            Destroy(other.gameObject); 
        }
        
        // 2. LOGIKA SALAH
        else if (other.gameObject.CompareTag("Organic") || other.gameObject.CompareTag("Anorganic"))
        {
            Debug.Log("Salah! Ditolak.");

            // Mainkan suara SALAH
            if (audioSource != null && wrongSound != null)
            {
                audioSource.PlayOneShot(wrongSound);
            }
            
            // Efek Fisika (Reject)
            Rigidbody rb = other.attachedRigidbody; 

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; 
                rb.angularVelocity = Vector3.zero;

                Vector3 rejectDirection = Vector3.up;
                rb.AddForce(rejectDirection * rejectForce, ForceMode.VelocityChange);
            }
        }
    }
}