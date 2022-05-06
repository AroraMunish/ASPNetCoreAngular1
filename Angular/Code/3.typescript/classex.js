var Employee = /** @class */ (function () {
    //makes the parameters as optional
    function Employee(code, name) {
        this.empName = name;
        this.empCode = code;
    }
    Employee.prototype.getSalary = function () {
        return this.salary;
    };
    Employee.prototype.details = function () {
        var dtls = this.empName + '(Ecode:' + this.empCode + ") is drawing a salary of " + this.salary;
        return dtls;
    };
    return Employee;
}());
var emp = new Employee(100, "Bill");
emp.salary = 12000;
//create a function which takes object of type Employee declared above
var printEmployee = function (e1) {
    console.log(e1.details());
};
//invoke function printEmp and pass employee object
printEmployee(emp);
