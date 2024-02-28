using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="RequiredIfTargetIsNullOrEmptyStringAttribute"/>
/// <param name="targetPropertyName"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetIsNullOrEmptyStringAttribute(string targetPropertyName) : 
    RequiredIfAttribute(RequiredIfMode.TargetIsNullOrEmptyString, targetPropertyName);
