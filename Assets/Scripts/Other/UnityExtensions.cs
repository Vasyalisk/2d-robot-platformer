using UnityEngine;

public static class UnityExtensions
{
    // Check if layer mask contains layer
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
