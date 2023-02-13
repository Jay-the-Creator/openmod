using System;
using System.Threading.Tasks;
using OpenMod.API.Commands;

namespace OpenMod.Unturned.AssetHelpers
{
    public class AssetIdCommandParameterResolveProvider : ICommandParameterResolveProvider
    {
        public Task<object?> ResolveAsync(Type type, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Task.FromResult<object?>(AssetId.Invalid);
            }

            if (AssetId.TryParse(input, out var assetId))
            {
                return Task.FromResult<object?>(assetId);
            }

            return Task.FromResult<object?>(AssetId.Invalid);
        }

        public bool Supports(Type type) => type == typeof(AssetId);
    }
}
