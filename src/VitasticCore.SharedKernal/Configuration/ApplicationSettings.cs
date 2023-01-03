﻿using VitasticCore.SharedKernal.Guards;

namespace VitasticCore.SharedKernal.Configuration;

/// <summary>
/// General application settings that are pulled from configuration.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// The UI-friendly name of the application.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Check the state of this configuration and throw an exception if it is invalid.
    /// </summary>
    public virtual void Validate()
    {
        _ = Name.EnsureNotNullOrEmpty("Property not found in application configuration.");
    }
}
