//response schema for rest api
export class AuthResponse{
    constructor(
        public token:string,
        public empid:string
        ){}
}