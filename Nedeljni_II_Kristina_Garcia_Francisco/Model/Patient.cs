using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using System.ComponentModel;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    public partial class vwClinicPatient : IDataErrorInfo
    {
        Validations validation = new Validations();

        /// <summary>
        /// Total amount of propertis we are checking
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "IdentificationCard",
            "Username",
            "UserPassword",
            "Gender",
            "HealthCareNumber"
        };

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    // there is an error
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Checks if the inputs are incorrect
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Does validations on the property location
        /// </summary>
        /// <param name="propertyName">property we are checking</param>
        /// <returns>if the property is valid (null) or error (string)</returns>
        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "Username":
                        result = this.validation.UsernameChecker(Username, UserID);
                        break;

                    case "IdentificationCard":
                        result = this.validation.IdentificationCardChecker(IdentificationCard, UserID);
                        break;

                    case "UserPassword":
                        result = this.validation.PasswordValidation(UserPassword);
                        break;

                    case "Gender":
                        result = this.validation.CannotBeEmpty(Gender);
                        break;

                    case "HealthCareNumber":
                        result = this.validation.HealthCareNumberChecker(HealthCareNumber, UserID);
                        break;

                    default:
                        result = null;
                        break;
                }

                return result;
            }
        }
    }
}

