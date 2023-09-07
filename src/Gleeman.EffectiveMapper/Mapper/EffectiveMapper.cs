namespace Gleeman.EffectiveMapper.Mapper;

public class EffectiveMapper: IEffectiveMapper
{
    public TResult Map<TResult, TSource>(TSource source)
    {
        var result = Activator.CreateInstance<TResult>();
        foreach (var sourceProperty in typeof(TSource).GetProperties())
        {
            var resultProperty = typeof(TResult).GetProperty(sourceProperty.Name);
            if (resultProperty != null)
            {
                resultProperty.SetValue(result, sourceProperty.GetValue(source));
            }
        }

        return result;
    }

    public IEnumerable<TResult> Map<TResult, TSource>(IEnumerable<TSource> sources)
    {
        return sources.Select(source => Map<TResult, TSource>(source)).ToList();
    }
}
