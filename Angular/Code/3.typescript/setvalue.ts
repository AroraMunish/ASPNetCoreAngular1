class Employee {
    private salary:number;

    //makes the parameters as optional
    constructor(private empCode?: number,private empName?: string) {
    }

    get Salary() : number {
        return this.salary;
    }

    set Salary(val: number) {
        if (val<0) throw new Error('Salary can not be less than 0');

        this.salary=val;
    }

    details() : string {
        let dtls =this.empName + '(Ecode:' +this.empCode +") is drawing a salary of " + this.Salary;
        return dtls;
    }
}

let emp = new Employee(100,"Bill");
emp.Salary=-5;

//create a function which takes object of type Employee declared above
let printEmployee =(e1: Employee)=>{
    console.log(e1.details());
}
//invoke function printEmp and pass employee object
printEmployee(emp);

