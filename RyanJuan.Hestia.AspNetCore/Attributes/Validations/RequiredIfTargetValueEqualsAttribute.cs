namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RequiredIfTargetValueEqualsAttribute : RequiredIfAttribute
{
    /// <inheritdoc cref="RequiredIfTargetValueEqualsAttribute"/>
    /// <param name="targetPropertyName"></param>
    /// <param name="targetValue"></param>
    public RequiredIfTargetValueEqualsAttribute(string targetPropertyName, object? targetValue = null)
        : base(RequiredIfMode.TargetValueEquals, targetPropertyName, targetValue)
    {
    }
}
