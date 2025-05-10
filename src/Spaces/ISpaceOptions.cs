using System.Collections.Generic;

/// <summary>
/// Interfaz que permite implementar opciones de eleccion a las casillas
/// </summary>
public interface ISpaceOptions
{
    List<Space> SpaceOptions { get; }
}
