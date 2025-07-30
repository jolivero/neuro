using Dapper;
using System;
using System.Data;

public class TimeOnlyHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = value.ToTimeSpan();
    }

    public override TimeOnly Parse(object value)
    {
        return value switch
        {
            TimeSpan ts => TimeOnly.FromTimeSpan(ts),
            string s => TimeOnly.Parse(s),
            _ => throw new DataException($"Cannot convert {value.GetType()} to TimeOnly")
        };
    }
}
