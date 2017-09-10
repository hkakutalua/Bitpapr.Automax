using Bitpapr.Automax.Commands;
using Bitpapr.Automax.Core.Model;
using Bitpapr.Automax.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bitpapr.Automax.ViewModels
{
    public class ManageEmployeesViewModel : BaseWindowViewModel
    {
        private readonly IEmployeeService _employeeService;

        public int CurrentEmployeeIndex { get; set; } = -1;
        public ObservableCollection<Employee> Employees { get; set; }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand ActivateEmployeeCommand { get; set; }
        public ICommand DisableEmployeeCommand { get; set; }

        public ManageEmployeesViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

            Employees = new ObservableCollection<Employee>(
                _employeeService.GetAllRegularsAndManagers());

            AddEmployeeCommand = new RelayCommand(ExecuteAddEmployee);
            ActivateEmployeeCommand = new RelayCommand(ExecuteActivateEmployee, CanActivateEmployee);
            DisableEmployeeCommand = new RelayCommand(ExecuteDisableEmployee, CanDisableEmployee);
        }

        private void ExecuteAddEmployee()
        {
            throw new NotImplementedException();
        }

        private void ExecuteActivateEmployee()
        {
            Guid employeeId = Employees[CurrentEmployeeIndex].Id;
            _employeeService.ActivateEmployee(employeeId);

            UpdateEmployeeCollection();
        }

        private bool CanActivateEmployee()
        {
            return (CurrentEmployeeIndex != -1) &&
                   !(Employees[CurrentEmployeeIndex].Active);
        }

        private void ExecuteDisableEmployee()
        {
            Guid employeeId = Employees[CurrentEmployeeIndex].Id;
            _employeeService.DisableEmployee(employeeId);

            UpdateEmployeeCollection();
        }

        private bool CanDisableEmployee()
        {
            return (CurrentEmployeeIndex != -1) &&
                   Employees[CurrentEmployeeIndex].Active;
        }

        private void UpdateEmployeeCollection()
        {
            Employees.Clear();
            foreach (Employee employee in _employeeService.GetAllRegularsAndManagers())
                Employees.Add(employee);
        }
    }
}
