//offer schema for Offer Object
export class Offer{
    constructor(
        public offerId:number,
        public noOfPoints:number,
        public employeeId:number,
        public status:string,
        public likes:number,
        public category:string,
        public details:string,
        public openedDate:Date,
        public engagedDate:Date,
        public closedDate:Date,
    ){}
}