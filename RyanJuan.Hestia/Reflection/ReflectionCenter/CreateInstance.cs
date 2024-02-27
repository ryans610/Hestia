namespace RyanJuan.Hestia;

public static partial class ReflectionCenter
{
    public static T CreateInstance<T>()
    {
        return ExpressionConstructor<T>.Factory.Invoke();
    }

    public static void SetInstanceFactory<T>(Func<T> factory)
    {
        Error.ThrowIfArgumentNull(nameof(factory), factory);
        ExpressionConstructor<T>.Factory = factory;
    }

    private static class ExpressionConstructor<T>
    {
        private static Func<T>? s_factory = null;

        public static Func<T> Factory
        {
            get => s_factory ??= Expression
                .Lambda<Func<T>>(Expression.New(typeof(T)))
                .Compile();
            set => s_factory = value;
        }
    }
}
