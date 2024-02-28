using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="RequiredIfTargetValueNotEqualsAttribute"/>
/// <param name="targetPropertyName"></param>
/// <param name="targetValue"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetValueNotEqualsAttribute(string targetPropertyName, object? targetValue = null) :
    RequiredIfAttribute(RequiredIfMode.TargetValueNotEquals, targetPropertyName, targetValue);
