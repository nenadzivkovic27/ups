using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ups.Nenad.DataTypes;

namespace Ups.Nenad.Helper
{
    public static class Utilities
    {
        #region MessageBoxes

        public static void ShowMessage(string message, string title = "Message")
        {
            _ = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string message, string title = "Error")
        {
            _ = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowQuestion(string message, string title = "Question")
        {
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool ShowWarningQuestion(string message, string title = "Warning")
        {
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return result == DialogResult.Yes;
        }

        #endregion

        #region ExportCSV

        public static string ExportToCSV(List<User> users, string filePath = null)
        {
            if (filePath == null)
            {
                string file = "upsExport-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv";
                filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), file);
            }

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }

            return filePath;
        }

        public static void OpenCsv(string filePath)
        {
            if (File.Exists(filePath))
            {
                Process.Start("explorer",filePath);
            }
        }

        #endregion
    }


}
