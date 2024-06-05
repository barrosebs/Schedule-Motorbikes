using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Domain.Enum;
using SM.Domain.Model;
using SM.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace SM.Web.Helpers
{
    public static class ToolsHelpers
    {
        /// <summary>
        /// Remove mask
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static string RemoveMaskCNPJ(string cnpj)
        {
            string cnpjClear = Regex.Replace(cnpj, "[./-]", "");
            return cnpjClear;
        }
        /// <summary>
        /// Uplods to files
        /// </summary>
        /// <param name="file"></param>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public static string UploadFile(IFormFile file, string subFolder)
        {
            string extension = Path.GetExtension(file.FileName);
            if (extension != ".png" || extension == ".bmp")
                throw new ApplicationException("Arquivo não é válido! Formato do arquivo deve ser: .png ou .bmp");

            var folderUploads = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", $"CNH_{subFolder}");

            if (!Directory.Exists(folderUploads))
            {
                Directory.CreateDirectory(folderUploads);
            }

            var pathFile = Path.Combine(folderUploads, file.FileName);

            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return pathFile;
        }

        public static AllocationVM CalcDays(AllocationVM allocation, PlanModel plan)
        {
            if (allocation.DeliveryDate <= allocation.StartDateToAllocation)
                throw new ValidationException("Data de ENTREGA não pode ser menor/igual a data de INíCIO");

            DateTime startDate = allocation.StartDateToAllocation;
            TimeSpan usedDays = allocation.DeliveryDate - startDate;
            decimal remainingDays = (decimal)plan.LimitDayPlan - (decimal)usedDays.TotalDays;
            allocation.UsedDays = usedDays.Days;
            decimal resultPlan = CalcToPlan(allocation, plan, remainingDays);
            if (usedDays.TotalDays < plan.LimitDayPlan)
            {
                allocation.Sum = (decimal)usedDays.TotalDays * plan.Value + resultPlan;
                allocation.ValueDay = plan.Value * (decimal)allocation.UsedDays;

            }
            else if (plan.LimitDayPlan == usedDays.TotalDays)
            {
                allocation.UsedDays = usedDays.TotalDays - plan.LimitDayPlan;
                allocation.Sum = plan.Value * plan.LimitDayPlan;
                allocation.ValueDay = plan.Value * plan.LimitDayPlan;
            }
            else
            {
                allocation.UsedDays = usedDays.TotalDays;
                allocation.ValueDay = (decimal)allocation.UsedDays * plan.Value;
                allocation.Sum = allocation.ValueDay + 50;
            }

            return allocation;
        }

        private static decimal CalcToPlan(AllocationVM viewModel, PlanModel plan, decimal remainingDays)
        {
            if (viewModel.EPlan == EAllocationPlan.Basic)
                viewModel.DailyRate = 0.20m;

            if (viewModel.EPlan == EAllocationPlan.Standard)
                viewModel.DailyRate = 0.40m;

            //Não vi na especificação a taxa referente a outros planos
            if (viewModel.EPlan != EAllocationPlan.Standard && viewModel.EPlan != EAllocationPlan.Basic)
                viewModel.DailyRate = 0;

            decimal remainingDaysTotalWithPenalty = (decimal)remainingDays * plan.Value * (viewModel.DailyRate);
            viewModel.Sum = viewModel.ValueDay + remainingDaysTotalWithPenalty;
            return viewModel.Sum;
        }


    }
}
