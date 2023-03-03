namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetIsNullOrEmptyStringAttribute : RequiredIfAttribute
{
    /// <inheritdoc cref="RequiredIfTargetIsNullOrEmptyStringAttribute"/>
    /// <param name="targetPropertyName"></param>
    public RequiredIfTargetIsNullOrEmptyStringAttribute(string targetPropertyName)
        : base(RequiredIfMode.TargetIsNullOrEmptyString, targetPropertyName)
    {
    }
}
