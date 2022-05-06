class Employee {
    salary:number;

    //makes the parameters as optional
    constructor(private empCode?: number,private empName?: string) {
    }

    getSalary() : number {
        return this.salary;
    }

    details() : string {
        let dtls =this.empName + '(Ecode:' +this.empCode +") is drawing a salary of " + this.salary;
        return dtls;
    }
}

let emp = new Employee(100,"Bill");
emp.salary = 12000;

//create a function which takes object of type Employee declared above
let printEmployee =(e1: Employee)=>{
    console.log(e1.details());
}
//invoke function printEmp and pass employee object
printEmployee(emp);

