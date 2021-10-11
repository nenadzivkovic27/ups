using System;

namespace Ups.Nenad.Services
{
    public interface IExceptionService
    {
        void HandleException(Exception ex);
        void LogException(Exception ex);
        void ShowErrorMessage(Exception ex);
    }
}