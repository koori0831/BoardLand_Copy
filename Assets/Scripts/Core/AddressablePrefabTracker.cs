using UnityEngine;

namespace Core
{
    public class AddressablePrefabTracker : MonoBehaviour
    {
        private string address;
        public void Initialize(string address)
        {
            this.address = address;
        }

        private void OnDestroy()
        {
            AddressablesAssetManager.AddressRelease(address);
        }
    }
}