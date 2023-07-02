using HarmonyLib;
using Kingmaker.BundlesLoading;
using Kingmaker.ResourceLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Utilities {
    internal static class Dynamic {
        private interface IDynamicAssetLink {
            WeakResourceLink Link { get; }
            Action<UnityEngine.Object> Init { get; }
            UnityEngine.Object CreateObject();
            Type AssetType { get; }
            Type LinkType { get; }
        }

        private abstract class DynamicAssetLink<T, TLink> : IDynamicAssetLink
            where T : UnityEngine.Object
            where TLink : WeakResourceLink<T>, new() {
            public virtual Type AssetType => typeof(T);
            public virtual Type LinkType => typeof(TLink);

            public virtual TLink Link { get; }

            WeakResourceLink IDynamicAssetLink.Link => Link;
            public virtual Action<T> Init { get; }
            Action<UnityEngine.Object> IDynamicAssetLink.Init => obj => {
                if (obj is not T t)
                    throw new InvalidCastException();

                Init(t);
            };

            public DynamicAssetLink(TLink assetLink, Action<T> init) {
                Init = init;
                Link = assetLink;
            }

            protected abstract T CloneObject(T obj);

            public virtual UnityEngine.Object CreateObject() {
                if (Link.LoadObject() is not T obj)
                    throw new Exception($"Failed to instantiate asset from {Link.AssetId}");

                var copy = CloneObject(obj);
                Init(copy);

                return copy;
            }
        }

        private class DynamicGameObjectLink<TLink> : DynamicAssetLink<GameObject, TLink>
            where TLink : WeakResourceLink<GameObject>, new() {
            protected override GameObject CloneObject(GameObject obj) {
                var copy = GameObject.Instantiate(obj);

                UnityEngine.Object.DontDestroyOnLoad(copy);

                return copy;
            }

            public DynamicGameObjectLink(TLink link, Action<GameObject> init) : base(link, init) { }
        }

        private class DynamicMonobehaviourLink<T, TLink> : DynamicAssetLink<T, TLink>
            where T : MonoBehaviour
            where TLink : WeakResourceLink<T>, new() {
            protected override T CloneObject(T obj) {
                Main.Log($"Trying to clone {obj.gameObject}");

                var copy = GameObject.Instantiate(obj.gameObject);
                copy.SetActive(false);

                UnityEngine.Object.DontDestroyOnLoad(copy);

                var component = copy.GetComponent<T>();

                Main.Log($"GetComponent |{typeof(T)}| = {component?.ToString() ?? "<null>"}");

                return component;
            }

            public DynamicMonobehaviourLink(TLink link, Action<T> init) : base(link, init) { }
        }

        private static readonly Dictionary<string, IDynamicAssetLink> DynamicAssetLinks = new();

        private static TLink CreateDynamicAssetLinkProxy<TLink>(IDynamicAssetLink proxy, string? assetId = null)
            where TLink : WeakResourceLink, new() {
            if (string.IsNullOrEmpty(assetId))
                assetId = null;

            assetId ??= Guid.NewGuid().ToString("N").ToLowerInvariant();

            DynamicAssetLinks.Add(assetId, proxy);

            return new() { AssetId = assetId };
        }

        public static TLink CreateDynamicProxy<TLink>(this TLink link, Action<GameObject> init, string? assetId = null)
            where TLink : WeakResourceLink<GameObject>, new() =>
            CreateDynamicAssetLinkProxy<TLink>(new DynamicGameObjectLink<TLink>(link, init), assetId);

        public static TLink CreateDynamicMonobehaviourProxy<T, TLink>(this TLink link, Action<T> init, string? assetId = null)
            where T : MonoBehaviour
            where TLink : WeakResourceLink<T>, new() =>
            CreateDynamicAssetLinkProxy<TLink>(new DynamicMonobehaviourLink<T, TLink>(link, init), assetId);

        [HarmonyPatch]
        static class Patches {
            [HarmonyPatch(typeof(AssetBundle), nameof(AssetBundle.LoadAsset), typeof(string), typeof(Type))]
            [HarmonyPrefix]
            static bool LoadAsset_Prefix(string name, ref UnityEngine.Object __result) {
                try {
                    if (DynamicAssetLinks.ContainsKey(name)) {
                        var assetProxy = DynamicAssetLinks[name];
                        Main.Log($"Creating dynamic asset: {name} -> {assetProxy.Link.AssetId}");

                        var copy = assetProxy.CreateObject();

                        if (copy is MonoBehaviour mb)
                            __result = mb.gameObject;
                        else
                            __result = copy;

                        return false;
                    }
                } catch (Exception e) {
                    Main.Error(e, $"Failed to load asset: {name}");
                }

                return true;
            }

            [HarmonyPatch(typeof(BundlesLoadService), nameof(BundlesLoadService.GetBundleNameForAsset))]
            [HarmonyPrefix]
            static bool GetBundleNameForAsset_Prefix(string assetId, ref string __result, BundlesLoadService __instance) {
                try {
                    if (DynamicAssetLinks.ContainsKey(assetId)) {
                        var assetProxy = DynamicAssetLinks[assetId];

                        Main.Log($"Getting bundle for dynamic asset {assetId} -> {assetProxy.Link.AssetId}");

                        __result = __instance.GetBundleNameForAsset(assetProxy.Link.AssetId);
                        return false;
                    }
                } catch (Exception e) {
                    Main.Error(e, $"Failed to fetch bundle name for {assetId}.");
                }
                return true;
            }
        }               
    }
    public static class FxDebug {
        public static string DumpGameObject(GameObject gameObject) {
            static IEnumerable<string> DumpGameObjectInner(GameObject obj, int depth = 0) {
                var indent = new string(Enumerable.Repeat("    ", depth).SelectMany(Functional.Identity).ToArray());

                yield return $"{indent}{obj.name}";

                var components = obj.GetComponents<Component>();

                yield return $"{indent}  {components.Length} Components:";

                foreach (var c in components) {
                    yield return $"{indent}    {c.GetType()}";
                    //if (c is ParticleSystemRenderer psr)
                    //{
                    //    yield return $"{indent}      Material: {psr.material.name}";
                    //    yield return $"{indent}      Shader: {psr.material.shader.name}";
                    //}
                }

                yield return $"{indent}  {obj.transform.childCount} Child GameObjects:";

                for (var i = 0; i < obj.transform.childCount; i++) {
                    foreach (var line in DumpGameObjectInner(obj.transform.GetChild(i).gameObject, depth + 1))
                        yield return line;
                }
            }
            var sb = new StringBuilder();

            foreach (var line in DumpGameObjectInner(gameObject))
                sb.AppendLine(line);

            return sb.ToString();
        }
    }
    public static partial class Functional {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Identity<T>(T x) => x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Ignore<T>(T _) { }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static U Apply<T, U>(this T obj, Func<T, U> f) => f(obj);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T UpCast<TParam, T>(TParam x) where TParam : T => (T)x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static U Downcast<T, U>(this T obj) where U : T => (U)obj!;
    }
}
