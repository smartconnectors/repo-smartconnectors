using System;
using System.Collections.Generic;
using System.Text;

namespace SmartConnectors.Core
{
    public class ReportDetail
    {
        public string FieldName;
        public string TableName;
        public string Subquery;
        public System.Nullable<int> DisplayOrder;
        public System.DateTime CreateDateUtc;
        public bool IsDeleted;
        public string Filter;
        public string GroupBy;
        public string OrderBy;
        public bool IsOuter;
        public string JoinType;
        public string ValueType;
    }

    //public class SqlGenerator
    //{
    //    private string BuildSql(List<ReportDetail> details)
    //    {
    //        /* declare variables
    //    for working with strings we use StringBuilder, instead of String, since we need to repeatedly modify the string.   */
    //        StringBuilder sql = new StringBuilder();
    //        var tables = new List<string>();
    //        StringBuilder select = new StringBuilder();
    //        StringBuilder from = new StringBuilder();
    //        StringBuilder where = new StringBuilder();
    //        StringBuilder defaultFilters = new StringBuilder();

    //        // build select and from sections:
    //        foreach (var detail in details)
    //        {
    //            var field = detail.FieldName;
    //            var table = detail.TableName;
    //            var filter = detail.Filter;
    //            var joinType = detail.JoinType;
    //            var isLocal = crt.IsLocal;
    //            var defaultFilter = crt.DefaultFilter;
    //            var field = detail.FieldName;
    //            var filter = detail.Filter;
    //            var rt = detail.ReportTable;
    //            var joinOn = rt.JoinOn;
    //            var localPrefix = "@server@.dbo.";
    //            var defaultJoinType = rt.DefaultJoinType;
    //            var joinType = joinType ?? defaultJoinType;
    //            var friendlyName = GetFriendlyName(table, field);

    //            if (select.Length == 0)
    //            {
    //                select.Append(string
    //                      .Format("select [{0}].[{1}] AS {2}", table, field, friendlyName))
    //            }
    //            else
    //            {
    //                select.Append(",[").Append(table).Append("].[")
    //                      .Append(field).Append("] AS ").Append(friendlyName);
    //            }

    //            if (!tables.Contains(table))
    //            {
    //                // default filters
    //                if (!string.IsNullOrEmpty(defaultFilter))
    //                {
    //                    if (defaultFilters.Length == 0)
    //                    {
    //                        defaultFilters = defaultFilters.Append((")  
    //                                        .Append(defaultFilter).Append(")");
    //                    }
    //                    else
    //                    {
    //                        defaultFilters.Append(" and (").Append(defaultFilter).Append(")");
    //                    }
    //                }
    //                if (from.Length == 0)
    //                {
    //                    from = from.Clear().Append("from ").Append(localPrefix)
    //                                      .Append("[").Append(table).Append("]");
    //                }
    //                else
    //                {
    //                    from.Append(" ").Append(joinType)) .Append(" join ")
    //                        .Append(localPrefix).Append("[").Append(table)
    //                        .Append("] on ").Append(joinOn);
    //                }
    //            }
    //            if (!tables.Contains(table))
    //            {
    //                tables.Add(table);
    //            }
    //        }

    //        //add filters to the where section
    //        int clauseCount = details.Where(x => !string.IsNullOrEmpty(x.Filter)).Count();
    //        int order = 1;
    //        foreach (var detail in details.Where(x => !string.IsNullOrEmpty(x.Filter)).ToList())
    //        {
    //            if (where.Length == 0)
    //            {
    //                where = where.Clear().Append("(" + detail.Filter + ")")
    //                             .Append((clauseCount != order ? " AND" : "") + " ");
    //            }
    //            else
    //            {
    //                where.Append("(" + detail.Filter + ")")
    //                .Append((clauseCount != order ? " AND" : "") + " ");
    //            }
    //            order++;
    //        }
    //        var subWhere = where.ToString();
    //        where.Clear().Append(" \nwhere ")
    //               .Append((subWhere.ToString() != "" ? subWhere.ToString() : ""))
    //               .Append((defaultFilters
    //               .ToString() != "" ? (subWhere.ToString() != "" ? " and " : "") + defaultFilters.ToString() : ""));

    //        // append the query string
    //        sql = select.CopyTo(sql).Append("\n").Append(from)
    //                    .Append("\n").Append(where).Append("\n").Append("\n");

    //        return sql.ToString();
    //    }
    //}
}
