using NeuroEase.Core.Model.Dto;
using Newtonsoft.Json;

namespace NeuroEase.Core.Middleware
{
    public class DataStandadization
    {
        public void DataStandardization(object data)
        {
            var dataToString = data.ToString();
            var dataToStandadization = JsonConvert.DeserializeObject<List<CompanyDataDto>>(dataToString);

            dataToStandadization = Standardize(dataToStandadization);
            string standardizedJson = JsonConvert.SerializeObject(dataToStandadization, Formatting.Indented);

        }
        static List<CompanyDataDto> Standardize(List<CompanyDataDto> data)
        {
            double revenueMean = data.Average(c => c.Revenue);
            double revenueStd = Math.Sqrt(data.Sum(c => Math.Pow(c.Revenue - revenueMean, 2)) / data.Count);

            double expensesMean = data.Average(c => c.Expenses);
            double expensesStd = Math.Sqrt(data.Sum(c => Math.Pow(c.Expenses - expensesMean, 2)) / data.Count);

            double profitMean = data.Average(c => c.ProfitMargin);
            double profitStd = Math.Sqrt(data.Sum(c => Math.Pow(c.ProfitMargin - profitMean, 2)) / data.Count);
            return data.Select(c => new CompanyDataDto
            {
                Company = c.Company,
                Revenue = (c.Revenue - revenueMean) / revenueStd,
                Expenses = (c.Expenses - expensesMean) / expensesStd,
                ProfitMargin = (c.ProfitMargin - profitMean) / profitStd
            }).ToList();
        }
    }
}
