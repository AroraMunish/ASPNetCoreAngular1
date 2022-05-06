//create an interface
interface Employee {
    empid:number,
    name:string,
    organisation:string
}

//create a function which takes object of type Employee declared above
let printEmployee =(emp: Employee)=>{
    console.log(emp.name + ' with employee id:' + emp.empid +' works in ' + emp.organisation);
}

//Create an employee object
let employee = <Employee> { }; 
employee.name = "John";
employee.empid = 123;
employee.organisation = 'Best Org';

//invoke function printEmp and pass employee object
printEmployee(employee);
