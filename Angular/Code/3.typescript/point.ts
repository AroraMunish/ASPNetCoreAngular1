export class Point{
    constructor(private x?:number, y?:number ){

    }
    draw(){
        console.log('X:'+this.x + ', Y:' + this.y);
    }    
}
