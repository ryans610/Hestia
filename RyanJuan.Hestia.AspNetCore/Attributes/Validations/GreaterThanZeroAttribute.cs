namespace RyanJuan.Hestia.AspNetCore.Attributes.Validations;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public class GreaterThanZeroAttribute : GreaterThanAttribute
{
    /// <inheritdoc cref="GreaterThanZeroAttribute"/>
    public GreaterThanZeroAttribute() : base(0) { }
}
