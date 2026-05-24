using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class soundOnCollision : MonoBehaviour
{
    AudioSource auSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        auSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        auSource.Play();
    }

}
