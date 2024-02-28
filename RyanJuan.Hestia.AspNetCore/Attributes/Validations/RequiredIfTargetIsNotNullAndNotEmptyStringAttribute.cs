using JetBrains.Annotations;

namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
/// <inheritdoc cref="RequiredIfTargetIsNotNullAndNotEmptyStringAttribute"/>
/// <param name="targetPropertyName"></param>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetIsNotNullAndNotEmptyStringAttribute(string targetPropertyName) :
    RequiredIfAttribute(RequiredIfMode.TargetIsNotNullAndNotEmptyString, targetPropertyName);
