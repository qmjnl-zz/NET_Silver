using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Silver
{
    public partial class Entity : INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> errorsDictionary = new Dictionary<string, List<string>>();
        public bool HasErrors => errorsDictionary.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return errorsDictionary.Values;
            }
            return errorsDictionary.ContainsKey(propertyName) ? errorsDictionary[propertyName] : null;
        }

        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        protected void AddErrors(string propertyName, IList<string> errors)
        {
            if (errors == null || errors.Count == 0) return;

            bool changed = false;
            if (!errorsDictionary.ContainsKey(propertyName))
            {
                errorsDictionary.Add(propertyName, new List<string>());
                changed = true;
            }
            foreach (string error in errors)
            {
                if (errorsDictionary[propertyName].Contains(error)) continue;
                errorsDictionary[propertyName].Add(error);
                changed = true;
            }
            if (changed)
            {
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName = "")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                errorsDictionary.Clear();
            }
            else
            {
                errorsDictionary.Remove(propertyName);
            }
            OnErrorsChanged(propertyName);
        }
    }
}
