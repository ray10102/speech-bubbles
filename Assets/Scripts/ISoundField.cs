using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundField {

    /// <summary>
    /// Behavior for starting the spawn process of a field.
    /// </summary>
    void StartSpawn();

    /// <summary>
    /// Behavior for ending the spawnig process of a field.
    /// </summary>
    void EndSpawn();

    /// <summary>
    /// Behavior while spawning.
    /// </summary>
    void Spawning();
    
    /// <summary>
    /// Behavior when the user/phone collides with the field.
    /// </summary>
    void OnCollideWithUser();

    /// <summary>
    /// Behavior when the usr/phone stops colliding with this field.
    /// </summary>
    void OnExitWithUser();

    /// <summary>
    /// Sets the color of this field to the given color.
    /// </summary>
    void SetColor(ASoundField.Color color);
}
