using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="RequiredIfTargetValueEqualsAttribute"/>
/// <param name="targetPropertyName"></param>
/// <param name="targetValue"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetValueEqualsAttribute(string targetPropertyName, object? targetValue = null) :
    RequiredIfAttribute(RequiredIfMode.TargetValueEquals, targetPropertyName, targetValue);
