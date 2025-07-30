using Dapper;
using System;
using System.Data;

public class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        // Convertir a DateTime (con hora mínima) para que SQL Server lo acepte
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        return value switch
        {
            DateTime dt => DateOnly.FromDateTime(dt),
            string s => DateOnly.Parse(s),
            _ => throw new DataException($"Cannot convert {value?.GetType().Name ?? "null"} to DateOnly")
        };
    }
}