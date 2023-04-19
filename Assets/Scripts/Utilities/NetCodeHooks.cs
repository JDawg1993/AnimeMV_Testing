using System;
using Unity.Netcode;

// useful for classes that can't be NetworkBehaviours themselves (for example, with dedicated servers, you can't have a NetworkBehaviour that exists
// on clients but gets stripped on the server, this will mess with your NetworkBehaviour indexing.
public class NetCodeHooks : NetworkBehaviour
{
    public event Action OnNetworkSpawnHook;

    public event Action OnNetworkDespawnHook;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        OnNetworkSpawnHook?.Invoke();
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        OnNetworkDespawnHook?.Invoke();
    }
}