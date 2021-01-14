using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Summary
{
    public class GetSummaryByCarIdResponse : IResponseData
    {
        public GetSummaryByCarIdResponse()
        {            
            CategoriesSummary = new List<CategorySummary>();
        }

        public int ExpensesCount { get; set; }
        public decimal ExpensesTotalAmount { get; set; }       
        public List<CategorySummary> CategoriesSummary { get; set; }
    }
}