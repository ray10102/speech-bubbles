using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASoundField : MonoBehaviour, ISoundField {

    public enum Color { NONE, RED, BLUE, GREEN, YELLOW, PURPLE };

    protected AudioSource audioSource;
    protected Animator anim;
    protected Color color;
    protected Renderer mesh;
    [SerializeField]
    protected Material none, red, blue, green, yellow, purple;
    public static Color selectedColor;

    // Initializes the animator, the mesh renderer, and the audio source. Sets the color to the currently selected color.
    protected virtual void Start() {
        SetColor(selectedColor);
        mesh = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            OnCollideWithUser();
        }
    }

    protected virtual void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            OnExitWithUser();
        }
    }

    // BUBBLE ACTIONS
    
    /// <summary>
    /// Sets the color of the field to the given color.
    /// </summary>
    /// <param name="color"> The color to set this field to. </param>
    public virtual void SetColor(Color color) {
        switch (color) {
            case Color.NONE:
                mesh.material = none;
                break;
            case Color.RED:
                setMaterial(red);
                break;
            case Color.GREEN:
                setMaterial(green);
                break;
            case Color.BLUE:
                setMaterial(blue);
                break;
            case Color.YELLOW:
                setMaterial(yellow);
                break;
            case Color.PURPLE:
                setMaterial(purple);
                break;
        }
    }

    /// <summary>
    /// Helper for setColor(); sets the material of the field to the given material.
    /// </summary>
    /// <param name="mat"></param>
    private void setMaterial(Material mat) {
        if (!mesh) {
            mesh = GetComponent<Renderer>();
        }
        if (mat != null) {
            mesh.material = mat;
        } else {
            mesh.material = none;
        }
    }

    /// <summary>
    /// Behavior on collision with the user's phone. Plays the audio in a loop by default; can be overriden.
    /// </summary>
    public virtual void OnCollideWithUser() {
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// Behavior on ending collision with the user's phone. Stops audio by default; can be overriden.
    /// </summary>
    public virtual void OnExitWithUser() {
        if (!audioSource) {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.Stop();
    }

    /// <summary>
    /// Behavior at the start of the spawning of a field (i.e., when the spawn button is first pressed).
    /// </summary>
    public abstract void StartSpawn();

    /// <summary>
    /// Behavior at the end of the spawning of a field (i.e., when the spawn button is released). 
    /// </summary>
    public abstract void EndSpawn();

    /// <summary>
    /// Behavior during the spawning of a field. Does nothing by default but can be overriden.
    /// </summary>
    public virtual void Spawning() { }
}
