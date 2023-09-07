namespace Gleeman.EffectiveMapper.Mapper;

public interface IEffectiveMapper
{
    TResult Map<TResult, TSource>(TSource source);
    IEnumerable<TResult> Map<TResult, TSource>(IEnumerable<TSource> sources);
}
