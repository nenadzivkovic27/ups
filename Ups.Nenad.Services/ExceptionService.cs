using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ups.Nenad.Helper;

namespace Ups.Nenad.Services
{
    public class ExceptionService : IExceptionService
    {
        protected ILogger _logger;

        public ExceptionService(ILogger<ExceptionService> logger)
        {
            _logger = logger;
        }

        public void LogException(Exception ex)
        {
            _logger.LogError(ex.Message);
            if (ex.InnerException != null)
            {
                LogException(ex.InnerException);
            }
        }

        public void ShowErrorMessage(Exception ex)
        {
            Utilities.ShowError(ex.Message);
        }

        public void HandleException(Exception ex)
        {
            LogException(ex);
            ShowErrorMessage(ex);
        }
    }
}
