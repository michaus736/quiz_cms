using System;
using System.Collections.Generic;
using System.Text;

namespace QuizVistaApiBusinnesLayer.Models
{
    public class ModelWithResult<T> where T : class
    {
        public T? result;
        public bool isValid;
        public string errorMessage;

        public ModelWithResult(T result)
        {
            this.result = result;
            this.isValid = true;
            errorMessage = string.Empty;
        }

        public ModelWithResult(string errorMessage)
        {
            this.result = null;
            this.errorMessage = errorMessage;
            this.isValid = false;
        }

    }
}
