﻿using System;
using System.Collections.Generic;
using System.Linq;
using GiveCRM.Models;

namespace GiveCRM.Web.Models.Search
{
    public abstract class SearchCriteria
    {
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public SearchFieldType Type { get; set; }
        public SearchOperator SearchOperator { get; set; }
        public string Value { get; set; }

        public abstract IEnumerable<SearchCriteria> GetEmptyCriteria();
        public abstract bool IsMatch(Member m);

        protected bool Evaluate(object lhs)
    {
        object rhs = null;
        switch (this.Type)
    {
        case SearchFieldType.String: rhs=this.Value; break;
        case SearchFieldType.Int: rhs=int.Parse(this.Value); break;
        case SearchFieldType.Double: rhs = double.Parse(this.Value); break;
        case SearchFieldType.Date: rhs = DateTime.Parse(this.Value); break;
        default: rhs=this.Value; break;
    }

    switch (this.SearchOperator)
{
    case SearchOperator.EqualTo: return lhs == rhs;
    case SearchOperator.NotEqualTo: return lhs != rhs;
    case SearchOperator.LessThan: return (rhs is int || rhs is double) && lhs < rhs;
    case SearchOperator.GreaterThan: return (rhs is int || rhs is double) && lhs > rhs;
    case SearchOperator.LessThanOrEqualTo: return (rhs is int || rhs is double) && lhs <= rhs;
    case SearchOperator.GreaterThanOrEqualTo: return (rhs is int || rhs is double) && lhs >= rhs;
    case SearchOperator.StartsWith: return lhs.ToString().StartsWith(rhs.ToString());
    case SearchOperator.EndsWith: return lhs.ToString().EndsWith(rhs.ToString());
    case SearchOperator.Contains: return lhs.ToString().Contains(rhs.ToString());
}
    }
    }
}