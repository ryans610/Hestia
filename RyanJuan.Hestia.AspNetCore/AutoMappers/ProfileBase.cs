using AutoMapper;

using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.AutoMappers;

/// <summary>
/// Provides a named configuration for maps. Naming conventions become scoped per profile.
/// Extended with <see cref="CreateChainMap{TSource,TIntermediate,TDestination}"/>.
/// </summary>
public abstract class ProfileBase : Profile
{
    /// <summary>
    /// Create a mapping configuration from the <typeparamref name="TSource"/> type
    /// to the <typeparamref name="TIntermediate"/> type
    /// and then to the <typeparamref name="TDestination"/> type.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TIntermediate"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    [PublicAPI]
    public void CreateChainMap<TSource, TIntermediate, TDestination>()
    {
        CreateMap<TSource, TDestination>()
            .ConvertUsing((entity, _, context) =>
            {
                var intermediate = context.Mapper.Map<TIntermediate>(entity);
                return context.Mapper.Map<TDestination>(intermediate);
            });
    }

    /// <summary>
    /// Create a mapping configuration from the <typeparamref name="TSource"/> type
    /// to the <typeparamref name="TIntermediate"/> type
    /// and then to the <typeparamref name="TDestination"/> type,
    /// and another mapping configuration with reverse order.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TIntermediate"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    [PublicAPI]
    public void CreateChainMapAndReverse<TSource, TIntermediate, TDestination>()
    {
        CreateChainMap<TSource, TIntermediate, TDestination>();
        CreateChainMap<TDestination, TIntermediate, TSource>();
    }
}
