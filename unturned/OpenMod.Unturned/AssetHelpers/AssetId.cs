using System;
using SDG.Unturned;

namespace OpenMod.Unturned.AssetHelpers
{
    /// <summary>
    /// Asset id wrapper of newest <see cref="System.Guid"/> and legacy <see cref="ushort"/>
    /// </summary>
    public readonly struct AssetId : IEquatable<AssetId>
    {
        public static readonly AssetId Invalid = new(0);

        public readonly Guid Guid;
        public readonly ushort Id;

        public AssetId(Guid guid)
        {
            Guid = guid;
            Id = 0;
        }

        public AssetId(ushort id)
        {
            Id = id;
            Guid = Guid.Empty;
        }

        public static bool TryParse(string input, out AssetId assetId)
        {
            if (Guid.TryParse(input, out var guid))
            {
                assetId = new(guid);
                return true;
            }

            if (ushort.TryParse(input, out var id))
            {
                assetId = new(id);
                return true;
            }

            assetId = Invalid;
            return false;
        }

        public T? FindItemAsset<T>() where T : ItemAsset
        {
            return Guid.Equals(Guid.Empty)
                ? Assets.find(EAssetType.ITEM, Id) as T
                : Assets.find<T>(Guid);
        }

        public VehicleAsset? FindVehicleAsset()
        {
            return Guid.Equals(Guid.Empty)
                ? Assets.find(EAssetType.VEHICLE, Id) as VehicleAsset
                : Assets.find<VehicleAsset>(Guid);
        }

        public bool Equals(AssetId other)
        {
            return Guid.Equals(Guid.Empty)
                ? Id.Equals(other.Id)
                : Guid.Equals(other.Guid);
        }

        public override int GetHashCode()
        {
            return Guid.Equals(Guid.Empty)
                ? Id.GetHashCode()
                : Guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is AssetId assetId && Equals(assetId);
        }

        public static bool operator ==(AssetId left, AssetId right) => left.Equals(right);
        public static bool operator !=(AssetId left, AssetId right) => !(left == right);
    }
}
