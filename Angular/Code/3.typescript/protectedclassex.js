var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
var SalesEmployee = /** @class */ (function (_super) {
    __extends(SalesEmployee, _super);
    function SalesEmployee(code, name, department) {
        var _this = _super.call(this, code, name) || this;
        _this.department = department;
        return _this;
    }
    SalesEmployee.prototype.SalesEmpdetails = function () {
        var dtls = this.details() + " and works in " + this.department;
        return dtls;
    };
    return SalesEmployee;
}(Employee));
var saleEmp = new SalesEmployee(100, "Bill", "Public Sales");
saleEmp.salary = 12000;
//create a function which takes object of type Employee declared above
var printEmployee = function (e1) {
    console.log(e1.SalesEmpdetails());
};
//invoke function printEmp and pass employee object
printEmployee(saleEmp);
