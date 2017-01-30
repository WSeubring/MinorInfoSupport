using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaZ.FeWebshop.Domain.Test.Utils
{
    public class ModelValidation
    {
        private object _model;
        public IList<ValidationResult> ValidationResultList { get; private set; }

        public ModelValidation(object model)
        {
            _model = model;
            ValidateModel();
        }

        private void ValidateModel()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_model, null, null);
            bool validateAllProperties = true;

            Validator.TryValidateObject(_model, validationContext, validationResults, validateAllProperties);

            ValidationResultList =  validationResults;
        }


        public bool ContainsErrorMessage(string errorMessage)
        {
            foreach (ValidationResult validationResult in ValidationResultList)
            {
                if (validationResult.ErrorMessage.Equals(errorMessage)) return true;
            }
            return false;
        }
    }
}
