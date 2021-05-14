using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
            
        }
        else
        {
            StopThrusting();
        }
        
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotating();

        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }

    }
    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    } 
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}






