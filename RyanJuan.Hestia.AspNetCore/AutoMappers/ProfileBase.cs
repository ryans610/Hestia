using AutoMapper;

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
    public void CreateChainMap<TSource, TIntermediate, TDestination>()
    {
        CreateMap<TSource, TDestination>()
            .ConvertUsing((entity, _, context) =>
            {
                var intermediate = context.Mapper.Map<TIntermediate>(entity);
                return context.Mapper.Map<TDestination>(intermediate);
            });
    }
}
