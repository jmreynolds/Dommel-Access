using System.Linq;
using System.Reflection;

namespace Dommel.Access
{
    public sealed class AccessSqlBuilder : DommelMapper.ISqlBuilder
    {
        public string BuildInsert(string tableName, string[] columnNames, string[] paramNames, PropertyInfo keyProperty)
        {
            //var keyCol = DommelMapper.Resolvers.Column(keyProperty);
            //var keyparm = $"@{keyProperty.Name }";
            //var cols = columnNames.Where(x => x != keyCol);

            return $"insert into {tableName} ({string.Join(", ", columnNames)}) values ({string.Join(", ", paramNames.Select(x => x.Replace("@", "?")).Select(x => $"{x}?"))});";
        }
    }
    public sealed class AccessUpdateBuilder : DommelMapper.IUpdateBuilder
    {
        public string BuildUpdate(string tableName, PropertyInfo[] typeProperties, PropertyInfo keyProperty)
        {
            var columnNames = typeProperties.Select(p => $"{DommelMapper.Resolvers.Column(p)} = ?{p.Name}?").ToArray();
            return $"update {tableName} set {string.Join(", ", columnNames)} where {DommelMapper.Resolvers.Column(keyProperty)} = ?{keyProperty.Name}?";
        }
    }
}
