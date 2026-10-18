using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class FPSMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionBuffer = 0.1f;

    private Rigidbody rb;
    private BoxCollider box;

    public int Puan;

    public TextMeshProUGUI Puantext;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        box = GetComponent<BoxCollider>();
        Puan = 0;
        Puantext.text = "Puan: " + Puan.ToString();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.forward * vertical + transform.right * horizontal).normalized;

        if (moveDir != Vector3.zero)
        {
            Vector3 halfExtents = box.size * 0.5f;
            Vector3 castOrigin = transform.position + box.center;

            // BoxCast: karakterin hareket yönünde çarpýþma kontrolü
            if (!Physics.BoxCast(castOrigin, halfExtents, moveDir, out RaycastHit hit, transform.rotation, moveSpeed * Time.fixedDeltaTime + collisionBuffer))
            {
                rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                Debug.DrawRay(transform.position, moveDir * 1.0f, Color.red, 0.1f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            Puan++;
            Destroy(other.gameObject); // Toprak nesnesini yok et
            Puantext.text = "Puan: " + Puan.ToString(); // Puaný güncelle
        }
    }
}
