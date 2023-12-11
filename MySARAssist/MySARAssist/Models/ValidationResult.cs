using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Models
{
    public class ValidationResult
    {
        private bool _success = false;
        private string _message;

        public ValidationResult() { }
        public bool success { get => _success; set => _success = value; }
        public string message { get => _message; set => _message = value; }

    }
}
