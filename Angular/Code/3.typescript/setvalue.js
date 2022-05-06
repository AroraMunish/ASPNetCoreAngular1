var Employee = /** @class */ (function () {
    //makes the parameters as optional
    function Employee(empCode, empName) {
        this.empCode = empCode;
        this.empName = empName;
    }
    Object.defineProperty(Employee.prototype, "Salary", {
        get: function () {
            return this.salary;
        },
        set: function (val) {
            if (val < 0)
                throw new Error('Salary can not be less than 0');
            this.salary = val;
        },
        enumerable: false,
        configurable: true
    });
    Employee.prototype.details = function () {
        var dtls = this.empName + '(Ecode:' + this.empCode + ") is drawing a salary of " + this.Salary;
        return dtls;
    };
    return Employee;
}());
var emp = new Employee(100, "Bill");
emp.Salary = -5;
//create a function which takes object of type Employee declared above
var printEmployee = function (e1) {
    console.log(e1.details());
};
//invoke function printEmp and pass employee object
printEmployee(emp);
