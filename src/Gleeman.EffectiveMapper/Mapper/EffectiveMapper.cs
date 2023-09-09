namespace Gleeman.EffectiveMapper.Mapper;

public class EffectiveMapper: IEffectiveMapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="mapProperty">If properties is not match</param>
    /// <returns></returns>
    public TResult Map<TResult, TSource>(TSource source, Func<TSource, TResult> mapProperty = null) 
    where TResult : class, new()
    where TSource : class, new()
    {
        var result = Activator.CreateInstance<TResult>();
        foreach (var sourceProperty in typeof(TSource).GetProperties())
        {
            var resultProperty = typeof(TResult).GetProperty(sourceProperty.Name);
            if (resultProperty != null)
            {
                resultProperty.SetValue(result, sourceProperty.GetValue(source));
            }
            else
            {
                result = mapProperty?.Invoke(source);
            }
        }

        return result;
    }

    public IEnumerable<TResult> Map<TResult, TSource>(IEnumerable<TSource> sources, Func<TSource, TResult> mapProperty = null)
    where TResult : class, new()
    where TSource : class, new()
    {
        return sources.Select(source => Map<TResult, TSource>(source, mapProperty)).ToList();
    }
}
