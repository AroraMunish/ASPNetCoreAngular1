//create a function which takes object of type Employee declared above
var printEmployee = function (emp) {
    console.log(emp.name + ' with employee id:' + emp.empid + ' works in ' + emp.organisation);
};
//Create an employee object
var employee = {};
employee.name = "John";
employee.empid = 123;
employee.organisation = 'Best Org';
//invoke function printEmp and pass employee object
printEmployee(employee);
