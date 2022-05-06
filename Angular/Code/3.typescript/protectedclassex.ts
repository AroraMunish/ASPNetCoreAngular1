class Employee {
    protected empCode: number;
    protected empName: string;
    salary:number;

    //makes the parameters as optional
    constructor(code?: number, name?: string) {
            this.empName = name;
            this.empCode = code;
    }

    getSalary() : number {
        return this.salary;
    }

    details() : string {
        let dtls =this.empName + '(Ecode:' +this.empCode +") is drawing a salary of " + this.salary;
        return dtls;
    }
}
class SalesEmployee extends Employee{
    private department: string;
    
    constructor(code: number, name: string, department: string) {
        super(code, name);
        this.department = department;
    }
    SalesEmpdetails() : string {
        let dtls =this.details() + " and works in " +this.department;
        return dtls;
    }
}

let saleEmp = new SalesEmployee(100,"Bill","Public Sales");
saleEmp.salary = 12000;

//create a function which takes object of type Employee declared above
let printEmployee =(e1: SalesEmployee)=>{
    console.log(e1.SalesEmpdetails());
}
//invoke function printEmp and pass employee object
printEmployee(saleEmp);

