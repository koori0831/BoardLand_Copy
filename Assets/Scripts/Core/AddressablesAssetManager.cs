using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core
{
    public class AddressablesHandleRentalInfo
    {
        public AsyncOperationHandle Handle { get; set; }
        public int ReferenceCount { get; set; }

        public AddressablesHandleRentalInfo(AsyncOperationHandle handle, int referenceCount = 1)
        {
            this.Handle = handle;
            this.ReferenceCount = referenceCount;
        }
    }

    public static class AddressablesAssetManager
    {
        private static Dictionary<string, AddressablesHandleRentalInfo> assetCache = new();
        private static Dictionary<string, float> releaseQueue = new();

        private static readonly float defaultReleaseDelay = 60f;
        private static bool isReleaseLoopRunning = false;

        public static async UniTask<GameObject> LoadAndInstantiateAsync(string assetAddress, Transform parent = null)
        {
            if (releaseQueue.ContainsKey(assetAddress))
                releaseQueue.Remove(assetAddress);

            // 캐시에 없는 경우
            if (assetCache.TryGetValue(assetAddress, out AddressablesHandleRentalInfo rentalInfo) == false)
            {
                AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(assetAddress);
                rentalInfo = new AddressablesHandleRentalInfo(handle);
                assetCache.Add(assetAddress, rentalInfo);

                // 로드가 완료될 때까지 await
                await handle.ToUniTask();

                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    LogManager.Log($"에셋 로드 실패: {assetAddress}", ELogType.Addressables, ELogCategory.AssetManager, ELogMessageType.Error);
                    assetCache.Remove(assetAddress);
                    return null;
                }
            }
            else if (rentalInfo.Handle.IsDone == false)
            {
                // 캐시에는 있는데 로드는 완료되지 않은 경우 대기
                await rentalInfo.Handle.ToUniTask();
            }

            // 참조 카운트 증가시키고 생성
            rentalInfo.ReferenceCount++;

            GameObject prefab = rentalInfo.Handle.Convert<GameObject>().Result;
            GameObject instance = GameObject.Instantiate(prefab, parent);

            var tracker = instance.AddComponent<AddressablePrefabTracker>();
            tracker.Initialize(assetAddress);

            return instance;
        }

        public static void AddressRelease(string assetAddress, float overrideReleaseDelay = 0.0f)
        {
            if (string.IsNullOrEmpty(assetAddress) || assetCache.TryGetValue(assetAddress, out var rentalInfo) == false)
                return;

            rentalInfo.ReferenceCount--;

            // 참조 카운트가 0이면 릴리즈 큐에 등록
            if (rentalInfo.ReferenceCount <= 0)
            {
                float releaseDelay = Mathf.Approximately(overrideReleaseDelay, 0.0f) ? defaultReleaseDelay : overrideReleaseDelay;
                releaseQueue[assetAddress] = releaseDelay;

                if (isReleaseLoopRunning == false)
                    StartReleaseTimerLoop().Forget();
            }
        }

        private static async UniTaskVoid StartReleaseTimerLoop()
        {
            isReleaseLoopRunning = true;
            List<string> keysToRemove = new();

            while (releaseQueue.Count > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                keysToRemove.Clear();

                foreach (var kvp in releaseQueue)
                {
                    float remainingTime = kvp.Value - 1f;
                    if (remainingTime <= 0f)
                        keysToRemove.Add(kvp.Key);
                }

                // 시간이 다 된 에셋들은 메모리에서 내리기
                foreach (var key in keysToRemove)
                {
                    releaseQueue.Remove(key);

                    if (assetCache.TryGetValue(key, out var rentalInfo))
                    {
                        if (rentalInfo.Handle.IsValid())
                            Addressables.Release(rentalInfo.Handle);

                        assetCache.Remove(key);
                        LogManager.Log($"에셋 Release: {key}", ELogType.Addressables, ELogCategory.AssetManager);
                    }
                }
            }

            isReleaseLoopRunning = false;
        }
    }
}