namespace Gleeman.EffectiveMapper.Mapper;

public interface IEffectiveMapper
{
    TResult Map<TResult, TSource>(TSource source, Func<TSource, TResult> mapProperty = null) 
    where TResult : class
    where TSource : class;
    IEnumerable<TResult> Map<TResult, TSource>(IEnumerable<TSource> sources, Func<TSource, TResult> mapProperty = null)
    where TResult : class
    where TSource : class;
}
