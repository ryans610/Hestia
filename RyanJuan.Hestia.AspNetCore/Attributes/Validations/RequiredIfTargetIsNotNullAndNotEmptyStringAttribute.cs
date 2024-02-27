namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetIsNotNullAndNotEmptyStringAttribute : RequiredIfAttribute
{
    /// <inheritdoc cref="RequiredIfTargetIsNotNullAndNotEmptyStringAttribute"/>
    /// <param name="targetPropertyName"></param>
    public RequiredIfTargetIsNotNullAndNotEmptyStringAttribute(string targetPropertyName)
        : base(RequiredIfMode.TargetIsNotNullAndNotEmptyString, targetPropertyName)
    {
    }
}
