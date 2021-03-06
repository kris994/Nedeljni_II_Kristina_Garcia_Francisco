﻿using Nedeljni_II_Kristina_Garcia_Francisco.Helper;
using System.ComponentModel;

namespace Nedeljni_II_Kristina_Garcia_Francisco.Model
{
    /// <summary>
    /// Manager validations
    /// </summary>
    public partial class vwClinicManager : IDataErrorInfo
    {
        Validations validation = new Validations();

        /// <summary>
        /// Returns full manager name
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        /// <summary>
        /// Total amount of propertis we are checking
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "IdentificationCard",
            "Username",
            "UserPassword",
            "Gender",
            "MaxNumberOfDoctors",
            "MinNumberOfRooms",
            "DateOfBirth"
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

                    case "MinNumberOfRooms":
                        result = this.validation.OmissionRestriction(MinNumberOfRooms, OmissionNumber);
                        break;

                    case "MaxNumberOfDoctors":
                        result = this.validation.OmissionRestriction(MaxNumberOfDoctors, OmissionNumber);
                        break;

                    case "DateOfBirth":
                        result = this.validation.CheckDate(DateOfBirth);
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