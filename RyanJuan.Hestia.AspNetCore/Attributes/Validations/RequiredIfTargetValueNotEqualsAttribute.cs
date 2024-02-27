namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetValueNotEqualsAttribute : RequiredIfAttribute
{
    /// <inheritdoc cref="RequiredIfTargetValueNotEqualsAttribute"/>
    /// <param name="targetPropertyName"></param>
    /// <param name="targetValue"></param>
    public RequiredIfTargetValueNotEqualsAttribute(string targetPropertyName, object? targetValue = null)
        : base(RequiredIfMode.TargetValueNotEquals, targetPropertyName, targetValue)
    {
    }
}
