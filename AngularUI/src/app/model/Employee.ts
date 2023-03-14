//employee schema for Employee object
export class Employee{
    constructor(
        public employeeId:number,
        public employeeName:string,
        public password :string,
       public pointsGained:number,
    ){}
}